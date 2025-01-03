using APIRest_2D_interface_project.Domain.Entities;

namespace APIRest_2D_interface_project.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
       Task<User> RegisterUser(User user);
    }
}
