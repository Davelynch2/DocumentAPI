namespace DocumentAPI.DTO
{
	[Serializable]
	public record class EventDto
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public DateTime DeclarationDateTime { get; set; }
		public UserDto DeclaredBy { get; set; }

		public List<FileDto> Documents { get; set; }
	}
}
