using Autofac;
using Autofac.Extensions.DependencyInjection;
using HermesSteaam.Notification.Worker.Domain.RabbitMQ;
using HermesStream.Notification.Worker;

IHost host = Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterType<Worker>().As<IHostedService>();
                builder.RegisterType<RabbitMqConsumer>().As<IRabbitMqConsumer>();
            })
            .Build();

host.Run();
