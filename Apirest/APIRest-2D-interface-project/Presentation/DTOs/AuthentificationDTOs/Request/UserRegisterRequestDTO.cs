using System.ComponentModel.DataAnnotations;

namespace APIRest_2D_interface_project.Presentation.DTOs.AuthentificationDTOs.Request
{
    public class UserRegisterRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 30 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 20 characters")]
        public string Password { get; set; }
    }
}
