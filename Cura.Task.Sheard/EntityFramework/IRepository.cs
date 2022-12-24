using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cura.Task.Sheard.Dtos;
using Microsoft.EntityFrameworkCore.Query;
using Cura.Task.Sheard.DTOs;

namespace Cura.Task.Sheard.EntityFramework
{
    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class
    {

    }
    public interface IRepository<TEntity, TKey> : IRepository where TEntity : class
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        ValueTask AddRangeAsync(List<TEntity> entities);
        long Count(Expression<Func<TEntity, bool>> filter = null);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        void Delete(TEntity entity);
        void Delete(TKey id);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true);
        Task<IEnumerable<TType>> FindAllSelectAsync<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true) where TType : class;
        Task<(int, IEnumerable<TEntity>)> FindPagedAsync(Expression<Func<TEntity, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true);
        Task<(int, IEnumerable<TType>)> FindPagedSelectAsync<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true) where TType : class;
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);
        TEntity Get();
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        TEntity Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, int pageSize, int pageNumber);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, int pageSize, int pageNumber, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, int pageSize, int pageNumber);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, int pageSize, int pageNumber, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predict);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, int pageSize, int pageNumber);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, int pageSize, int pageNumber, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllLAsync(Expression<Func<TEntity, object>> includes, Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, int pageSize, int pageNumber);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, int pageSize, int pageNumber, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predict);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TType>> GetSelectAsync<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class;
        Task<TType> GetSingleAsync<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class;
        Task<TB> Max<TB>(Expression<Func<TEntity, TB>> selector, Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> Query();
        TEntity Update(TEntity entity);
        void Update(TEntity originalEntity, TEntity newEntity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        Task<TEntity> FindAsync(params object[] ids);
        void RemoveLogical(TEntity entity);
        TEntity Find(params object[] ids);
      //  long GetNextSequenceValue(string query);
        Task<bool> Any(Expression<Func<TEntity, bool>> predicate = null);
    }

    public interface IRepository { }
}
