namespace DocumentAPI.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime HireDate { get; set; }
		public List<Event> Events { get; set; }
	}
}
