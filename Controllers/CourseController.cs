using AutoMapper;
using Courses_API.Dtos;
using Courses_API.Models;
using Courses_API.Services;
using Courses_API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq.Dynamic.Core;

namespace Courses_API.Controllers
{
    [ApiController]
    [Route("/api/courses")]
    [HeaderAggregationFilter("Controller", "CourseController")]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _contextDb;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly ILogger<CourseController> _logger;
        private readonly IOutputCacheStore _outputCacheStore;
        private const string container = "courses";

        private const string cache = "get-courses";
        public CourseController(ApplicationDbContext applicationDbContext, IMapper mapper, IFileStorage fileStorage, ILogger<CourseController> logger, IOutputCacheStore outputCacheStore)
        {
            _contextDb = applicationDbContext;
            _mapper = mapper;
            _fileStorage = fileStorage;
            _logger = logger;
            _outputCacheStore = outputCacheStore;
        }

        [HttpGet]
        [EndpointSummary("1.1 Obtiene todos los cursos")]
        [EndpointDescription("Obtiene todos los cursos disponibles del sistema")]
        //[OutputCache(Tags = [cache])]
        [ServiceFilter<ActionFilter>()]
        [HeaderAggregationFilter("action", "GetAllCourses")]
        public async Task<IEnumerable<CourseDto>> Get([FromQuery] PaginationDto paginationDto)
        {
            //throw new Exception("Error de prueba para el filtro de acciones");
            IQueryable<Course> queryable = _contextDb.Courses.AsQueryable();
            await HttpContext.InsertPaginationParams(queryable);
            List<Course> courses = await queryable
                                               .OrderBy(order => order.Id)
                                               .Page(paginationDto)
                                               .Include(include => include.Lessons)
                                               .ToListAsync();

            List<CourseDto> coursesDto = _mapper.Map<List<CourseDto>>(courses);

            return coursesDto;
        }

        [HttpGet("Filter")]
        [AllowAnonymous]
        public async Task<ActionResult> Filter([FromQuery] CourseFilterDto paginationDto)
        {
            IQueryable<Course> queryable = _contextDb.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(paginationDto.Name))
            {
                queryable = queryable.Where(x => x.Name!.Contains(paginationDto.Name));
            }

            if (!string.IsNullOrEmpty(paginationDto.Description))
            {
                queryable = queryable.Where(x => x.Description!.Contains(paginationDto.Description));
            }

            if (paginationDto.IncludeLessons)
            {
                queryable = queryable.Include(x => x.Lessons);
            }

            if (paginationDto.IsFoto.HasValue)
            {
                if (paginationDto.IsFoto.Value)
                {
                    queryable = queryable.Where(x => x.Foto != null);
                }
                else
                {
                    queryable = queryable.Where(x => x.Foto == null);
                }
            }

            if (paginationDto.IsLessons.HasValue)
            {
                if (paginationDto.IsLessons.Value)
                {
                    queryable = queryable.Where(x => x.Lessons!.Any());
                }
                else
                {
                    queryable = queryable.Where(x => !x.Lessons!.Any());
                }
            }

            if (!string.IsNullOrEmpty(paginationDto.Lessons))
            {
                queryable = queryable.Where(x => x.Lessons!.Any(item => item.Name!.Contains(paginationDto.Lessons)));
            }

            if (!string.IsNullOrEmpty(paginationDto.SortFile))
            {
                var ordenType = paginationDto.AscendingOrder ? "ascending" : "descending";

                try
                {
                    queryable = queryable.OrderBy($"{paginationDto.SortFile} {ordenType}");
                }
                catch (Exception ex)
                {
                    queryable = queryable.OrderBy(x => x.Id);
                    _logger.LogError(ex.Message, ex);
                }
            }
            else
            {
                queryable = queryable.OrderBy(x => x.Id);
            }

            List<Course> courses = await queryable
                                               .Page(paginationDto.PaginationDto)
                                               .ToListAsync();

            List<CourseDto> coursesDto = _mapper.Map<List<CourseDto>>(courses);
            return Ok(coursesDto);
        }


        [HttpGet("{id:int}", Name = "ObtenerCurso")]
        [EndpointSummary("1.2 Obtiene un curso por Id")]
        [EndpointDescription("Obtiene un curso filtrado por un Id único")]
        [AllowAnonymous]
        [ProducesResponseType<CourseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[OutputCache(Tags = [cache])]
        public async Task<ActionResult<CourseDto>> Get([Description("El id del curso")] int id)
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] CourseRequestDto courseRequestDto)
        {
            Course course = _mapper.Map<Course>(courseRequestDto);

            course.Code = Guid.NewGuid().ToString();

            _contextDb.Courses.Add(course);
            await _contextDb.SaveChangesAsync();
            await _outputCacheStore.EvictByTagAsync(cache, default);
            CourseDto courseDto = _mapper.Map<CourseDto>(course);

            return CreatedAtRoute("ObtenerCurso", new { id = course.Id }, courseDto);
        }

        [HttpPost("storage")]
        [Authorize]
        public async Task<ActionResult> PostWithFile([FromForm] CourseRequestWithPhotoDto courseRequestDto)
        {
            Course course = _mapper.Map<Course>(courseRequestDto);

            if (courseRequestDto.Photo is not null)
            {
                string url = await _fileStorage.Store(container, courseRequestDto.Photo);
                course.Foto = url;
            }

            course.Code = Guid.NewGuid().ToString();

            _contextDb.Courses.Add(course);
            await _contextDb.SaveChangesAsync();
            await _outputCacheStore.EvictByTagAsync(cache, default);
            CourseDto courseDto = _mapper.Map<CourseDto>(course);

            return CreatedAtRoute("ObtenerCurso", new { id = course.Id }, courseDto);
        }

        [HttpPost("{courseId:int}/users/{userId:int}")]
        [Authorize]
        public async Task<ActionResult> AddUser(int courseId, int userId)
        {
            Course? courseFound = await _contextDb.Courses
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (courseFound is null)
            {
                return NotFound();
            }

            User? userFound = await _contextDb.Users
                                  .Include(x => x.Detail)
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

            DetailDto? userDetail = null;
            if (userFound.Detail is not null)
            {
                userDetail = _mapper.Map<DetailDto>(userFound.Detail);
            }

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
                    Detail = userDetail
                }]
            });
        }

        [HttpPatch("{id:int}")]
        [Authorize]
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
            await _outputCacheStore.EvictByTagAsync(cache, default);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Policy = "isadmin")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(int id)
        {
            Course? course = await _contextDb.Courses.FirstOrDefaultAsync(x => x.Id == id);

            if (course is null)
            {
                return NotFound();
            }

            course.IsDeleted = true;

            await _contextDb.SaveChangesAsync();
            _contextDb.Courses.Update(course);
            await _fileStorage.Delete(course.Foto, container);
            await _outputCacheStore.EvictByTagAsync(cache, default);
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
