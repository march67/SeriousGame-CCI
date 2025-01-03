using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Presentation.DTOs;
using AutoMapper;

namespace APIRest_2D_interface_project.Infrastructure.Mappings.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
