using AutoMapper;
using FormEODY.DataAccess.Entities;
using FormEODY.Web.Models;

namespace FormEODY.Web.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Application, ApplicationViewModel>()
                .ForMember(dest => dest.OccupationName, opt => opt.MapFrom(src => src.Occupation != null ? src.Occupation.Name : string.Empty))
                .ReverseMap();
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
