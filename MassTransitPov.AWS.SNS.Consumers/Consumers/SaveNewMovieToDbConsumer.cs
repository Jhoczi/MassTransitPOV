using MassTransit;
using MassTransitPov.AWS.SNS.Consumers.Models;
using MassTransitPov.AWS.SNS.Consumers.Repositories;
using MassTransitPov.Common.Messages;

namespace MassTransitPov.AWS.SNS.Consumers.Consumers;

public class SaveNewMovieToDbConsumer : IConsumer<NewReservationMessage>
{
    private readonly IReservationRepository _reservationRepository;

    public SaveNewMovieToDbConsumer(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task Consume(ConsumeContext<NewReservationMessage> context)
    {
        var message = context.Message;

        Console.WriteLine($"Starting creating a movie for message id:: {message.Id}");
        var result = await _reservationRepository.Save(new ReservationEntity
        {
            ProcessId = message.Id,
            Email = message.Email,
            Title = message.Title,
            ScreeningDate = message.ScreeningDate,
            TimeStamp = message.TimeStamp
        });

        Console.WriteLine($"Successful saved to db with ID: {result.Id}");
    }
}