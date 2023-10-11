using System.Linq.Expressions;

namespace DbProvider.Abstract;

public interface IDataProvider<TEntity> where TEntity: IEntity
{
    Task<TEntity> Create(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> GetSingle(object id);
    Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> criteria);
    Task<IDeleteResult> Delete(object id);
    Task<IDeleteResult> Delete(Expression<Func<TEntity, bool>> criteria);
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter);
}