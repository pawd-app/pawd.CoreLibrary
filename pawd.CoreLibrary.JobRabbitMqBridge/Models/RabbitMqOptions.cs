namespace pawd.CoreLibrary.JobRabbitMqBridge.Models
{
    public class RabbitMqOptions
    {
        public string HostName { get; set; }
        public int Port { get; set; } = 5672;
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string VirtualHost { get; set; } = "/";
        public string JobEventsExchange { get; set; }
        public string JobCreatedQueue { get; set; }
    }
}
