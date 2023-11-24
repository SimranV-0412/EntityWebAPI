using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Project.Models;
using static WebAPI_Project.DAL.MyAppDBContext;

namespace WebAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // Handle user registration logic using UserManager
            // Example: Creating a new user
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // User registered successfully
                return Ok("User registered successfully.");
            }
            else
            {
                // Registration failed, return error messages
                return BadRequest(result.Errors);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // Handle user login logic using SignInManager
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //var token = GenerateJwtToken(model.Email);
                // User logged in successfully
                return Ok("User logged in successfully.");
            }
            else
            {
                // Login failed
                return BadRequest("Invalid login attempt.");
            }
        }
    }
}
