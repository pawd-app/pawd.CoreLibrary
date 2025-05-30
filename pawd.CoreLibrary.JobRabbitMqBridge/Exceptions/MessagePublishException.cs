namespace pawd.CoreLibrary.JobRabbitMqBridge.Exceptions
{
    public class MessagePublishException : Exception
    {
        public MessagePublishException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
