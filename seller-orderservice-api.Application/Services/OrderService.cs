using seller_orderservice_api.Application.DTOs;
using seller_orderservice_api.Application.Repositories;
using seller_orderservice_api.Application.Services;
using seller_orderservice_api.Domain.Entities;
using seller_orderservice_api.Infrastructure;  // Importando a interface do RabbitMQ

namespace seller_orderservice_api.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IRabbitMQService rabbitMQService, IOrderRepository orderRepository)
        {
            _rabbitMQService = rabbitMQService;
            _orderRepository = orderRepository;
        }

        public Order CreateOrder(OrderRequest orderRequest)
        {
            // Criar o objeto de pedido a partir do DTO
            var order = new Order
            {
                CustomerId = orderRequest.CustomerId,  // Mapeando o CustomerId do DTO para o modelo de domínio
                Products = orderRequest.Products?.Select(p => new Product
                {
                    Name = p.Name,         // Mapeando o Name do ProductRequest para o Product
                    Quantity = p.Quantity, // Mapeando a Quantity
                    Price = p.Price        // Mapeando o Price
                }).ToList() ?? new List<Product>(), // Se Products for null, inicializar uma lista vazia
                TotalAmount = orderRequest.Products?.Sum(p => p.Quantity * p.Price) ?? 0m // Calculando o total do pedido
            };

            // Salvar o pedido no repositório (se necessário)
            _orderRepository.AddAsync(order);

            // Publicar o pedido na fila do RabbitMQ
            var orderMessage = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            _rabbitMQService.PublishOrderToQueue("ordersQueue", orderMessage);

            return order;
        }
        public Order GetOrderById(int id)
        {
            return _orderRepository.GetByIdAsync(id).Result;
        }
    }
}
