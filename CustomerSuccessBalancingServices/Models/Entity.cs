namespace CustomerSuccessBalacingService.Models
{
	public abstract class Entity
	{
		public int Id { get; private set; }

		public Entity(int id)
		{
			Id = id;
		}

		public abstract bool IsValid();
	}
}