using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "127.0.0.1",
                UserName="guest",
                Password="guest"
            };
            var end = false;
            using (var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    while (!end) { 
                    channel.QueueDeclare(queue: "myq", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    string msg = Console.ReadLine();
                        if (msg.ToLower() =="quit")
                        {
                            end = true;
                        }
                    var buffer = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish(exchange: "", routingKey: "myq", basicProperties: null, body: buffer);
                    Console.WriteLine($"send {msg}>>");
                    }
                }
            }
        }
    }
}
