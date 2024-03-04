namespace HermesSteaam.Notification.Worker.RabbitMQ
{
    public interface IRabbitMqConsumer
    {
        void Consume();
        void Dispose();
    }
}