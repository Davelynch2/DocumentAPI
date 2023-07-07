namespace DocumentAPI.Models
{
	public class Event
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public DateTime DeclarationDateTime { get; set; }
		public User DeclaredBy { get; set; }
		public Guid DeclaredById { get; set; }
		public List<FileModel>? Documents { get; set; }
	}
}
