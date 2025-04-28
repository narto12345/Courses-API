using Microsoft.EntityFrameworkCore;

namespace Courses_API.Models
{
	[PrimaryKey(nameof(UserId), nameof(CourseId))]
	public class UserCourse
	{
		public int UserId { get; set; }
		public int CourseId { get; set; }
		public User? User { get; set; }
		public Course? Course { get; set; }
	}
}
