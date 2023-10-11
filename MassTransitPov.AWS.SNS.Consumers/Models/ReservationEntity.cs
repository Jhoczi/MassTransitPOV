using DbProvider.Abstract;
using MongoDB.Bson.Serialization.Attributes;

namespace MassTransitPov.AWS.SNS.Consumers.Models;

public class ReservationEntity : IEntity
{
    [BsonId]
    public object Id { get; set; }
    public Guid ProcessId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Title { get; set; }
    public DateTime ScreeningDate { get; set; }
    public string Email { get; set; }
}