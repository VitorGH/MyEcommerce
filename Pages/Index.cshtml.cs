using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEcommerce.Data;
using MyEcommerce.Models;


namespace MyEcommerce.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db; 
        }

        public List<Product> Products { get; set; } = [];

        public async Task OnGetAsync()
        {
            Products = await _db.Products.ToListAsync();
        }

        public IActionResult OnPostAddToCart(int productId)
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
            var cartItems = string.IsNullOrEmpty(cart)
                ? new List<CartItem>()
                : System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(cart) ?? [];

            var product = _db.Products.Find(productId);
            if (product != null)
            {
                var existingItem = cartItems.FirstOrDefault(c => c.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    cartItems.Add(new CartItem
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = 1
                    });
                }

                HttpContext.Session.SetString("Cart",
                    System.Text.Json.JsonSerializer.Serialize(cartItems));
            }

            return RedirectToPage();
        }
    }
}
