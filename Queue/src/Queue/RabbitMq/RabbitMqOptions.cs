
namespace Queue.RabbitMq
{
    public sealed class RabbitMqOptions
    {
        public string HostName { get; set; } = "localhost";

        public int Port { get; set; } = 5672;

        public string UserName { get; set; } = "guest";

        public string Password { get; set; } = "guest";

        public string ExchangeName { get; set; } = "event_exchange";

        public string QueueName { get; set; }

        public int RetryCount { get; set; } = 5;
    }
}
