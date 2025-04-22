using AutoMapper;
using Courses_API.Dtos;
using Courses_API.Models;
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
	}
}
