namespace Courses_API.Models
{
	public class Detail
	{
		public int Id { get; set; }
		public string? Email { get; set; }
		public string? Address { get; set; }
		public DateOnly? Birthdate { get; set; }
		public required int UserFk { get; set; }
		public required User User { get; set; }
	}
}
