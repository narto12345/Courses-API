using AutoMapper;
using Courses_API.Dtos;
using Courses_API.Models;
using Courses_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Courses_API.Controllers
{

	[ApiController]
	[Route("/api/users")]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly ApplicationDbContext _contextDb;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly SignInManager<IdentityUser> _signInManager;

		public UserController(
			ApplicationDbContext applicationDbContext,
			IMapper mapper,
			UserManager<IdentityUser> userManager,
			IConfiguration configuration,
			SignInManager<IdentityUser> signInManager,
			IUserService userService)
		{
			_userManager = userManager;
			_contextDb = applicationDbContext;
			_mapper = mapper;
			_configuration = configuration;
			_signInManager = signInManager;
			_userService = userService;
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<ActionResult<AuthenticationResponseDto>> Register(UserRequestDto userRequestDto)
		{
			IdentityUser user = new()
			{
				UserName = userRequestDto.UserName,
				Email = userRequestDto.UserName
			};

			IdentityResult result = await _userManager.CreateAsync(user, userRequestDto.Password!);

			if (result.Succeeded)
			{
				AuthenticationResponseDto response = await BuildToken(userRequestDto);

				User userOrigin = new()
				{
					UserName = userRequestDto.UserName,
					Name = userRequestDto.Name,
					Lastname = userRequestDto.Lastname,
					UserIdentityId = user.Id
				};

				_contextDb.Add(userOrigin);
				await _contextDb.SaveChangesAsync();

				return response;
			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}

				return ValidationProblem();
			}
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<AuthenticationResponseDto>> Login(UserCredentialDto userCredentialDto)
		{
			IdentityUser? user = await _userManager.FindByEmailAsync(userCredentialDto.Email);

			if (user is null)
			{
				return ReturnIncorrectLogin();
			}

			Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, userCredentialDto.Password!, false);

			UserRequestDto userRequestDto = new()
			{
				Name = userCredentialDto.Email,
				UserName = userCredentialDto.Email,
				Password = null
			};

			if (result.Succeeded)
			{
				return await BuildToken(userRequestDto);
			}
			else
			{
				return ReturnIncorrectLogin();
			}
		}

		private ActionResult ReturnIncorrectLogin()
		{
			ModelState.AddModelError(string.Empty, "Login incorrecto");
			return ValidationProblem();
		}

		private async Task<AuthenticationResponseDto> BuildToken(UserRequestDto userCredentialDto)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim("email", userCredentialDto.UserName)
			};

			IdentityUser? user = await _userManager.FindByEmailAsync(userCredentialDto.UserName);
			IList<Claim> claimsDB = await _userManager.GetClaimsAsync(user!);

			claims.AddRange(claimsDB);

			SymmetricSecurityKey secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtkey"]!));
			SigningCredentials credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

			DateTime expiration = DateTime.UtcNow.AddYears(1);

			JwtSecurityToken securityToken = new JwtSecurityToken(
				issuer: null,
				audience: null,
				claims: claims,
				expires: expiration,
				signingCredentials: credentials);

			string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

			return new AuthenticationResponseDto
			{
				Token = token,
				Expiration = expiration,
			};
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IEnumerable<UserDto>> Get()
		{
			List<User> users = await _contextDb.Users
											   .Include(include => include.Detail)
											   .ToListAsync();

			List<UserDto> usersDto = _mapper.Map<List<UserDto>>(users);
			return usersDto;
		}

		[AllowAnonymous]
		[HttpGet("{id:int}", Name = "ObtenerUsuario")]
		public async Task<ActionResult> Get(int id)
		{
			User? userFound = await _contextDb.Users
											  .Include(include => include.Detail)
											  .FirstOrDefaultAsync(x => x.Id == id);

			if (userFound is null)
			{
				return NotFound();
			}

			UserDto userDto = _mapper.Map<UserDto>(userFound);

			return Ok(userDto);
		}

		//[HttpPost]
		//public async Task<ActionResult> Post([FromBody] UserRequestDto userRequestDto)
		//{
		//	User user = _mapper.Map<User>(userRequestDto);

		//	_contextDb.Add(user);
		//	await _contextDb.SaveChangesAsync();

		//	UserDto userDto = _mapper.Map<UserDto>(user);

		//	return CreatedAtRoute("ObtenerUsuario", new { id = user.Id }, userDto);
		//}

		[HttpPatch("{id:int}")]
		public async Task<ActionResult> Patch(int id, JsonPatchDocument<UserPatchDto> patchDocument)
		{
			if (patchDocument is null)
			{
				return BadRequest();
			}

			User? userFound = await _contextDb.Users.FirstOrDefaultAsync(x => x.Id == id);

			if (userFound is null)
			{
				return NotFound();
			}

			UserPatchDto userPatchDtoDb = _mapper.Map<UserPatchDto>(userFound);
			patchDocument.ApplyTo(userPatchDtoDb, ModelState);
			bool isValid = TryValidateModel(userPatchDtoDb);

			if (!isValid)
			{
				return ValidationProblem();
			}

			_mapper.Map(userPatchDtoDb, userFound);
			await _contextDb.SaveChangesAsync();
			return NoContent();
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

			return NoContent();
		}
	}
}
