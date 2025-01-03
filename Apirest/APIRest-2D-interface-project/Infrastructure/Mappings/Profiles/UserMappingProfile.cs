using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Infrastructure.Services.Interfaces;
using APIRest_2D_interface_project.Presentation.DTOs;
using AutoMapper;

namespace APIRest_2D_interface_project.Infrastructure.Mappings.Profiles
{
    public class UserMappingProfile : Profile
    {
        private readonly IPasswordHashingService _passwordHashingService;
        public UserMappingProfile(IPasswordHashingService passwordHashingService)
        {
            _passwordHashingService = passwordHashingService;

            CreateMap<UserDTO, User>()
                .ForMember(destination => destination.PasswordHash,
                          option => option.MapFrom(source => _passwordHashingService.HashPassword(source.Password)));

            CreateMap<User, UserDTO>();
        }
    }
}
