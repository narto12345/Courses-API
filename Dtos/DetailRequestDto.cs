using System.ComponentModel.DataAnnotations;

namespace Courses_API.Dtos
{
	public class DetailRequestDto
	{
		public string? Email { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public required string Address { get; set; }
		public DateOnly? Birthdate { get; set; }

		[Required(ErrorMessage = "El campo {0} es requerido")]
		public required int? UserIdFk { get; set; }
	}
}
