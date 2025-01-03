using APIRest_2D_interface_project.Business.Services.Interfaces;
using APIRest_2D_interface_project.DataAccess.Context;
using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Presentation.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIRest_2D_interface_project.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController (ApplicationDbContext applicationDbContext, IUserService userService, IMapper mapper)
        {
            _context = applicationDbContext;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _userService.UserRegister(user);

                // Convert model to DTO for result
                var registeredUserDTO = _mapper.Map<UserDTO>(result);

                // Return with status code 201 Created
                return CreatedAtAction(
                    nameof(Register),
                    new { id = result.Id },
                    registeredUserDTO
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
