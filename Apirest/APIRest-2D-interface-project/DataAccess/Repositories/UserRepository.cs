using APIRest_2D_interface_project.DataAccess.Context;
using APIRest_2D_interface_project.Domain.Entities;

namespace APIRest_2D_interface_project.DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly User user;
        private readonly ApplicationDbContext _context ;
        public UserRepository(ApplicationDbContext applicationDbContext ,User user)
        {
            this.user = user;
            this._context = applicationDbContext;
        }

        public void RegisterUser(User user)
        {
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
