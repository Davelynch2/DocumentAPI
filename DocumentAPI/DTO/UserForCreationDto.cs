using DocumentAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace DocumentAPI.DTO
{
	public record UserForCreationDto
	{
		[Required(ErrorMessage = "User first name is a required field.")]
		public string? FirstName { get; set; }

		[Required(ErrorMessage = "User last name is a required field.")]
		public string? LastName { get; set; }

		[Required(ErrorMessage = "User hire date is a required field.")]
		public DateTime HireDate { get; set; }

		public List<Event>? Events { get; init; }
	}
}
