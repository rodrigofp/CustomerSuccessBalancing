using ClientSuccessBalancingService.Extensions;
using CustomerSuccessBalancingService.Extensions;
using CustomerSuccessBalancingService.Factory;
using CustomerSuccessBalancingService.Models;
using CustomerSuccessBalancingService.Strategy;
using System.ComponentModel.DataAnnotations;

namespace CustomerSuccessBalancingServices
{
	public class CustomerSuccessBalancing
	{
		private readonly IEnumerable<Client> _clients;
		private readonly IEnumerable<CustomerSuccess> _customerSuccesses;
		private readonly IAllocationStrategy _allocationStrategy;

		public CustomerSuccessBalancing(IEnumerable<CustomerSuccess> customerSuccesses, IEnumerable<Client> clients, int[] awayCustomerSuccessIds, IAllocationStrategyFactory factory)
		{
			customerSuccesses.SetAbsences(awayCustomerSuccessIds);

			ValidateData(customerSuccesses, clients);

			_customerSuccesses = customerSuccesses.GetActive();
			_clients = clients;
			_allocationStrategy = factory.CreateStrategy();
		}

		private static void ValidateData(IEnumerable<CustomerSuccess> customerSuccesses, IEnumerable<Client> clients)
		{
			if (!customerSuccesses.IsValid() || !clients.IsValid())
				throw new ValidationException();
		}

		public int Execute()
		{
			_allocationStrategy.AllocateClients(_customerSuccesses, _clients);

			return GetCustomerSuccessIdWithMostClients();
		}

		private int GetCustomerSuccessIdWithMostClients()
		{
			var maxNumberOfClients = _customerSuccesses.Max(cs => cs.NumberOfClients);

			try { return _customerSuccesses.Single(cs => cs.NumberOfClients == maxNumberOfClients).Id; }
			catch (InvalidOperationException) { return 0; }
		}
	}
}