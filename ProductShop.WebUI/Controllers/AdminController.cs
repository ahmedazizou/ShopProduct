using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductShop.WebUI.Integrations.ProductShop.Api.Models;
using ProductShop.WebUI.Integrations.ProductShop.Api.Services;
using ProductShop.WebUI.Integrations.ProductShopApi.Models;
using ProductShop.WebUI.Integrations.ProductShopApi.Services;

namespace ProductShop.WebUI.Controllers
{
    public class AdminController : Controller
    {
        [Route("Admin")]
        [Route("Login")]
        public IActionResult Login()
        {

            return View("LoginAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> LoginAdmin(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("LoginAdmin");

            }
            var user = await AccountService.Login(loginModel);
            if (user != null && !user.IsClient)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("FullName", $"Hi! {user.FirstName} {user.LastName}");

                
                return RedirectToAction("AllProducts", "Admin");
            }
            else
            {
                ModelState.AddModelError("Login Failed", "Invalid user name or password.");
                return View("LoginAdmin");

            }
        }
        public async Task<IActionResult> AllProducts()
        {
            if (HttpContext.Session.GetInt32("UserId") > 0)
            {
               

                var products = await ProductService.Get();
                return View("Products", products);
            }else
            {
               return RedirectToAction("Login");
            };
                
        }
        [HttpGet]
        public IActionResult AddEditProduct()
        {
            if (HttpContext.Session.GetInt32("UserId") > 0)
            {
                var product = new Product();
                return View(product);
            }
            else
            {
                return RedirectToAction("Login");

            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") > 0)
            {
                var product = await ProductService.GetProductById(id);
                return View("AddEditProduct", product);
            }
            else
            {
                return RedirectToAction("Login");

            }
           
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditProduct(Product product )
        {

            if (!ModelState.IsValid)
            {
                return View("AddEditProduct");

            }
            var img = Request.Form.Files[0];

            if (product.Id==0)
            {

                if (img != null)
                {
                    var res = await ProductService.CreateProduct(product, img);
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("AllProducts");
                    }
                }
            }
            else
            {
                
                var res = await ProductService.UpdateProduct(product, img,product.Id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("AllProducts");
                }
            }
           


            return RedirectToAction("AllProducts");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await ProductService.DeleteProduct(id);
            var products = await ProductService.Get();

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("AllProducts", products);
            }

            return View("Products", products);
        }
        public async Task<IActionResult> ManageUsers()
        {
            if (HttpContext.Session.GetInt32("UserId") > 0)
            {
                var users = await AccountService.GetAllUsers();

                return View("Users", users);
            }
            else
            {
                return RedirectToAction("Login");

            }
           
        }
        public IActionResult RegisterUserAdmin()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUserAdmin(User user)
        {
           
            if (user.Id == 0)
            {
                if (!ModelState.IsValid)
                {
                    return View(user);

                }
                var result = await AccountService.RegisterUserAdmin(user);
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMsg"] = "User register successfully";
                    RedirectToAction("ManageUsers", "Admin");
                }
                else
                {

                    ModelState.AddModelError("", "user already exists.");

                    return View();
                }
            }
            

            return RedirectToAction("ManageUsers", "Admin");
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");

        }
        public async Task<IActionResult> EditUser(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") > 0)
            {
                var user = await AccountService.GetUserById(id);
                UpdateUserModel userModel = new UpdateUserModel()
                {
                    Id=id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    IsClient=user.IsClient

                };
                return View("EditUserForm", userModel);
            }
            else
            {
                return RedirectToAction("Login");

            }

        }
        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateUserModel userModel)
        {
            var user = new User()
            {
                Id=userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Password = userModel.Password,
                IsClient =userModel.IsClient
            };
            //edit user
            var result = await AccountService.UpdateUser(user, user.Id);
            if (result.IsSuccessStatusCode)
            {
                TempData["SuccessMsg"] = "User save successfully";
                RedirectToAction("ManageUsers", "Admin");
            }
            else
            {

                ModelState.AddModelError("", "Something went wrong..");

                return View("EditUserForm", userModel);
            }
          return  RedirectToAction("ManageUsers", "Admin");


        }
        public async Task<IActionResult> DeleteUser(int id)
        {
            var res = await AccountService.DeleteUser(id);
            var users = await AccountService.GetAllUsers();

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("ManageUsers", users);
            }

            return RedirectToAction("ManageUsers", users);

        }
    }
}