using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Models;
using MyEcommerce.Services;
using System.Text.Json;

namespace MyEcommerce.Controllers;

public class CheckoutController : Controller
{
    private readonly IStripeService _stripeService;

    public CheckoutController(IStripeService stripeService)
    {
        _stripeService = stripeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession()
    {
        var cart = HttpContext.Session.GetString("Cart");
        var cartItems = string.IsNullOrEmpty(cart)
            ? []
            : JsonSerializer.Deserialize<List<CartItem>>(cart) ?? [];

        if (cartItems.Count == 0)
            return RedirectToAction("Index", "Cart");

        var domain = $"{Request.Scheme}://{Request.Host}";
        var successUrl = $"{domain}/Checkout/Success";
        var cancelUrl = $"{domain}/Cart";

        var sessionUrl = await _stripeService.CreateCheckoutSessionAsync(
            cartItems, successUrl, cancelUrl);

        return Redirect(sessionUrl);
    }

    public IActionResult Success()
    {
        HttpContext.Session.Remove("Cart"); // Limpa o carrinho
        return View();
    }
}