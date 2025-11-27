using Courses_API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Courses_API.Controllers.V1;

[ApiController]
[Route("api/v1")]
public class RootController : ControllerBase
{

    [HttpGet(Name = "GetRootV1")]
    public IEnumerable<HateoasDataDto> Get()
    {
        ResourceDto response = new ResourceDto();

        response.Links.Add(new HateoasDataDto(
            Url: Url.Link("GetRootV1", null)!,
            Description: "Self",
            Method: "GET"));

        response.Links.Add(new HateoasDataDto(
            Url: Url.Link("GetAllCourses", null)!,
            Description: "Get all courses",
            Method: "GET"));

        response.Links.Add(new HateoasDataDto(
            Url: Url.Link("CreateCourse", null)!,
            Description: "Create a new course",
            Method: "POST"));

        return response.Links;
    }
}