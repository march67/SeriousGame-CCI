using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Business.Services.Interfaces;
using APIRest_2D_interface_project.DataAccess.Repositories.Interfaces;
using APIRest_2D_interface_project.Infrastructure.Services.Interfaces;
using AutoMapper;
using APIRest_2D_interface_project.Presentation.DTOs.AuthentificationDTOs.Request;

namespace APIRest_2D_interface_project.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHashingService passwordHashingService, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
            _mapper = mapper;
        }

        public async Task<User> UserRegister(User user)
        {
            return await _userRepository.RegisterUser(user);
        }
    }
}
