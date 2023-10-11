using MassTransit;
using MassTransitPov.Api.Messages;

namespace MassTransitPov.Api.Publishers;

public class PingPublisher : BackgroundService
{
    private readonly ILogger<PingPublisher> _logger;
    private readonly IBus _bus;

    public PingPublisher(ILogger<PingPublisher> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();

            var keyPressed = Console.ReadKey(true);
            if (keyPressed.Key != ConsoleKey.Escape)
            {
                await _bus.Publish(new Ping(keyPressed.Key.ToString()), stoppingToken);
            }

            await Task.Delay(200, stoppingToken);
        }
    }
}