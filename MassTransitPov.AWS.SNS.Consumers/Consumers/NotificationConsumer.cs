using MassTransit;
using MassTransitPov.Common.Messages;

namespace MassTransitPov.AWS.SNS.Consumers.Consumers;

public class NotificationConsumer : IConsumer<NewReservationMessage>
{
    public Task Consume(ConsumeContext<NewReservationMessage> context)
    {
        var message = context.Message;

        Console.WriteLine($"Email notification will be sent to: {message.Email}");
        // SOME LOGIC TO SEND EMAILS ...
        return Task.CompletedTask;
    }
}