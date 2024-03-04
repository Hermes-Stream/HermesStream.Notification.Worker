using HermesStream.Notification.Worker.RabbitMQ;
using RabbitMQ.Client;
using System.Text;

namespace HermesSteaam.Notification.Worker.RabbitMQ
{
    public class RabbitMqConsumer : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _model;
        private readonly string _queueName = "send-notification";
        private readonly ILogger<RabbitMqConsumer> _logger;


        public RabbitMqConsumer(ILogger<RabbitMqConsumer> logger)
        {
            _connection = new RabbitMqConnection().GetConnection();
            _model = _connection.CreateModel();
            _logger = logger;
            _model.QueueDeclare(_queueName, true, false, false, null);
        }

#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
        public void Consume()
        {
            var consumer = new QueueingBasicConsumer(_model);
            _model.BasicConsume(_queueName, false, consumer);

            while (true)
            {
                var basicDeliverEventArgs = consumer.Queue.Dequeue();
                var body = basicDeliverEventArgs.Body;
                var message = Encoding.UTF8.GetString(body);
              
               
                // Processar a mensagem

                _model.BasicAck(basicDeliverEventArgs.DeliveryTag, false);
            }
        }

        public void Dispose()
        {
            _model.Dispose();
            _connection.Dispose();
        }
    }
}
