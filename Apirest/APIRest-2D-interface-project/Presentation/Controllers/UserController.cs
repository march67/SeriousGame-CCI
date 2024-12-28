using APIRest_2D_interface_project.Business.Services;
using APIRest_2D_interface_project.DataAccess.Context;
using APIRest_2D_interface_project.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APIRest_2D_interface_project.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;

        public UserController (ApplicationDbContext applicationDbContext, UserService userService)
        {
            _context = applicationDbContext;
            _userService = userService;
        }

        /*
        [HttpGet("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                _userService.UserRegister(userDTO);
                return Ok("succesful");
            }
            catch (Exception ex)
            {

            }
        }*/

    }
}
