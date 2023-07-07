using DocumentAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace DocumentAPI.DTO
{
	public record EventForCreationDto
	{
		[Required(ErrorMessage = "Event description is a required field.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Event Declaration date time is a required field.")]
		public DateTime DeclarationDateTime { get; set; }

		[Required(ErrorMessage = "The Id of the user who declare the event is a required field.")]
		public Guid DeclaredById { get; set; }

		public List<FileModel>? Documents { get; init; }
	}
}
