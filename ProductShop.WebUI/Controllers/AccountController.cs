using Microsoft.AspNetCore.Mvc;
using ProductShop.WebUI.Integrations.ProductShopApi.Models;
using ProductShop.WebUI.Integrations.ProductShopApi.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProductShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            var result = await AccountService.RegisterUser(user);
            if (result.IsSuccessStatusCode)
            {
                RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("", "user already exists.");

                return View();
            }
            return RedirectToAction("Products", "Shop");
        }

        [HttpPost]
        public async Task<IActionResult>  Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            var user = await AccountService.Login(loginModel);
            if (user!=null && user.IsClient)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("FullName", $"Hi! {user.FirstName} {user.LastName}");

               
                return  RedirectToAction("Products", "Shop");
            }
            else
            {
                ModelState.AddModelError("Login Failed","Invalid user name or password.");
                return View();

            }
        }
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");

        }
    }
}
