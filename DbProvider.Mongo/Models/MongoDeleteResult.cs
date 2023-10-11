using DbProvider.Abstract;
using MongoDB.Driver;

namespace DbProvider.Mongo.Models;

public class MongoDeleteResult : IDeleteResult
{
    public long DeletedCount { get => _mongoDeleteResult.DeletedCount;}
    public bool IsAcknowledged { get => _mongoDeleteResult.IsAcknowledged; }

    private readonly DeleteResult _mongoDeleteResult;

    public MongoDeleteResult(DeleteResult mongoDeleteResult)
    {
        _mongoDeleteResult = mongoDeleteResult;
    }
}