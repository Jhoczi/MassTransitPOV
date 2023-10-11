using MassTransit;
using MassTransitPov.Common.Messages;

namespace MassTransitPov.RabbitMQ.Consumer;

public class NotificationConsumer : IConsumer<NewMovieMessage>
{
    public Task Consume(ConsumeContext<NewMovieMessage> context)
    {
        var message = context.Message;
        Console.WriteLine($"Email notification for message with ID: {message.Id} has been created.");
        return Task.CompletedTask;
    }
}