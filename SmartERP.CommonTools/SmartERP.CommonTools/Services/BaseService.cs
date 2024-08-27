using Mapster;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SmartERP.CommonTools.Repositories;
using System.Linq.Expressions;

namespace SmartERP.CommonTools.Services
{
    public abstract class BaseService : IBaseService
    {
        private readonly IBaseRepository _repository;

        public BaseService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public TModel Add<TEntity, TModel>(TModel model)
            where TEntity : class
            where TModel : class
        {            
            TEntity entity = model.Adapt<TEntity>();
            entity = _repository.Add(entity);
            return entity.Adapt<TModel>();
        }

        public IEnumerable<TModel> AddRange<TEntity, TModel>(IEnumerable<TModel> models)
            where TEntity : class
            where TModel : class
        {
            IEnumerable<TEntity> entities = models.Adapt<IEnumerable<TEntity>>();
            _repository.AddRange(entities);
            return entities.Adapt<IEnumerable<TModel>>();
        }

        public int Count<TEntity>() where TEntity : class
        {
            return _repository.Count<TEntity>();
        }

        public async Task<int> CountAsync<TEntity>() where TEntity : class
        {
            return await _repository.CountAsync<TEntity>();
        }

        public TModel? Get<TEntity, TModel>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
            where TModel : class
        {
            TEntity? entity = _repository.Get(predicate);
            return entity.Adapt<TModel?>();
        }

        public IEnumerable<TModel> GetAll<TEntity, TModel>() where TEntity : class
        {
            IEnumerable<TEntity> entities = _repository.GetAll<TEntity>();
            return entities.Adapt<IEnumerable<TModel>>();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync<TEntity, TModel>()
            where TEntity : class
            where TModel : class
        {
            IEnumerable<TEntity> entities = await _repository.GetAllAsync<TEntity>(); 
            return entities.Adapt<IEnumerable<TModel>>();
        }

        public async Task<TModel?> GetAsync<TEntity, TModel>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
            where TModel : class
        {
            TEntity? entity = await _repository.GetAsync(predicate);
            return entity.Adapt<TModel?>();
        }

        public TModel? GetById<TEntity, TModel>(long id)
            where TEntity : class
            where TModel : class
        {
            TEntity? entity = _repository.GetById<TEntity>(id); 
            return entity.Adapt<TModel?>();
        }

        public async Task<TModel?> GetByIdAsync<TEntity, TModel>(long id)
            where TEntity : class
            where TModel : class
        {
            TEntity? entity = await _repository.GetByIdAsync<TEntity>(id);
            return entity.Adapt<TModel?>();
        }

        public IEnumerable<TModel> GetList<TEntity, TModel>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
            where TModel : class
        {
            IEnumerable<TEntity> entites = _repository.GetList(predicate);
            return entites.Adapt<IEnumerable<TModel>>();
        }

        public async Task<IEnumerable<TModel>> GetListAsync<TEntity, TModel>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
            where TModel : class
        {
            IEnumerable<TEntity> entites = await _repository.GetListAsync(predicate);
            return entites.Adapt<IEnumerable<TModel>>();
        }

        public void Remove<TEntity, TModel>(TModel model)
            where TEntity : class
            where TModel : class
        {
            TEntity entity = model.Adapt<TEntity>();
            _repository.Remove(entity);
        }

        public void RemoveRange<TEntity, TModel>(IEnumerable<TModel> models)
            where TEntity : class
            where TModel : class
        {
            IEnumerable<TEntity> entities = models.Adapt<IEnumerable<TEntity>>();
            _repository.RemoveRange(entities);
        }

        public void Update<TEntity, TModel>(TModel model)
            where TEntity : class
            where TModel : class
        {
            TEntity entity = model.Adapt<TEntity>();
            _repository.Update(entity);
        }
    }
}
