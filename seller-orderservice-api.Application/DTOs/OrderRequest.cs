namespace seller_orderservice_api.Application.DTOs
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public List<ProductRequest> Products { get; set; } = new List<ProductRequest>(); // Inicializando a lista
    }

    public class ProductRequest
    {
        public string Name { get; set; } = string.Empty; // Definindo como string vazia
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}