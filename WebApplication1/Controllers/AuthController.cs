using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.User;
using WebApplication1.Models;
using WebApplication1.Services.AuthService;
using WebApplication1.Strings;
using DataLibrary.Models;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService; 
        }

        [HttpPost("register")]
        public async Task <IActionResult> Register(UserRegisterDto userRegister)
        {
            ServiceResponse<string> resposne = await _authService.Register(
                new Users { Name = userRegister.Name, UserName = userRegister.username, Email = userRegister.email }, userRegister.password 
                );
            if(!resposne.success)
            {
                return BadRequest(resposne);
            }
            else
            {
                
                return Ok(resposne);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            ServiceResponse<string> resposne = await _authService.Login(
                userLogin.email, userLogin.password);
            if (!resposne.success)
            {
                return BadRequest(resposne);
            }
            else
            {
                return Ok(resposne);
            }
        }
        [HttpGet]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail( [FromQuery] string id, [FromQuery]string token)
        {
            if (id == null || token == null)
            {
                return NotFound();
            }
            ServiceResponse<string> response = await _authService.ConfirmEmail(id, token);
            
            if (response.success)
            {
                return Ok(ErrorStrings.Thanks);
            }
            else
            {
                return BadRequest(ErrorStrings.EmailConfirmationFailed);
            }
        }
    }
}
