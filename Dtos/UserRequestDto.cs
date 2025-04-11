using System.ComponentModel.DataAnnotations;

namespace Courses_API.Dtos
{
	public class UserRequestDto
	{
		[Required]
		public required string UserName { get; set; }
		[Required]
		public required string Name { get; set; }

		public string? Lastname { get; set; }
	}
}
