using seller_orderservice_api.Application.DTOs;  
using seller_orderservice_api.Domain.Entities;

namespace seller_orderservice_api.Application.Services
{
    public interface IOrderService
    {
        Order CreateOrder(OrderRequest orderRequest);
        Order GetOrderById(int id);
    }
}
