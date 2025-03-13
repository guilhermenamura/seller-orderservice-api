namespace seller_orderservice_api.Domain.Entities
{
    public class Product
    {
        public string? Name { get; set; } = string.Empty; // Definir como string vazia para evitar warnings
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
