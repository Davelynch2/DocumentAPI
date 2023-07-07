using AutoMapper;
using DocumentAPI.DTO;
using DocumentAPI.Models;

namespace DocumentAPI
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserForCreationDto, User>();

			CreateMap<User, UserDto>()
				.ForMember("FullName", opt => opt.MapFrom(x => string.Join(' ', x.LastName, x.FirstName)));

			CreateMap<EventForCreationDto, Event>();

			CreateMap<Event, EventDto>();

			CreateMap<FileModel, FileDto>();
		}
	}
}
