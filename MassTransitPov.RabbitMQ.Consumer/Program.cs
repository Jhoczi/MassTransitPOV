using MassTransit;
using MassTransitPov.RabbitMQ.Consumer;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost", "/", h =>
    {
        h.Username("user");
        h.Password("password");
    });

    cfg.ReceiveEndpoint("new-movie-queue", e => { e.Consumer<NewMovieConsumer>(); });
    //cfg.ReceiveEndpoint("notification-queue", e => { e.Consumer<NotificationConsumer>(); });
    //cfg.ReceiveEndpoint( e => { e.Consumer<NewMovieConsumer>(); });
});

await busControl.StartAsync(); // Start the bus
Console.WriteLine("Press any key to exit");
await Task.Run(() => Console.ReadKey());
await busControl.StopAsync(); // Stop the bus