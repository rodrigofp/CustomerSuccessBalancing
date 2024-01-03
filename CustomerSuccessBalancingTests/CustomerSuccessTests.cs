using CustomerSuccessBalancingService.Extensions;
using CustomerSuccessBalancingService.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomerSuccessBalacingTest
{
	public class CustomerSuccessTests
	{

		[SetUp]
		public void SetUp()
		{
		}

		[Test]
		public void Test_Scenario_One()
		{
			var customerSuccess = Utils.BuildCustomerSuccess();

			Assert.That(customerSuccess.IsValid(), Is.False);
		}

		[Test]
		public void Test_Scenario_Two()
		{
			var customerSuccess = Utils.BuildCustomerSuccessRange(1, 1000, 1);

			Assert.That(customerSuccess.IsValid(), Is.False);
		}

		[Test]
		public void Test_Scenario_Three()
		{
			Assert.Throws<ValidationException>(() => new CustomerSuccess(1, -1));
		}

		[Test]
		public void Test_Scenario_Four()
		{
			Assert.Throws<ValidationException>(() => new CustomerSuccess(1, 10000));
		}

		[Test]
		public void Test_Scenario_Five()
		{
			Assert.Throws<ValidationException>(() => new CustomerSuccess(-1, 1));
		}

		[Test]
		public void Test_Scenario_Six()
		{
			Assert.Throws<ValidationException>(() => new CustomerSuccess(1000, 1));
		}

		[Test]
		public void Test_Scenario_Seven()
		{
			var customerSuccess = new List<CustomerSuccess> { new(1, 1) };
			customerSuccess[0].SetAbsence(true);

			Assert.That(customerSuccess.IsValid(), Is.False);
		}

		[Test]
		public void Test_Scenario_Eight()
		{
			var customerSuccess = Utils.BuildCustomerSuccess(10, 10);

			Assert.That(customerSuccess.IsValid(), Is.False);
		}
	}
}
