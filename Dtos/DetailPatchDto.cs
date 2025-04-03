namespace Courses_API.Dtos
{
	public class DetailPatchDto
	{
		public required string Email { get; set; }
		public required string Address { get; set; }
		public required DateOnly Birthdate { get; set; }
	}
}
