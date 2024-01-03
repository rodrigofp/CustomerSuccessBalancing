using ClientSuccessBalancingService.Extensions;
using CustomerSuccessBalancingService.Extensions;
using CustomerSuccessBalancingService.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomerSuccessBalancingServices
{
	public class CustomerSuccessBalancing
	{
		private readonly IEnumerable<Client> _clients;
		private readonly IEnumerable<CustomerSuccess> _customerSuccesses;

		public CustomerSuccessBalancing(IEnumerable<CustomerSuccess> customerSuccesses, IEnumerable<Client> clients, int[] awayCustomerSuccessIds)
		{
			customerSuccesses.SetAbsences(awayCustomerSuccessIds);

			ValidateData(customerSuccesses, clients);

			_customerSuccesses = customerSuccesses.GetActive();
			_clients = clients;
		}

		private static void ValidateData(IEnumerable<CustomerSuccess> customerSuccesses, IEnumerable<Client> clients)
		{
			if (!customerSuccesses.IsValid() || !clients.IsValid())
				throw new ValidationException();
		}

		public int Execute()
		{
			AllocateClientsToCustomerSuccess();
			return GetCustomerSuccessIdWithMostClients();
		}

		private void AllocateClientsToCustomerSuccess()
		{
			foreach (var client in _clients)
			{
				var customerSuccess = GetCustomerSuccessByClientSize(client.Size);

				if (customerSuccess == null)
					continue;

				customerSuccess.AddClient();
			}
		}

		private CustomerSuccess? GetCustomerSuccessByClientSize(int clientSize)
		{
			return _customerSuccesses
				.Where(cs => cs.Level >= clientSize)
				.MinBy(cs => cs.Level - clientSize);
		}

		private int GetCustomerSuccessIdWithMostClients()
		{
			var maxClientsOnCustomerSuccess = _customerSuccesses.Max(cs => cs.NumberOfClients);

			var customerSuccess = _customerSuccesses.Where(cs => cs.NumberOfClients == maxClientsOnCustomerSuccess);

			if (customerSuccess.Count() > 1)
				return 0;

			return customerSuccess.Single().Id;
		}
	}
}