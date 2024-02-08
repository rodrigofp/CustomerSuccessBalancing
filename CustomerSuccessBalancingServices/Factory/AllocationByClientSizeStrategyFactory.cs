using CustomerSuccessBalancingService.Strategy;

namespace CustomerSuccessBalancingService.Factory
{
	public class AllocationByClientSizeStrategyFactory : IAllocationStrategyFactory
	{
		public IAllocationStrategy CreateStrategy()
		{
			return new AllocationByClientSizeStrategy();
		}
	}
}