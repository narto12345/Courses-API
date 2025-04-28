namespace Courses_API.Dtos
{
	public class CourseWithUsersDto
	{
		public required int Id { get; set; }
		public required string Code { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public required List<UserDto> Users { get; set; }
	}
}
