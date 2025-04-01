using Courses_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Controllers
{

	[ApiController]
	[Route("/api/users")]
	public class UserController : ControllerBase
	{
		private readonly ApplicationDbContext _contextDb;
		public UserController(ApplicationDbContext applicationDbContext)
		{
			_contextDb = applicationDbContext;
		}

		[HttpGet]
		public async Task<IEnumerable<User>> Get()
		{
			return await _contextDb.Users.ToListAsync();
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult> Get(int id)
		{
			User? userFound = await _contextDb.Users.FirstOrDefaultAsync(x => x.Id == id);

			if (userFound is null)
			{
				return NotFound();
			}

			return Ok(userFound);
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] User user)
		{
			_contextDb.Add(user);
			await _contextDb.SaveChangesAsync();
			return Created();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			int registersDeleted = await _contextDb.Users.Where(user => user.Id == id)
														 .ExecuteDeleteAsync();

			if (registersDeleted == 0)
			{
				return NotFound();
			}

			return Ok();
		}
	}
}
