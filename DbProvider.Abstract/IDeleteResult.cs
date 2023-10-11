namespace DbProvider.Abstract;

public interface IDeleteResult
{
    long DeletedCount { get; }
    bool IsAcknowledged { get; }
}