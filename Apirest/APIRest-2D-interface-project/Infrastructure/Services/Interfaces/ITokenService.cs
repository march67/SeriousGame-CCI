using APIRest_2D_interface_project.Domain.Entities;

namespace APIRest_2D_interface_project.Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
