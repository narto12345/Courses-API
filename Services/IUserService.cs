using Microsoft.AspNetCore.Identity;

namespace Courses_API.Services
{
	public interface IUserService
	{
		Task<IdentityUser?> GetUser();
	}
}