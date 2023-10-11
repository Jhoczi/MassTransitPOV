using System.Linq.Expressions;
using DbProvider.Abstract;
using DbProvider.Mongo.Abstract;
using DbProvider.Mongo.Models;
using MongoDB.Driver;

namespace DbProvider.Mongo;

public class MongoProvider<TEntity> : IMongoProvider<TEntity> where TEntity : IEntity
{
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<TEntity> _collection;

    public MongoProvider(IMongoClient mongoClient, string databaseName, string collectionName = "")
    {
        _db = mongoClient.GetDatabase(databaseName);
        
        if (string.IsNullOrEmpty(collectionName))
            collectionName= typeof(TEntity).Name;
        
        _collection = _db.GetCollection<TEntity>(collectionName);
    }
    
    public async Task<TEntity> Create(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        var result = await _collection.ReplaceOneAsync(
            x => x.Id.Equals(entity.Id), entity,
            new ReplaceOptions { IsUpsert = false });

        if (result.MatchedCount != 1)
        {
            throw new KeyNotFoundException($"Updating {_collection.CollectionNamespace} failed for id = {entity.Id}");
        }

        return entity;
    }

    public async Task<TEntity> GetSingle(object id)
    {
        return await GetSingle( x => x.Id.Equals(id));
    }

    public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> criteria)
    {
        return (await _collection.FindAsync(criteria)).FirstOrDefault();
    }

    public async Task<IDeleteResult> Delete(object id)
    {
        return await Delete(x => x.Id.Equals(id));
    }

    public async Task<IDeleteResult> Delete(Expression<Func<TEntity, bool>> criteria)
    {
        return new MongoDeleteResult(await _collection.DeleteOneAsync(criteria));
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter)
    {
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter)
    {
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, int page, int pageSize)
    {
        if (page < 1)
            throw new ArgumentException("Page index must be at least 1", nameof(page));
        
        return await _collection.Find(filter).Skip(page-1).Limit(pageSize).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter, int page, int pageSize)
    {
        if (page < 1)
            throw new ArgumentException("Page index must be at least 1", nameof(page));
        
        return await _collection.Find(filter).Skip(page-1).Limit(pageSize).ToListAsync();
    }
}