using System.ComponentModel.DataAnnotations;

namespace Courses_API.Dtos
{
	public class UserCredentialDto
	{
		[Required]
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		public string? Password { get; set; }
	}
}
