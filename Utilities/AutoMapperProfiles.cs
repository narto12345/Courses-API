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

			CreateMap<Detail, DetailDto>();
		}
	}
}
