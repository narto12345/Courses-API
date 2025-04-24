using AutoMapper;
using Courses_API.Dtos;
using Courses_API.Models;

namespace Courses_API.Utilities
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<User, UserDto>()
				.ForMember(dto => dto.FullName,
						   config => config.MapFrom(user => $"{user.Name} {user.Lastname}"));
			CreateMap<UserRequestDto, User>();
			CreateMap<User, UserPatchDto>().ReverseMap();

			CreateMap<Detail, DetailDto>();
			CreateMap<DetailRequestDto, Detail>();
			CreateMap<Detail, DetailPatchDto>().ReverseMap();

			CreateMap<Course, CourseDto>();
			CreateMap<CourseRequestDto, Course>();
			CreateMap<Course, CoursePathDto>().ReverseMap();

			CreateMap<Lesson, LessonDto>()
				.ForMember(dto => dto.CourseCode,
						   config => config.MapFrom(lesson => lesson.Course == null ? null : lesson.Course.Code));
			CreateMap<LessonRequestDto, Lesson>();
			CreateMap<Lesson, LessonPatchDto>().ReverseMap();
		}
	}
}
