using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Cura.Task.Sheard.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cura.Task.Sheard.EntityFramework
{
    public class EfBaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private DbContext _db;
        private DbSet<TEntity> _dbSet;

        public EfBaseRepository(DbContext db)
        {
            _db = db;

            _dbSet = _db.Set<TEntity>();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsNoTracking();
        }

        public TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
           // await _db.Set<TEntity>().AddAsync(entity);
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async ValueTask AddRangeAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(TEntity originalEntity, TEntity newEntity)
        {
            _db.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }
        public long Count(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return Query().Count(filter);
            }
            return Query().Count();
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return await Query().CountAsync(filter);
            }
            return await Query().CountAsync();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveLogical(TEntity entity)
        {
            var type = entity.GetType();
            var property = type.GetProperty("IsDeleted");
            if (property != null) property.SetValue(entity, true);
            var id = type.GetProperty("Id")?.GetValue(entity);
            var original = _dbSet.Find(id);
            Update(original, entity);
        }

        public TEntity Find(params object[] ids)
        {
            return _dbSet.Find(ids);
        }

        public async Task<TEntity> FindAsync(params object[] ids)
        {
            return await _dbSet.FindAsync(ids);
        }

        public void Delete(TKey id)
        {
            var entity = _dbSet.Find(id);

            if (entity == null)
            {
                throw new Exception();
            }

            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public TEntity Get()
        {
            return Query().FirstOrDefault();
        }


        public IEnumerable<TEntity> GetAll()
        {
            return Query();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Query().ToListAsync();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return Query().Where(filter);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType)
        {
            var orderedEntities = Query().OrderEntities(orderBy, orderType);

            return orderedEntities.Where(filter);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, int pageSize, int pageNumber)
        {
            var orderedEntities = Query().Paginate(pageSize, pageNumber);

            return orderedEntities.Where(filter);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, int pageSize, int pageNumber)
        {
            var orderedEntities = Query().OrderEntities(orderBy, orderType).Paginate(pageSize, pageNumber);

            return orderedEntities.Where(filter);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, params Expression<Func<TEntity, object>>[] includes)
        {

            return Query().Where(filter).OrderEntities(orderBy, orderType).Includes(includes);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, int pageSize, int pageNumber, params Expression<Func<TEntity, object>>[] includes)
        {
            var orderedEntities = Query().Paginate(pageSize, pageNumber).Includes(includes);

            return orderedEntities.Where(filter);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, int pageSize, int pageNumber, params Expression<Func<TEntity, object>>[] includes)
        {

            return Query().Where(filter).OrderEntities(orderBy, orderType).Paginate(pageSize, pageNumber).Includes(includes);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predict)
        {
            return await Query().Where(predict).ToListAsync();
        }
        //----------------------------------------------------------------------------------------------------------------

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType)
        {
            var orderedEntities = Query().OrderEntities(orderBy, orderType);

            return await orderedEntities.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, int pageSize, int pageNumber)
        {
            var orderedEntities = Query().Paginate(pageSize, pageNumber);

            return await orderedEntities.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, int pageSize, int pageNumber)
        {
            var orderedEntities = Query().OrderEntities(orderBy, orderType).Paginate(pageSize, pageNumber);

            return await orderedEntities.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, params Expression<Func<TEntity, object>>[] includes)
        {

            return await Query().Where(filter).OrderEntities(orderBy, orderType).Includes(includes).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, int pageSize, int pageNumber, params Expression<Func<TEntity, object>>[] includes)
        {
            var orderedEntities = Query().Paginate(pageSize, pageNumber).Includes(includes);

            return await orderedEntities.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, OrderingType orderType, int pageSize, int pageNumber, params Expression<Func<TEntity, object>>[] includes)
        {

            return await Query().Where(filter).OrderEntities(orderBy, orderType).Paginate(pageSize, pageNumber).Includes(includes).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllLAsync(Expression<Func<TEntity, object>> includes, Expression<Func<TEntity, bool>> filter)
        {

            return await Query().Where(filter).Includes(includes).ToListAsync();
        }
        //-------------------------------------------------



        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return Query().FirstOrDefault(filter);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {

            return Query().Where(filter).Includes(includes).FirstOrDefault();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predict)
        {
            return await Query().Where(predict).FirstOrDefaultAsync();
        }


        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            return await Query().Includes(includes).FirstOrDefaultAsync(filter);
        }

        public TEntity Update(TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await ValueTask.FromResult(_dbSet.Update(entity).Entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }


        //------------------------


        public async Task<(int, IEnumerable<TEntity>)> FindPagedAsync(Expression<Func<TEntity, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            int count = query.Count();
            if (orderByCriteria != null)
            {
                string field = orderByCriteria.First().PairAsSqlExpression;
               
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            query = query.Skip(skip).Take(take);
            if (include != null)
            {
                query = include(query);
            }
            return (count, await query.ToListAsync());
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (include != null)
            {
                query = include(query);
            }
            if (orderByCriteria != null)
            {
               // query = query.OrderBy(orderByCriteria.First().PairAsSqlExpression);
            }
            return await query.ToListAsync();
        }


        public async Task<IEnumerable<TType>> GetSelectAsync<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return await _dbSet.Where(where).Select(select).ToListAsync();
        }

        public async Task<TType> GetSingleAsync<TType>(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TType>> select) where TType : class
        {
            return await _dbSet.Where(where).Select(select).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TType>> FindAllSelectAsync<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true) where TType : class
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderByCriteria != null)
            {
               // query = query.OrderBy(orderByCriteria.First().PairAsSqlExpression);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.Select(select).ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderByCriteria != null)
            {
              //  query = query.OrderBy(orderByCriteria.First().PairAsSqlExpression);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public async Task<(int, IEnumerable<TType>)> FindPagedSelectAsync<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true) where TType : class
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            var count = query.Count();
            if (orderByCriteria == null) return (count, await query.Skip(skip).Take(take).Select(select).ToListAsync());
            var field = orderByCriteria.First().PairAsSqlExpression;
           // query = query.OrderBy(field).Skip(skip).Take(take);

            return (count, await query.Select(select).ToListAsync());
        }
        public async Task<TB> Max<TB>(Expression<Func<TEntity, TB>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbSet.MaxAsync(selector);
            return await _dbSet.Where(predicate).MaxAsync(selector);
        }

        //public long GetNextSequenceValue(string sequenceName)
        //{
        //    var value = _db.GetNextSequenceValue(sequenceName);
        //    return value;
        //}
        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate = null) => predicate == null ? await _dbSet.AnyAsync() : await _dbSet.AnyAsync(predicate);
    }
}
