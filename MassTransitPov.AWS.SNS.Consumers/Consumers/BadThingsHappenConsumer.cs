using MassTransit;
using MassTransitPov.Common.Messages;

namespace MassTransitPov.AWS.SNS.Consumers.Consumers;

public class BadThingsHappenConsumer : IConsumer<NewReservationMessage>
{
    public Task Consume(ConsumeContext<NewReservationMessage> context)
    {
        Console.WriteLine("Trying to trigger bad things happend consumer action...");
        // IN THIS Consumer scenario something bad happend...
        throw new Exception("Very bad things happened");
    }
}