using RabbitMQ.Client;
namespace HermesStream.Notification.Worker.RabbitMQ
{
    public class RabbitMqConnection
    {
        private readonly string _host = "localhost";
        private readonly int _port = 56195;
        private readonly string _username = "admin";
        private readonly string _password = "123456";

        public IConnection GetConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _host,
                Port = _port,
                UserName = _username,
                Password = _password
            };

            return connectionFactory.CreateConnection();
        }
    }

}
