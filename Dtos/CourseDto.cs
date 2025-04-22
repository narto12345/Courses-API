namespace Courses_API.Dtos
{
	public class CourseDto
	{
		public required int Id { get; set; }
		public required string Code { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public required List<LessonDto> Lessons { get; set; }
	}
}
