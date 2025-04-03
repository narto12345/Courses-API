using AutoMapper;
using Courses_API.Dtos;
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
		private readonly IMapper _mapper;
		public UserController(ApplicationDbContext applicationDbContext, IMapper  mapper)
		{
			_contextDb = applicationDbContext;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IEnumerable<UserDto>> Get()
		{
			List<User> users = await _contextDb.Users.ToListAsync();
			List<UserDto> usersDto = _mapper.Map<List<UserDto>>(users);
			return usersDto;
		}

		[HttpGet("{id:int}", Name = "ObtenerUsuario")]
		public async Task<ActionResult> Get(int id)
		{
			User? userFound = await _contextDb.Users.FirstOrDefaultAsync(x => x.Id == id);

			if (userFound is null)
			{
				return NotFound();
			}

			UserDto userDto = _mapper.Map<UserDto>(userFound);

			return Ok(userDto);
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] User user)
		{
			_contextDb.Add(user);
			await _contextDb.SaveChangesAsync();

			UserDto userDto = _mapper.Map<UserDto>(user);

			return CreatedAtRoute("ObtenerUsuario", new { id = user.Id }, userDto);
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
