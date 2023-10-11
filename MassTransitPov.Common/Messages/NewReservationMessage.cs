namespace MassTransitPov.Common.Messages;

public class NewReservationMessage
{
    public Guid Id { get; init; }
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
    public string Title { get; init; }
    public DateTime ScreeningDate { get; init; }
    public string Email { get; init; }
}