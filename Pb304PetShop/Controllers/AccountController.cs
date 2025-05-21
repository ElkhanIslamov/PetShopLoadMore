using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pb304PetShop.Models;

namespace Pb304PetShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View();
            }
            var user = new AppUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                FullName = registerViewModel.Fullname
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password); 

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View();
            }
            var user = await _userManager.FindByNameAsync(loginViewModel.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
            if(!result.IsLockedOut)
            {
                ModelState.AddModelError("", "User is locked out");
                return View();
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "You are bannaed");
                
            }
            if (loginViewModel.ReturnUrl != null)
            {
                return LocalRedirect(loginViewModel.ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }
    }
}
