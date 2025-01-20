using APIRest_2D_interface_project.Business.Services.Interfaces;
using APIRest_2D_interface_project.DataAccess.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIRest_2D_interface_project.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
    }
}
