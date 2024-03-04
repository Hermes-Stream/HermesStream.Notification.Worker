using HermesStream.Notification.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddLogging();
    })
    .Build();

host.Run();
