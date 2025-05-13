using AutoMapper;
using Courses_API.Dtos;
using Courses_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Controllers
{
	[ApiController]
	[Route("/api/courses")]
	public class CourseController : ControllerBase
	{
		private readonly ApplicationDbContext _contextDb;
		private readonly IMapper _mapper;
		public CourseController(ApplicationDbContext applicationDbContext, IMapper mapper)
		{
			_contextDb = applicationDbContext;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IEnumerable<CourseDto>> Get()
		{
			List<Course> courses = await _contextDb.Courses
											   .Include(include => include.Lessons)
											   .ToListAsync();

			List<CourseDto> coursesDto = _mapper.Map<List<CourseDto>>(courses);
			return coursesDto;
		}

		[HttpGet("{id:int}", Name = "ObtenerCurso")]
		public async Task<ActionResult> Get(int id)
		{
			Course? courseFound = await _contextDb.Courses
										.Include(include => include.Lessons)
										.FirstOrDefaultAsync(x => x.Id == id);

			if (courseFound == null)
			{
				return NotFound();
			}

			CourseDto courseDto = _mapper.Map<CourseDto>(courseFound);

			return Ok(courseDto);
		}

		[HttpGet("{id:int}/users", Name = "ObtenerCursoConUsuarios")]
		public async Task<ActionResult> GetCourseWithUsers(int id)
		{
			Course? courseFound = await _contextDb.Courses
							.FirstOrDefaultAsync(x => x.Id == id);

			if (courseFound is null)
			{
				return NotFound();
			}

			List<UserCourse>? usersCoursesFound = await _contextDb.UsersCourses
										.Include(userCourse => userCourse.User!.Detail)
										.Where(userCourse => userCourse.CourseId == id)
										.ToListAsync();

			List<UserDto> usersDto = _mapper.Map<List<UserDto>>(usersCoursesFound.Select(item => item.User).ToList());

			return Ok(new CourseWithUsersDto
			{
				Id = id,
				Code = courseFound.Code!,
				Name = courseFound.Name!,
				Description = courseFound.Description!,
				Users = usersDto
			});
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CourseRequestDto courseRequestDto)
		{
			Course course = _mapper.Map<Course>(courseRequestDto);

			course.Code = Guid.NewGuid().ToString();

			_contextDb.Courses.Add(course);
			await _contextDb.SaveChangesAsync();

			CourseDto courseDto = _mapper.Map<CourseDto>(course);

			return CreatedAtRoute("ObtenerCurso", new { id = course.Id}, courseDto);
		}

		[HttpPost("{courseId:int}/users/{userId:int}")]
		public async Task<ActionResult> AddUser(int courseId, int userId)
		{
			Course? courseFound = await _contextDb.Courses
				.FirstOrDefaultAsync(x => x.Id == courseId);

			if (courseFound is null)
			{
				return NotFound();
			}

			User? userFound = await _contextDb.Users
								  .FirstOrDefaultAsync(x => x.Id == userId);

			if (userFound is null)
			{
				return NotFound();
			}

			bool userCourseExist = await _contextDb.UsersCourses.AnyAsync(x => x.UserId == userId && x.CourseId == courseId);

			if (userCourseExist)
			{
				return BadRequest("El usuario ya se encuentra inscrito en el curso");
			}

			UserCourse course = new UserCourse
			{
				CourseId = courseId,
				UserId = userId
			};

			_contextDb.UsersCourses.Add(course);
			await _contextDb.SaveChangesAsync();

			return CreatedAtRoute("ObtenerCursoConUsuarios", new { id = courseId }, new CourseWithUsersDto
			{
				Id = courseFound.Id,
				Code = courseFound.Name!,
				Name = courseFound.Name!,
				Description = courseFound.Description!,
				Users = [new UserDto {
					Id = userFound.Id,
					FullName = $"{userFound.Name} {userFound.Lastname}",
					UserName = userFound.UserName!,
					Detail = null
				}]
			});
		}

		[HttpPatch("{id:int}")]
		public async Task<ActionResult> Patch(int id, JsonPatchDocument<CoursePathDto> patchDocument)
		{
			if (patchDocument is null)
			{
				return BadRequest();
			}

			Course? courseFound = await _contextDb.Courses
										.Include(include => include.Lessons)
										.FirstOrDefaultAsync(x => x.Id == id);

			if (courseFound is null)
			{
				return NotFound();
			}

			CoursePathDto coursePathDto = _mapper.Map<CoursePathDto>(courseFound);
			patchDocument.ApplyTo(coursePathDto, ModelState);
			bool isValid = TryValidateModel(coursePathDto);

			if (!isValid)
			{
				return ValidationProblem();
			}

			_mapper.Map(coursePathDto, courseFound);
			await _contextDb.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			int registersDeleted = await _contextDb.Courses.Where(course => course.Id == id)
														   .ExecuteDeleteAsync();

			if (registersDeleted == 0)
			{
				return NotFound();
			}

			return NoContent();
		}

		[HttpDelete("{courseId:int}/users/{userId:int}")]
		public async Task<ActionResult> DeleteUser(int courseId, int userId)
		{
			int registersDeleted = await _contextDb.UsersCourses.Where(userCourse => userCourse.UserId == userId && userCourse.CourseId == courseId)
																.ExecuteDeleteAsync();

			if (registersDeleted == 0)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
