using CustomerSuccessBalancingService.Strategy;

namespace CustomerSuccessBalancingService.Factory
{
	public interface IAllocationStrategyFactory
	{
		IAllocationStrategy CreateStrategy();
	}
}