using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MyEcommerce.Data;
using MyEcommerce.Models;
using System.Text.Json;

namespace MyEcommerce.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.ToListAsync();
        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId)
    {
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return RedirectToPage("/Account/Login", new
            {
                area = "Identity",
                returnUrl = $"/?addProduct={productId}"
            });
        }

        var cart = HttpContext.Session.GetString("Cart");
        var cartItems = string.IsNullOrEmpty(cart) ?
            new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(cart) ?? [];

        var product = await _context.Products.FindAsync(productId);

        if (product != null) {
            
            var existingItem = cartItems.FirstOrDefault(c => c.ProductId == productId);
            
            if (existingItem != null)
            {
                existingItem.Quantity++;
            } 
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("Error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
    }
}