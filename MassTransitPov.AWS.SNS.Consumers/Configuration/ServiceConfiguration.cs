using DbProvider.Mongo.Abstract;

namespace MassTransitPov.AWS.SNS.Consumers.Configuration;

public class ServiceConfiguration : IMongoProviderSettings
{
    private Dictionary<string, string> Settings { get; set; }
    public Dictionary<string, string> DatabaseNames { get; set; }
    public Dictionary<string, string> ConnectionStrings { get; set; }

    public ServiceConfiguration(IConfiguration configuration)
    {
        configuration.Bind(this);
    }
}