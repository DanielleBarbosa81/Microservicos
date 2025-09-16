using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Shared.Contracts;

namespace SalesService.Infrastructure.Messaging
{
    public class RabbitMqPublisher
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "venda_criada";

        public void PublicarVenda(VendaCriadaEvent evento)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname
            };

            // ✅ Conexão e canal síncronos
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // ✅ Declaração da fila
            channel.QueueDeclare(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            // ✅ Serialização do evento
            var body = JsonSerializer.SerializeToUtf8Bytes(evento);

            // ✅ Publicação do evento
            channel.BasicPublish(
                exchange: "",
                routingKey: _queueName,
                basicProperties: null,
                body: body
            );
        }
    }
}
