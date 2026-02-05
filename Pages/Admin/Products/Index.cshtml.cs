using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEcommerce.Data;
using MyEcommerce.Models;

namespace MyEcommerce.Pages.Admin.Products
{

    [Authorize]
    public class IndexModel: PageModel
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
    }
}
