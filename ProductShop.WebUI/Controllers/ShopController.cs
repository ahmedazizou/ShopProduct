using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductShop.WebUI.Integrations.ProductShop.Api.Services;

namespace ProductShop.WebUI.Controllers
{
    public class ShopController : Controller
    {
        public async Task<IActionResult> Products()
        {
            if (HttpContext.Session.GetInt32("UserId")>0)
            {
                var products = await ProductService.GetAllProducts();
                return View(products);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
          
        }
    }
}