using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Reciever
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "127.0.0.1",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "myq", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += consumer_Received;
                    channel.BasicConsume(queue: "myq", autoAck: true, consumer: consumer);
                    Console.ReadKey();
                }
            }
        }

        private static void consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var msg = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Recieved {msg}");
        }
    }
}
