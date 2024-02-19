using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: string.Empty,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        Console.WriteLine("[*] Waiting for message..");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x]  {message}");
        };

        channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

        Console.ReadLine();
    }

}