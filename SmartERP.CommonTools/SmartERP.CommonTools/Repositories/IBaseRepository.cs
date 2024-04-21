using System.Linq.Expressions;

namespace SmartERP.CommonTools.Repositories
{
    public interface IBaseRepository
    {
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;


        TEntity? GetById<TEntity>(long id) where TEntity : class;
        Task<TEntity?> GetByIdAsync<TEntity>(long id) where TEntity : class;
        TEntity? Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        IEnumerable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<IEnumerable<TEntity>> GetListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class;

        int Count<TEntity>() where TEntity : class;
        Task<int> CountAsync<TEntity>() where TEntity : class;

        void Dispose();
    }
}
