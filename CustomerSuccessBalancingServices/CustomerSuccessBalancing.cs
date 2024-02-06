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
				var customerSuccess = _customerSuccesses.GetMostAdequadeByClientSize(client.Size);

				if (customerSuccess == null)
					continue;

				customerSuccess.AddClient();
			}
		}

		private int GetCustomerSuccessIdWithMostClients()
		{
			var maxNumberOfClients = _customerSuccesses.Max(cs => cs.NumberOfClients);

			try { return _customerSuccesses.Single(cs => cs.NumberOfClients == maxNumberOfClients).Id; }
			catch (InvalidOperationException) { return 0; }
		}
	}
}