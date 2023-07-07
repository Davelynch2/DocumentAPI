namespace DocumentAPI.DTO
{
	[Serializable]
	public record UserDto
	{
		public Guid Id { get; init; }

		public string? FullName { get; init; }
	}
}
