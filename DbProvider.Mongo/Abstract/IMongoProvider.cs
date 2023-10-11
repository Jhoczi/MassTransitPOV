using System.Linq.Expressions;
using DbProvider.Abstract;
using MongoDB.Driver;

namespace DbProvider.Mongo.Abstract;

public interface IMongoProvider<TEntity> : IDataProvider<TEntity> where TEntity : IEntity
{
    Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter);
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, int page, int pageSize);
    Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter, int page, int pageSize);
}