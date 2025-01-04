using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Infrastructure.Services.Interfaces;
using APIRest_2D_interface_project.Presentation.DTOs.AuthentificationDTOs.Request;
using AutoMapper;

namespace APIRest_2D_interface_project.Infrastructure.Mappings.Resolvers
{
    public class PasswordHashResolver : IValueResolver<UserRegisterRequestDTO, User, string>
    {
        private readonly IPasswordHashingService _passwordHashingService;

        public PasswordHashResolver(IPasswordHashingService passwordHashingService)
        {
            _passwordHashingService = passwordHashingService;
        }

        public string Resolve(UserRegisterRequestDTO source, User destination, string destinationMember, ResolutionContext context)
        {
            return _passwordHashingService.HashPassword(source.Password);
        }
    }
}
