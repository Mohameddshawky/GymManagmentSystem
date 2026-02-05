using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.AccountViewModel;
using GymManagmentDAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountService accountService,SignInManager<ApplicationUser> signInManager)
        {
            this.accountService = accountService;
            this.signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await accountService.ValidateUserAsync(model);
            if (user == null)
            {
                ModelState.AddModelError("Invalid Login", "Invalid login attempt.");
                return View(model);
            }
            // Sign in the user
             var res= await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (res.IsNotAllowed) ModelState.AddModelError("Invalid Login", "Your Account Is Not Allowed");
            if(res.Succeeded) return RedirectToAction("Index", "Home");

            return View(model);                                         
        }

    }
}
