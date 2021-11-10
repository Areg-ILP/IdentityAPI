using AutoMapper;
using Identity.Domain.Entities;
using Identity.Infastructure.Application.Models.DetailsModels;

namespace Identity.Infastructure.Application.Utilities.AutoMapper
{
    public class MapperProfile :  Profile
    {
        public MapperProfile()
        {
            //Entity -> DetailModel
            CreateMap<User, UserDetailsModel>();
            CreateMap<Role, RoleDetailsModel>();

            //DetailModel -> Entity
            CreateMap<UserDetailsModel, User>();
            CreateMap<RoleDetailsModel, Role>();
        }
    }
}
