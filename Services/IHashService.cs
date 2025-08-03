using Courses_API.Dtos;

namespace Courses_API.Services
{
	public interface IHashService
	{
		HashResultDto Hash(string input);
		HashResultDto Hash(string input, byte[] sal);
	}
}