using APIRest_2D_interface_project.Business.Services.Interfaces;
using APIRest_2D_interface_project.DataAccess.Context;
using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Infrastructure.Services.Interfaces;
using APIRest_2D_interface_project.Presentation.DTOs.AuthentificationDTOs.Request;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIRest_2D_interface_project.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserController (ApplicationDbContext applicationDbContext, IUserService userService, IMapper mapper, ITokenService tokenService)
        {
            _context = applicationDbContext;
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequestDTO userRegisterDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userRegisterDTO);
                var result = await _userService.UserRegister(user);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
       public async Task<IActionResult> Login(UserLoginRequestDTO userLoginDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userLoginDTO);
                var result = await _userService.UserLogin(user);
                if (!result)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }

                var token = _tokenService.GenerateJwtToken(user);

                Response.Headers["Cache-Control"] = "no-store";
                Response.Headers["Pragma"] = "no-cache";

                return Ok(new
                {
                    accessToken = token,
                    tokenType = "Bearer"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
       }

        [HttpGet("testJwt")]
        public async Task<IActionResult> test()
        {
            return Ok();
        }


    }
}
