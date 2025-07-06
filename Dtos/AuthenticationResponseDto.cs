namespace Courses_API.Dtos
{
	public class AuthenticationResponseDto
	{
		public required string Token { get; set; }
		public DateTime Expiration { get; set; }
	}
}
