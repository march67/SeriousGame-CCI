using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Business.Services.Interfaces;
using APIRest_2D_interface_project.DataAccess.Repositories.Interfaces;

namespace APIRest_2D_interface_project.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> UserRegister(User user)
        {
            return await userRepository.RegisterUser(user);
        }
    }
}
