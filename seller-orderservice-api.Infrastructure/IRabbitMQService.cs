namespace seller_orderservice_api.Infrastructure
{
    public interface IRabbitMQService
    {
        void PublishOrderToQueue(string queueName, string message);
    }
}
