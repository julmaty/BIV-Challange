using BIV_Challange.Models;
using BIV_Challange.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BIV_Challange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        ApplicationContext _context;

        public UserController(
            UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterAsync(RegisterRequestModel request)
        {
            User user = new User { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            await _signInManager.SignInAsync(user, true);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginAsync(LoginRequestModel request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);
            var result =
                    await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe = true, false);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<ActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpGet]
        [Route("UserName")]
        public async Task<ActionResult> UserName()
        {
            var user = HttpContext.User.Identity;
            if (user is not null && user.IsAuthenticated)
            {
                return Ok(user.Name);
            }
            else
            {
                return Ok("not auth");
            }
        }
    }
}
