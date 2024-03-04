using Autofac;
using HermesSteaam.Notification.Worker.RabbitMQ;

namespace HermesSteaam.Notification.Worker
{
    public class Ioc : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RabbitMqConsumer>().As<IRabbitMqConsumer>();
        }
    }
}
