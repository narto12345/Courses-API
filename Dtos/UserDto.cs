using Courses_API.Models;

namespace Courses_API.Dtos
{
	public class UserDto
	{
		public required int Id { get; set; }
		public required string UserName { get; set; }
		public required string FullName { get; set; }
		public required DetailDto? Detail { get; set; }
	}
}
