using MassTransit;
using MassTransitPov.Common.Messages;

namespace MassTransitPov.RabbitMQ.Consumer;

public class NewMovieConsumer : IConsumer<NewMovieMessage>
{
    public Task Consume(ConsumeContext<NewMovieMessage> context)
    {
        var message = context.Message;
        Console.WriteLine($"New movie: {message.Title} with ID: {message.Id} has been created {message.TimeStamp} and consumed.");
        return Task.CompletedTask;
    }
}