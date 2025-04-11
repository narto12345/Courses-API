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
		}
	}
}
