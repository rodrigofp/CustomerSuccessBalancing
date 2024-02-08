using CustomerSuccessBalancingService.Extensions;
using CustomerSuccessBalancingService.Models;

namespace CustomerSuccessBalancingService.Strategy
{
	public class AllocationByClientSizeStrategy : IAllocationStrategy
	{
		public void AllocateClients(IEnumerable<CustomerSuccess> customerSuccesses, IEnumerable<Client> clients)
		{
			foreach (var client in clients)
			{
				var customerSuccess = customerSuccesses.GetMostAdequadeByClientSize(client.Size);

				if (customerSuccess == null)
					continue;

				customerSuccess.AddClient();
			}
		}
	}
}
