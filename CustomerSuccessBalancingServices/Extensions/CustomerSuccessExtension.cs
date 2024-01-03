using CustomerSuccessBalancingService.Models;

namespace CustomerSuccessBalancingService.Extensions
{
	public static class CustomerSuccessExtension
	{
		private const int MAX_COUNT = 1000;

		public static void SetAbsences(this IEnumerable<CustomerSuccess> customerSuccesses, int[] absentIds)
		{
			if (absentIds.Length == 0)
				return;

			foreach (var customerSuccess in customerSuccesses.Where(cs => absentIds.Contains(cs.Id)))
				customerSuccess.SetAbsence(true);
		}

		public static IEnumerable<CustomerSuccess> GetActive(this IEnumerable<CustomerSuccess> customerSuccesses)
		{
			return customerSuccesses.Where(cs => !cs.IsAbsence);
		}

		public static bool IsValid(this IEnumerable<CustomerSuccess> customerSuccesses)
		{
			if (customerSuccesses.HaveDuplicatedLevels()
				|| customerSuccesses.ExceedMaxAbsenceAllowed()
				|| customerSuccesses.IsEmptyOrOversized()
				|| customerSuccesses.Any(cs => !cs.IsValid()))
				return false;

			return true;
		}

		private static bool HaveDuplicatedLevels(this IEnumerable<CustomerSuccess> customerSuccess)
		{
			return customerSuccess.GroupBy(cs => cs.Level).Any(cs => cs.Count() > 1);
		}

		private static bool ExceedMaxAbsenceAllowed(this IEnumerable<CustomerSuccess> customerSuccess)
		{
			var absentCount = customerSuccess.Count(cs => cs.IsAbsence);

			return absentCount > customerSuccess.GetMaxAbsenceAllowed();
		}

		private static int GetMaxAbsenceAllowed(this IEnumerable<CustomerSuccess> customerSuccess)
		{
			return (int)Math.Floor(customerSuccess.Count() / 2.0D);
		}

		private static bool IsEmptyOrOversized(this IEnumerable<CustomerSuccess> customerSuccess)
		{
			return !customerSuccess.Any() || customerSuccess.Count() >= MAX_COUNT;
		}
	}
}
