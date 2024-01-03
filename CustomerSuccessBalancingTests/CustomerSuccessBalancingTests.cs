using CustomerSuccessBalancingServices;
using System.Diagnostics;

namespace CustomerSuccessBalacingTest
{
	public class CustomerSuccessBalancingTests
	{

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test_Scenario_One()
		{
			var customerSuccess = Utils.BuildCustomerSuccess(60, 20, 95, 75);
			var clients = Utils.BuildClients(90, 20, 70, 40, 60, 10);
			var csAusenteIds = new int[] { 2, 4 };

			var balancer = new CustomerSuccessBalancing(customerSuccess, clients, csAusenteIds);
			var expectedResult = 1;

			Assert.That(expectedResult, Is.EqualTo(balancer.Execute()));
		}

		[Test]
		public void Test_Scenario_Two()
		{
			var customerSuccess = Utils.BuildCustomerSuccess(11, 21, 31, 3, 4, 5);
			var clients = Utils.BuildClients(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
			var csAusenteIds = Array.Empty<int>();

			var balancer = new CustomerSuccessBalancing(customerSuccess, clients, csAusenteIds);
			var expectedResult = 0;

			Assert.That(balancer.Execute(), Is.EqualTo(expectedResult));
		}

		[Test]
		public void Test_Scenario_Three()
		{
			var customerSuccess = Utils.BuildCustomerSuccessRange(1, 999);
			var clients = Utils.BuildClientsFromValue(10000, 998);
			var csAusenteIds = new int[] { 999 };

			//Set timeout workaround
			var timer = Stopwatch.StartNew();
			timer.Start();
			var balancer = new CustomerSuccessBalancing(customerSuccess, clients, csAusenteIds);
			timer.Stop();
			var expectedResult = 998;

			Assert.Multiple(() =>
			{
				Assert.That(timer.ElapsedMilliseconds, Is.LessThan(1000));
				Assert.That(balancer.Execute(), Is.EqualTo(expectedResult));
			});
		}

		[Test]
		public void Test_Scenario_Four()
		{
			var customerSuccess = Utils.BuildCustomerSuccess(1, 2, 3, 4, 5, 6);
			var clients = Utils.BuildClients(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
			var csAusenteIds = Array.Empty<int>();

			var balancer = new CustomerSuccessBalancing(customerSuccess, clients, csAusenteIds);
			var expectedResult = 0;

			Assert.That(balancer.Execute(), Is.EqualTo(expectedResult));
		}

		[Test]
		public void Test_Scenario_Five()
		{
			var customerSuccess = Utils.BuildCustomerSuccess(100, 2, 3, 6, 4, 5);
			var clients = Utils.BuildClients(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
			var csAusenteIds = Array.Empty<int>();

			var balancer = new CustomerSuccessBalancing(customerSuccess, clients, csAusenteIds);
			var expectedResult = 1;

			Assert.That(balancer.Execute(), Is.EqualTo(expectedResult));
		}

		[Test]
		public void Test_Scenario_Six()
		{
			var customerSuccess = Utils.BuildCustomerSuccess(100, 99, 88, 3, 4, 5);
			var clients = Utils.BuildClients(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
			var csAusenteIds = new int[] { 1, 3, 2 };

			var balancer = new CustomerSuccessBalancing(customerSuccess, clients, csAusenteIds);
			var expectedResult = 0;

			Assert.That(balancer.Execute(), Is.EqualTo(expectedResult));
		}

		[Test]
		public void Test_Scenario_Seven()
		{
			var customerSuccess = Utils.BuildCustomerSuccess(100, 99, 88, 3, 4, 5);
			var clients = Utils.BuildClients(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
			var csAusenteIds = new int[] { 4, 5, 6 };

			var balancer = new CustomerSuccessBalancing(customerSuccess, clients, csAusenteIds);
			var expectedResult = 3;

			Assert.That(balancer.Execute(), Is.EqualTo(expectedResult));
		}

		[Test]
		public void Test_Scenario_Eight()
		{
			var customerSuccess = Utils.BuildCustomerSuccess(60, 40, 95, 75);
			var clients = Utils.BuildClients(90, 70, 20, 40, 60, 10);
			var csAusenteIds = new int[] { 2, 4 };

			var balancer = new CustomerSuccessBalancing(customerSuccess, clients, csAusenteIds);
			var expectedResult = 1;

			Assert.That(balancer.Execute(), Is.EqualTo(expectedResult));
		}
	}
}