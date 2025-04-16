using System.ComponentModel.DataAnnotations;

namespace Courses_API.Models
{
	public class Lesson
	{
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? InstructorName { get; set; }
		[Required]
		public int Hours { get; set; }
		[Required]
		public int CourseId { get; set; }
		[Required]
		public Course? Course { get; set; }
	}
}
