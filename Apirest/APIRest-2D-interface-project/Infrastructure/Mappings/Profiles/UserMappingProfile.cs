using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Infrastructure.Mappings.Resolvers;
using APIRest_2D_interface_project.Infrastructure.Services.Interfaces;
using APIRest_2D_interface_project.Presentation.DTOs.AuthentificationDTOs.Request;
using APIRest_2D_interface_project.Presentation.DTOs.AuthentificationDTOs.Response;
using AutoMapper;


namespace APIRest_2D_interface_project.Infrastructure.Mappings.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterRequestDTO, User>()
                .ForMember(
                    destination => destination.PasswordHash,
                    option => option.MapFrom<PasswordHashResolver>()
                );
            CreateMap<User, UserRegisterResponseDTO>();
        }
    }
}
