using System.Text.Json.Serialization;

namespace MassTransitPov.RabbitMQ.Producer.DTOs;

public class AddMovieRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Title { get; init; }
    public string Creator { get; init; }
    public string? Description { get; init; }
    [JsonIgnore]
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    public int YearOfCreation { get; init; }
}