using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Work.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("test", false, false, false);

                    var message = Encoding.UTF8.GetBytes("Hello!");

                    channel.BasicPublish("", "test", null, message);

                    Console.WriteLine("Message sent!");
                    Console.Read();
                }
            }
        }
    }
}
