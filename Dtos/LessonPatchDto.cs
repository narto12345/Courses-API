namespace Courses_API.Dtos
{
	public class LessonPatchDto
	{
		public required string Name { get; set; }
		public required string InstructorName { get; set; }
		public required int Hours { get; set; }
	}
}
