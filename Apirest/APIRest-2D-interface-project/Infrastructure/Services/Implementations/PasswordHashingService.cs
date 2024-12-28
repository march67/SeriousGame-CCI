using APIRest_2D_interface_project.Infrastructure.Services.Interfaces;

namespace APIRest_2D_interface_project.Infrastructure.Services.Implementations
{

    public class PasswordHashingService : IPasswordHashingService
    {
        public string HashPassword(string password)
        {
            return BC.HashPassword(password, BC.GenerateSalt(12));
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
