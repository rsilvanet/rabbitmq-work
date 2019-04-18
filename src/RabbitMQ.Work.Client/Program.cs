using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Work.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var guid = Guid.NewGuid().ToString();

            Console.WriteLine("Hello, your id is {0}.", guid);

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
            };
            
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("test", false, false, false);

                    var message1 = Encoding.UTF8.GetBytes($"Client {guid} connected.");
                    channel.BasicPublish(string.Empty, "test", null, message1);

                    while (true)
                    {
                        var text = Console.ReadLine();

                        if (text == "quit")
                        {
                            break;
                        }

                        var message2 = Encoding.UTF8.GetBytes($"Client {guid} says: {text}");
                        channel.BasicPublish(string.Empty, "test", null, message2);
                    }

                    var message3 = Encoding.UTF8.GetBytes($"Client {guid} disconnected.");
                    channel.BasicPublish(string.Empty, "test", null, message3);
                }
            }
        }
    }
}
