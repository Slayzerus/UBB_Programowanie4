using System.Linq.Expressions;

namespace SmartERP.CommonTools.Services
{
    public interface IBaseService
    {
        TModel Add<TEntity, TModel>(TModel model) where TEntity : class where TModel : class;
        IEnumerable<TModel> AddRange<TEntity, TModel>(IEnumerable<TModel> models) where TEntity : class where TModel : class;
        void Update<TEntity, TModel>(TModel model) where TEntity : class where TModel : class;
        void Remove<TEntity, TModel>(TModel model) where TEntity : class where TModel : class;
        void RemoveRange<TEntity, TModel>(IEnumerable<TModel> models) where TEntity : class where TModel : class;


        TModel? GetById<TEntity, TModel>(long id) where TEntity : class where TModel : class;
        Task<TModel?> GetByIdAsync<TEntity, TModel>(long id) where TEntity : class where TModel : class;
        TModel? Get<TEntity, TModel>(Expression<Func<TEntity, bool>> predicate) where TEntity : class where TModel : class;
        Task<TModel?> GetAsync<TEntity, TModel>(Expression<Func<TEntity, bool>> predicate) where TEntity : class where TModel : class;
        IEnumerable<TModel> GetList<TEntity, TModel>(Expression<Func<TEntity, bool>> predicate) where TEntity : class where TModel : class;
        Task<IEnumerable<TModel>> GetListAsync<TEntity, TModel>(Expression<Func<TEntity, bool>> predicate) where TEntity : class where TModel : class;
        IEnumerable<TModel> GetAll<TEntity, TModel>() where TEntity : class;
        Task<IEnumerable<TModel>> GetAllAsync<TEntity, TModel>() where TEntity : class where TModel : class;

        int Count<TEntity>() where TEntity : class;
        Task<int> CountAsync<TEntity>() where TEntity : class;
    }
}
