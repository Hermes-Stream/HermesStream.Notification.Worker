using HermesSteaam.Notification.Worker.Domain.RabbitMQ;

namespace HermesStream.Notification.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMqConsumer _consumer;

        public Worker(ILogger<Worker> logger, IRabbitMqConsumer consumer)
        {
            _logger = logger;
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                 _consumer.Consume();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
