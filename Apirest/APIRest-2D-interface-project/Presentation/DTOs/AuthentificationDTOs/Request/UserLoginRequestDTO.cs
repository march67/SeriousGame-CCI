using System.ComponentModel.DataAnnotations;

namespace APIRest_2D_interface_project.Presentation.DTOs.AuthentificationDTOs.Request
{
    public class UserLoginRequestDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
