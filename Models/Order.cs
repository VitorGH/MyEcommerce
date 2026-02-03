namespace MyEcommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required List<Product> Products { get; set; }
        public OrderStatus Status { get; set; }
        public int ClientId { get; set; }
    }
}
