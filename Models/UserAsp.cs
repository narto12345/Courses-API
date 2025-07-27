using Microsoft.AspNetCore.Identity;

namespace Courses_API.Models;

public class UserAsp: IdentityUser
{
	public DateTime Birthday { get; set; }
}
