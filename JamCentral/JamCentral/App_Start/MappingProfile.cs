using AutoMapper;
using JamCentral.Dtos;
using JamCentral.Models;
using JamCentral.Models.NotificationFeed;

namespace JamCentral.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, ApplicationUserDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();
        }
        
    }
}