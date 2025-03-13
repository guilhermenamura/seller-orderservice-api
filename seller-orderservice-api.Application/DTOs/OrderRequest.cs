namespace seller_orderservice_api.Application.DTOs
{
    public class ProductRequest
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }  // Adicionando o preço para o cálculo do TotalAmount
    }

    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public List<ProductRequest>? Products { get; set; }
    }
}
