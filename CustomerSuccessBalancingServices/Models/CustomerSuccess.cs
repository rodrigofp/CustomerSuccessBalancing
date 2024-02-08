using CustomerSuccessBalacingService.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomerSuccessBalancingService.Models
{
	public class CustomerSuccess : Entity
	{
		private const int MIN_ID = 0;
		private const int MAX_ID = 1000;
		private const int MIN_LEVEL = 0;
		private const int MAX_LEVEL = 10000;

		public int Level { get; private set; }
		public bool IsAbsence { get; private set; }

		public int NumberOfClients { get; private set; }

		public CustomerSuccess(int id, int level)
			: base(id)
		{
			Level = level;
			IsAbsence = false;
			NumberOfClients = 0;

			if (!IsValid())
				throw new ValidationException(nameof(Client));
		}

		public void SetAbsence(bool isAbsence)
		{
			IsAbsence = isAbsence;
		}

		public void AddClient()
		{
			NumberOfClients++;
		}

		public bool CanAssumeClient(int clientSize)
		{
			return Level >= clientSize;
		}

		public int CalculateGapBetweenLevelAndClientSize(int clientSize)
		{
			if (CanAssumeClient(clientSize))
				return Level - clientSize;
			else
				return -1;
		}

		public override bool IsValid()
		{
			if (Id <= MIN_ID
				|| Id >= MAX_ID
				|| Level <= MIN_LEVEL
				|| Level >= MAX_LEVEL)
				return false;

			return true;
		}
	}
}