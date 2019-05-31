
namespace Queue.RabbitMq
{
    internal static class Defaults
    {
        public const string EXCHANGE_NAME = "event_bus_exchange";
        public const int  RETRY_COUNT = 5;
        public const string DEFAULT_USER_NAME = "guest";
        public const string DEFAULT_PASSWORD = "guest";
        public const string DEFAULT_HOST_NAME = "localhost";
        public const int DEFAULT_HOST_PORT = 5672;
    }

    public sealed class RabbitMQOptions
    {
        private int _port = Defaults.DEFAULT_HOST_PORT;
        private string _userName = Defaults.DEFAULT_USER_NAME;
        private string _password = Defaults.DEFAULT_PASSWORD;
        private string _hostName = Defaults.DEFAULT_HOST_NAME;
        private string _exchangeName = Defaults.EXCHANGE_NAME;
        private int _retryCount = Defaults.RETRY_COUNT;

        public string HostName
        {
            get => _hostName;
            set => _hostName = value;
        }

        public int Port
        {
            get => _port;
            set => _port = value;
        }

        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string ExchangeName
        {
            get => _exchangeName;
            set => _exchangeName = value;
        }

        public string QueueName { get; set; }

        public int RetryCount
        {
            get => _retryCount;
            set => _retryCount = value;
        }
    }
}
