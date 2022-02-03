using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_with_Swagger.Models;
using Shop_with_Swagger.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_with_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPut]
        public async Task<string> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = model.Email;
                User user = new User { Email = email, UserName = email };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Buyer");
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    return "The user registered";
                }
            }
            return "The user didn't register";
        }

        [HttpPost]
        public async Task<string> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return "The user is logged in";
            }
            return "The user isn't logged in";
        }

        [HttpDelete]
        [Authorize]
        public async Task<string> Logout()
        {
            await _signInManager.SignOutAsync();
            return "Logout";
        }
    }
}
