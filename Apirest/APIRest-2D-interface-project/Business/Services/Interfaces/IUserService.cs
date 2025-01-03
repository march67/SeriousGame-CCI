using APIRest_2D_interface_project.DataAccess.Repositories;
using APIRest_2D_interface_project.Domain.Entities;

namespace APIRest_2D_interface_project.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> UserRegister(User user);
    }
}
