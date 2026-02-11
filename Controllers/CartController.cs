using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Models;
using System.Text.Json;

namespace MyEcommerce.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("Cart");
            var cartItem = string.IsNullOrEmpty(cart)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cart) ?? [];
            return View(cartItem);
        }
    }
}
