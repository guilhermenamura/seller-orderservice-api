using seller_orderservice_api.Domain.Entities;

namespace seller_orderservice_api.Application.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order> GetByIdAsync(int id);
    }
}
