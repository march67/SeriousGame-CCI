namespace APIRest_2D_interface_project.Infrastructure.Services.Interfaces
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }

}
