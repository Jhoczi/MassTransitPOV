using System.Text.Json;
using MassTransit;
using MassTransitPov.AWS.SNS.Consumers.Models;
using MassTransitPov.AWS.SNS.Consumers.Repositories;
using MassTransitPov.Common.Messages;

namespace MassTransitPov.AWS.SNS.Consumers.Faults;

public class DbFaultConsumer : IConsumer<Fault<NewReservationMessage>>
{
    private readonly IErrorRepository _errorRepository;

    public DbFaultConsumer(IErrorRepository errorRepository)
    {
        _errorRepository = errorRepository;
    }
    
    public async Task Consume(ConsumeContext<Fault<NewReservationMessage>> context)
    {
        var exceptionInfo = context.Message.Exceptions.Last();
        var messageBody = context.Message.Message;
        
        Console.WriteLine($"Faulted message with id: {context.Message.Message.Id}");
        await _errorRepository.Save(new ErrorEntity()
        {
            ErrorName = exceptionInfo.ExceptionType,
            ErrorDescription = exceptionInfo.Message,
            Payload = JsonSerializer.Serialize(messageBody),
        });
        Console.WriteLine("Success save error.");
    }
}