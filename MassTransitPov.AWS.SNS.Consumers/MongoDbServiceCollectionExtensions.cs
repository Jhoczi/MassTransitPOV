using DbProvider.Abstract;
using DbProvider.Mongo;
using DbProvider.Mongo.Abstract;
using DbProvider.Mongo.Configuration;
using MassTransitPov.AWS.SNS.Consumers.Configuration;
using MassTransitPov.AWS.SNS.Consumers.Models;
using MongoDB.Driver;

namespace MassTransitPov.AWS.SNS.Consumers;

public static class MongoDbServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        services.AddSingleton<IMongoProviderSettings>(x => x.GetService<ServiceConfiguration>());
        services.AddSingleton<MongoProviderSettings>();
        services.AddSingleton<IMongoClient>(x =>
        {
            var mongoSettings = x.GetRequiredService<MongoProviderSettings>();
            return new MongoClient(mongoSettings?.ConnectionStrings["Mongo"]);
        });

        services.AddSingleton<IDataProvider<ReservationEntity>, MongoProvider<ReservationEntity>>(provider =>
            new MongoProvider<ReservationEntity>(
                provider.GetRequiredService<IMongoClient>(),
                provider.GetRequiredService<MongoProviderSettings>().DatabaseNames["MassTransitPov"]
            ));

        services.AddSingleton<IDataProvider<ErrorEntity>, MongoProvider<ErrorEntity>>(provider =>
            new MongoProvider<ErrorEntity>(
                provider.GetRequiredService<IMongoClient>(),
                provider.GetRequiredService<MongoProviderSettings>().DatabaseNames["MassTransitPov"]
            ));

        return services;
    }
}