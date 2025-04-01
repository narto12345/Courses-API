using System.ComponentModel.DataAnnotations;

namespace Courses_API.Models
{
	public class Detail
	{
		public int Id { get; set; }
		public string? Email { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public string? Address { get; set; }
		public DateOnly? Birthdate { get; set; }

		[Required(ErrorMessage = "El campo {0} es requerido")]
		public int? UserIdFk { get; set; }
		public User? User { get; set; }
	}
}
