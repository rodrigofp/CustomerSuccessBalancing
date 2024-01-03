using ClientSuccessBalancingService.Extensions;
using CustomerSuccessBalancingService.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomerSuccessBalacingTest
{
	public class ClientTests
	{

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test_Scenario_One()
		{
			var clients = Utils.BuildClients();

			Assert.That(clients.IsValid(), Is.False);
		}

		[Test]
		public void Test_Scenario_Two()
		{
			var clients = Utils.BuildClientsFromValue(1000000, 1, 10);

			Assert.That(clients.IsValid(), Is.False);
		}

		[Test]
		public void Test_Scenario_Three()
		{
			Assert.Throws<ValidationException>(() => new Client(1000000, 1));
		}

		[Test]
		public void Test_Scenario_Four()
		{
			Assert.Throws<ValidationException>(() => new Client(-1, 1));
		}

		[Test]
		public void Test_Scenario_Five()
		{
			Assert.Throws<ValidationException>(() => new Client(1, -1));
		}

		[Test]
		public void Test_Scenario_Six()
		{
			Assert.Throws<ValidationException>(() => new Client(1, 100000));
		}
	}
}
