using Courses_API.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Courses_API.Services;

public class UserService : IUserService
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UserService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
	{
		this._userManager = userManager;
		this._httpContextAccessor = httpContextAccessor;
	}

	public async Task<IdentityUser?> GetUser()
	{
		System.Security.Claims.Claim? emailClaim = _httpContextAccessor.HttpContext!.User.Claims.Where(item => item.Value == "email").FirstOrDefault();

		if (emailClaim is null)
		{
			return null;
		}

		string email = emailClaim.Value;
		return await _userManager.FindByEmailAsync(email);
	}
}
