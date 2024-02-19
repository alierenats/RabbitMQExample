using RabbitMQ.Client;
using System.Text;

 class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Press enter");
        Console.ReadLine();
        var factory = new ConnectionFactory();
        factory.HostName = "localhost";
        factory.Port = 5672;
        factory.UserName = "guest";
        factory.Password = "guest";

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        string message = "Message1111";

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: "hello",
            basicProperties: null,
            body: body
            );

        Console.WriteLine($" [*] Send {message}");

        Console.ReadLine();







    }
}