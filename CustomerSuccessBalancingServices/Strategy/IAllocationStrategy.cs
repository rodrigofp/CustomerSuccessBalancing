using CustomerSuccessBalancingService.Models;

namespace CustomerSuccessBalancingService.Strategy
{
	public interface IAllocationStrategy
	{
		void AllocateClients(IEnumerable<CustomerSuccess> customerSuccesses, IEnumerable<Client> clients);
	}
}
