using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seller_orderservice_api.Application.Repositories;
using seller_orderservice_api.Domain.Entities;
using seller_orderservice_api.Infrastructure.Settings;

namespace seller_orderservice_api.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _orders = mongoDatabase.GetCollection<Order>("Orders");
        }

        public async Task AddAsync(Order order)
        {
            await _orders.InsertOneAsync(order);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orders.Find(order => order.Id == id).FirstOrDefaultAsync();
        }
    }
}
