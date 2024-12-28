using APIRest_2D_interface_project.DataAccess.Repositories;
using APIRest_2D_interface_project.Domain.Entities;

namespace APIRest_2D_interface_project.Business.Services
{
    public class UserService
    {
        UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void UserRegister(User user)
        {
            userRepository.RegisterUser(user);
        }
    }
}
