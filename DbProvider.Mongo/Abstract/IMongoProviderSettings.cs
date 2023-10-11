namespace DbProvider.Mongo.Abstract;

public interface IMongoProviderSettings
{
    public Dictionary<string, string> DatabaseNames { get; set; }
    public Dictionary<string, string> ConnectionStrings { get; set; }
}