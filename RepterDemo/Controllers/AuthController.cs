using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepterDemo.DTO;
using RepterDemo.Models;
using RepterDemo.Repositories;

namespace RepterDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("getusers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            var users = await _authService.GetUsersAsync(); 

            return Ok(users);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _authService.RegisterUserAsync(model);

            if (!(bool)result.Success)  // Check the success flag
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var loginResult = await _authService.LoginUserAsync(model);

            if (loginResult == null)
            {
                return Unauthorized("Invalid username or password");
            }

            // Return both token and userId
            return Ok(new
            {
                token = loginResult.Token,
                userId = loginResult.UserId
            });
        }

        [HttpGet("protected-resource")]
        public IActionResult GetProtectedResource()
        {
            return Ok("This is a protected resource.");
        }

    }

}
