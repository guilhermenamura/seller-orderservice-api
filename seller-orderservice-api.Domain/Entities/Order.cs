namespace seller_orderservice_api.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; } = new List<Product>(); // Inicializando a lista
        public decimal TotalAmount { get; set; }
    }
}
