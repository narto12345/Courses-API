namespace Courses_API.Dtos
{
	public class LessonDto
	{
		public required int Id { get; set; }
		public required string Name { get; set; }
		public required string InstructorName { get; set; }
		public required int Hours { get; set; }
		public required string CourseCode { get; set; }
	}
}
