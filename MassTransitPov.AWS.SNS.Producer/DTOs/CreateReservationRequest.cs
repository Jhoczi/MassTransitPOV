namespace MassTransitPov.AWS.SNS.Producer.DTOs;

public class CreateReservationRequest
{
    public string Title { get; init; }
    public DateTime ScreeningDate { get; init; }
    public string Email { get; init; }
}