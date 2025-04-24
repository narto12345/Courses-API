using System.ComponentModel.DataAnnotations;

namespace Courses_API.Dtos
{
	public class CourseRequestDto
	{
		[Required]
		public required string Name { get; set; }
		public string? Description { get; set; }
	}
}
