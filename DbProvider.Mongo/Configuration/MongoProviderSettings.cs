using DbProvider.Mongo.Abstract;

namespace DbProvider.Mongo.Configuration;

public class MongoProviderSettings
{
    public Dictionary<string,string> DatabaseNames { get; set; }
    public Dictionary<string,string> ConnectionStrings { get; set; }

    public MongoProviderSettings(IMongoProviderSettings configuration)
    {
        ConnectionStrings = configuration.ConnectionStrings;
        DatabaseNames = configuration.DatabaseNames;
    }
}