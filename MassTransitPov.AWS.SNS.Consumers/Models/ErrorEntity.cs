using DbProvider.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MassTransitPov.AWS.SNS.Consumers.Models;

public class ErrorEntity : IEntity<ObjectId>
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string ErrorName { get; set; }
    public string ErrorDescription { get; set; }
    public DateTime TimeSpan { get; init; } = DateTime.UtcNow;
    public string Payload { get; set; }
}