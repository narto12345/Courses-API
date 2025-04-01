using Courses_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Controllers
{

	[ApiController]
	[Route("/api/details")]
	public class UserDetailController : ControllerBase
	{
		private readonly ApplicationDbContext _contextDb;
		public UserDetailController(ApplicationDbContext applicationDbContext)
		{
			_contextDb = applicationDbContext;
		}

		[HttpGet]
		public async Task<IEnumerable<Detail>> Get()
		{
			return await _contextDb.Details.ToListAsync();
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult> Get(int id)
		{
			Detail? detailFound = await _contextDb.Details.FirstOrDefaultAsync(x => x.Id == id);

			if (detailFound is null)
			{
				return NotFound();
			}

			return Ok(detailFound);
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] Detail detail)
		{
			bool existUser = await _contextDb.Users.AnyAsync(user => user.Id  == detail.UserIdFk);

			if (!existUser)
			{
				return BadRequest($"El usuario con id '{detail.UserIdFk}' no existe");
			}

			bool existDetail = await _contextDb.Details.AnyAsync(detail => detail.UserIdFk == detail.UserIdFk);

			if (existDetail)
			{
				return BadRequest($"El usuario con id '{detail.UserIdFk}' ya tiene un detalle creado");
			}

			_contextDb.Add(detail);
			await _contextDb.SaveChangesAsync();
			return Created();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			int registersDeleted = await _contextDb.Details.Where(detail => detail.Id == id)
														   .ExecuteDeleteAsync();

			if (registersDeleted == 0)
			{
				return NotFound();
			}

			return Ok();
		}
	}
}
