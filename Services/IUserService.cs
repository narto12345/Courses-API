using Courses_API.Models;
using Microsoft.AspNetCore.Identity;

namespace Courses_API.Services
{
	public interface IUserService
	{
		Task<UserAsp?> GetUser();
	}
}