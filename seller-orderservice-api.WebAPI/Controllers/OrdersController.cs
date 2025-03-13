using Microsoft.AspNetCore.Mvc;
using seller_orderservice_api.Application.Services;
using seller_orderservice_api.Application.DTOs;
using seller_orderservice_api.Domain.Entities;

namespace seller_orderservice_api.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                return BadRequest("Invalid order data.");
            }

            // Chama a service para criar o pedido e enviar para o RabbitMQ
            var order = _orderService.CreateOrder(orderRequest);

            return CreatedAtAction(nameof(_orderService.GetOrderById), new { id = order.Id }, order);
        }
    }
}
