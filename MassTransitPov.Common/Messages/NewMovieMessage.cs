namespace MassTransitPov.Common.Messages;

public class NewMovieMessage
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Creator { get; init; }
    public string? Description { get; init; }
    public DateTime TimeStamp { get; init; }
    public int YearOfCreation { get; init; }
}