using System.ComponentModel.DataAnnotations;

namespace Courses_API.Dtos
{
	public class CourseRequestWithPhotoDto : CourseRequestDto
	{
		public IFormFile? Photo { get; set; }
	}
}