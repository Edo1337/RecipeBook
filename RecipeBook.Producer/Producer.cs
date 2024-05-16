using Newtonsoft.Json;
using RabbitMQ.Client;
using RecipeBook.Producer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Producer
{
    internal class Producer : IMessageProducer
    {
        public void SendMessage<T>(T message, string routingKey, string? exchange = null)
        {
            var factory = new ConnectionFactory() { HostName = "localhost"};
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var json = JsonConvert.SerializeObject(message, Formatting.Indented, 
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange, routingKey, body: body);

        }
    }
}
