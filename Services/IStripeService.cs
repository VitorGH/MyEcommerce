using MyEcommerce.Models;

namespace MyEcommerce.Services
{
    public interface IStripeService
    {
        Task<string> CreateCheckoutSessionAsync(List<CartItem> items, string successUrl, string cancelUrl);
    }
}
