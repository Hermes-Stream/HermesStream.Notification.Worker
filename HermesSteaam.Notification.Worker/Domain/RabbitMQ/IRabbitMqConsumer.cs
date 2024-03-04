namespace HermesSteaam.Notification.Worker.Domain.RabbitMQ
{
    public interface IRabbitMqConsumer
    {
        void Consume();
        void Dispose();
    }
}