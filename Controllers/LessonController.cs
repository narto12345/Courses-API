using AutoMapper;
using Courses_API.Dtos;
using Courses_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Controllers
{

	[ApiController]
	[Route("/api/lessons")]
	public class LessonController : ControllerBase
	{
		private readonly ApplicationDbContext _contextDb;
		private readonly IMapper _mapper;
		public LessonController(ApplicationDbContext applicationDbContext, IMapper mapper)
		{
			_contextDb = applicationDbContext;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<List<LessonDto>> Get()
		{
			List<Lesson> lessonsFound = await _contextDb.Lessons
														.Include(lesson => lesson.Course)
														.ToListAsync();
			List<LessonDto> lessonsDto = _mapper.Map<List<LessonDto>>(lessonsFound);
			return lessonsDto;
		}

		[HttpGet("{id:int}", Name = "ObtenerLeccionDeCurso")]
		public async Task<ActionResult> Get(int id)
		{
			Lesson? lessonFound = await _contextDb.Lessons
												  .Include(lesson => lesson.Course)
												  .FirstOrDefaultAsync(x => x.Id == id);

			if (lessonFound == null)
			{
				return NotFound();
			}

			LessonDto lessonDto = _mapper.Map<LessonDto>(lessonFound);

			return Ok(lessonDto);
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] LessonRequestDto lessonRequestDto)
		{
			bool existCourse = await _contextDb.Courses.AnyAsync(course => course.Id == lessonRequestDto.CourseId);

			if (!existCourse)
			{
				return BadRequest($"El curso con id '{lessonRequestDto.CourseId}' no existe");
			}

			Lesson lesson = _mapper.Map<Lesson>(lessonRequestDto);

			_contextDb.Add(lesson);
			await _contextDb.SaveChangesAsync();

			Lesson? lessonFound = await _contextDb.Lessons
												  .Include(l => l.Course)
												  .FirstOrDefaultAsync(x => x.Id == lesson.Id);

			LessonDto lessonDto = _mapper.Map<LessonDto>(lessonFound);

			return CreatedAtRoute("ObtenerLeccionDeCurso", new { id = lesson.Id }, lessonDto);
		}

		[HttpPatch("{id:int}")]
		public async Task<ActionResult> Patch(int id, JsonPatchDocument<LessonPatchDto> patchDocument)
		{
			if (patchDocument is null)
			{
				return BadRequest();
			}

			Lesson? lessonFound = await _contextDb.Lessons.FirstOrDefaultAsync(x => x.Id == id);

			if (lessonFound == null)
			{
				return NotFound();
			}

			LessonPatchDto lessonPatchDto = _mapper.Map<LessonPatchDto>(lessonFound);
			patchDocument.ApplyTo(lessonPatchDto, ModelState);
			bool isValid = TryValidateModel(lessonPatchDto);

			if (!isValid)
			{
				return ValidationProblem();
			}

			_mapper.Map(lessonPatchDto, lessonFound);
			await _contextDb.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			int registersDeleted = await _contextDb.Lessons.Where(lesson => lesson.Id == id)
														   .ExecuteDeleteAsync();

			if (registersDeleted == 0)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
