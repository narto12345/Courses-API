using System.ComponentModel.DataAnnotations;

namespace Courses_API.Dtos
{
	public class LessonRequestDto
	{
		[Required]
		public required string Name { get; set; }
		[Required]
		public required string InstructorName { get; set; }
		[Required]
		public required int? Hours { get; set; }
		[Required]
		public required int? CourseId { get; set; }
	}
}
