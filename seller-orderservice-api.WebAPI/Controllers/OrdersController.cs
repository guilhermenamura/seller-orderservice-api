using Microsoft.AspNetCore.Mvc;
using seller_orderservice_api.Application.Services; // Importando a interface IOrderService
using seller_orderservice_api.Application.DTOs; // Para os DTOs
using seller_orderservice_api.Domain.Entities; // Para as entidades

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

            var order = _orderService.CreateOrder(orderRequest);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
