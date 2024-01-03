using CustomerSuccessBalacingService.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomerSuccessBalancingService.Models
{
	public class Client : Entity
	{
		private const int MIN_ID = 0;
		private const int MAX_ID = 1000000;
		private const int MIN_SIZE = 0;
		private const int MAX_SIZE = 100000;

		public int Size { get; private set; }

		public Client(int id, int size)
			: base(id)
		{
			Size = size;

			if (!IsValid())
				throw new ValidationException(nameof(Client));
		}

		public override bool IsValid()
		{
			if (Size <= MIN_SIZE
				|| Size >= MAX_SIZE
				|| Id <= MIN_ID
				|| Id >= MAX_ID)
				return false;

			return true;
		}
	}
}