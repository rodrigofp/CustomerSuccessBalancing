using CustomerSuccessBalancingService.Models;

namespace ClientSuccessBalancingService.Extensions
{
	public static class ClientExtension
	{
		private const int MAX_COUNT = 1000000;

		public static bool IsValid(this IEnumerable<Client> clients)
		{
			if (clients.IsEmptyOrOversized()
				|| clients.Any(c => !c.IsValid()))
				return false;

			return true;
		}

		private static bool IsEmptyOrOversized(this IEnumerable<Client> clients)
		{
			return !clients.Any() || clients.Count() >= MAX_COUNT;
		}
	}
}
