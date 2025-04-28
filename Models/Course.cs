using System.ComponentModel.DataAnnotations;

namespace Courses_API.Models
{
	public class Course
	{
		public int Id { get; set; }
		[Required]
		public string? Code { get; set; }
		[Required]
		public string? Name { get; set; }
		public string? Description { get; set; }
		public List<Lesson>? Lessons { get; set; }
		public List<UserCourse> Users { get; set; } = [];
	}
}
