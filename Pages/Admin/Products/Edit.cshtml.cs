using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEcommerce.Data;
using MyEcommerce.Models;

namespace MyEcommerce.Pages.Admin.Products;

[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public EditModel(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public Product Product { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        Product = product;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _db.Products.Update(Product);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}