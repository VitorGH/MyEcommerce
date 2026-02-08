using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEcommerce.Data;
using MyEcommerce.Models;

namespace MyEcommerce.Controllers;

[Authorize(Roles = "Admin")]
[Route("Admin/Products")]
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Stock
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Products.ToListAsync());
    }

    // GET: Admin/Stock/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        if (product == null) return NotFound();

        return View(product);
    }

    // GET: Admin/Stock/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Stock/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Amount")] Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Admin/Stock/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        return View(product);
    }

    // POST: Admin/Stock/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Amount")] Product product)
    {
        if (id != product.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Admin/Stock/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        if (product == null) return NotFound();

        return View(product);
    }

    // POST: Admin/Stock/Delete/5
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}
