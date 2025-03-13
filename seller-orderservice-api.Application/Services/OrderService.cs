using seller_orderservice_api.Application.DTOs; 
using seller_orderservice_api.Domain.Entities;

namespace seller_orderservice_api.Application.Services
{
    public class OrderService : IOrderService
    {
        // Aqui você injetaria o repositório de pedidos ou outro componente necessário
        public Order CreateOrder(OrderRequest orderRequest)
        {
            // Mapeando de ProductRequest para Product
            var products = orderRequest.Products.Select(p => new Product
            {
                Name = p.Name,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList();

            // Criando o pedido
            var order = new Order
            {
                Id = new Random().Next(1, 1000), // Simulação de criação de ID
                Products = products,
                CustomerId = orderRequest.CustomerId,
                TotalAmount = products.Sum(p => p.Quantity * p.Price) // Exemplo de cálculo de total
            };

            return order;
        }

        public Order GetOrderById(int id)
        {
            // Implementação simples de buscar pedido (aqui você buscaria no banco de dados ou na memória)
            return new Order
            {
                Id = id,
                Products = new List<Product> {
                    new Product { Name = "Cerveja", Quantity = 10, Price = 5.0m }
                },
                CustomerId = 12345,
                TotalAmount = 50.0m
            };
        }
    }
}
