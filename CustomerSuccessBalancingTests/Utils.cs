using CustomerSuccessBalancingService.Models;

namespace CustomerSuccessBalacingTest
{
	public static class Utils
	{
		public static List<Client> BuildClients(params int[] sizes)
		{
			var clientList = new List<Client>();

			for (int i = 0; i < sizes.Length; i++)
				clientList.Add(new Client(i + 1, sizes[i]));

			return clientList;
		}

		public static List<Client> BuildClientsFromValue(int length, int value, int? defaultId = null)
		{
			var clientList = new List<Client>();

			for (int i = 0; i < length; i++)
				clientList.Add(new Client(defaultId ?? i + 1, value));

			return clientList;
		}

		public static List<CustomerSuccess> BuildCustomerSuccess(params int[] levels)
		{
			var customerSuccessList = new List<CustomerSuccess>();

			for (int i = 0; i < levels.Length; i++)
				customerSuccessList.Add(new CustomerSuccess(i + 1, levels[i]));
			return customerSuccessList;
		}

		public static List<CustomerSuccess> BuildCustomerSuccessRange(int initial, int end, int? defaultId = null)
		{
			var customerSuccessList = new List<CustomerSuccess>();

			for (int i = 0; i < end; i++)
			{
				customerSuccessList.Add(new CustomerSuccess(defaultId ?? i + 1, initial));
				initial++;
			}

			return customerSuccessList;
		}
	}
}
