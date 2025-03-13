using RabbitMQ.Client;
using System.Text;

namespace seller_orderservice_api.Infrastructure
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly string _hostname = "localhost"; // ou outro servidor RabbitMQ
        private readonly string _username = "guest";
        private readonly string _password = "guest";

        public void PublishOrderToQueue(string queueName, string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            try
            {
                // A nova forma de criar a conexão
                using (var connection = factory.CreateConnection())  // Método correto de acordo com a versão 6.x ou superior
                using (var channel = connection.CreateModel())
                {
                    // Declarar a fila (para garantir que a fila exista)
                    channel.QueueDeclare(queue: queueName,
                                         durable: false,  // A fila não é durável
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(message); // Convertendo a mensagem para bytes

                    // Publicar a mensagem na fila
                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"[x] Enviado: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar a mensagem: {ex.Message}");
            }
        }
    }
}
