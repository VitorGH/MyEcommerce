using Stripe;
using Stripe.Checkout;
using MyEcommerce.Models;

namespace MyEcommerce.Services
{
    public class StripeService : IStripeService
    {
        public StripeService(IConfiguration config) 
        {
            StripeConfiguration.ApiKey = config["Stripe:SecretKey"];
        }

        public async Task<string> CreateCheckoutSessionAsync(
            List<CartItem> items, string successUrl, string cancelUrl)
        {
            var lineItems = items.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "brl",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName,
                    },

                    UnitAmount = (long)(item.Price * 100),
                },

                Quantity = item.Quantity
            }).ToList();
            
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["card"],
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }

    }
}
