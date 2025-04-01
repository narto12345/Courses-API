using Courses_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers
{

	[ApiController]
	[Route("/api/users")]
	public class UserController : ControllerBase
	{

		[HttpGet]
		public IEnumerable<User> Get()
		{
			return new List<User>()
			{
				new User
				{
					Id = 1,
					Name = "Nicolas",
					Lastname = "Sosa",
					UserName = "nicosan"
				},
				new User
				{
					Id = 2,
					Name = "Santiago",
					Lastname = "Jimenez",
					UserName = "santi"
				}
			};
		}
	}
}
