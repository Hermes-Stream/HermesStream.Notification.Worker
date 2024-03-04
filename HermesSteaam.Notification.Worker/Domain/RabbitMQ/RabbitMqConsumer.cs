using HermesSteaam.Notification.Worker.Services.Dtos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace HermesSteaam.Notification.Worker.Domain.RabbitMQ
{
    public class RabbitMqConsumer : IDisposable, IRabbitMqConsumer
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

        [Obsolete]
        public void Consume()
        {
            var consumer = new QueueingBasicConsumer(_model);
            _model.BasicConsume(_queueName, false, consumer);


            while (true)
            {
                var basicDeliverEventArgs = consumer.Queue.Dequeue();
                var body = basicDeliverEventArgs.Body;
                var message = Encoding.UTF8.GetString(body) ?? string.Empty;

                try
                {
                    var notification = JsonConvert.DeserializeObject<Services.Dtos.Notification>(message);
                    _logger.LogInformation("MSG: " + notification?.Message + " ID: " + notification?.NotificationId);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing message: {ex.Message}");
                }


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
