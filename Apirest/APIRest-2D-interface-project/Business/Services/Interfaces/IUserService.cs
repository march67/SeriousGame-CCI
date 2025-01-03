using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Presentation.DTOs;

namespace APIRest_2D_interface_project.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> UserRegister(UserDTO userDTO);
    }
}
