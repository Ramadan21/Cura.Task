using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cura.Task.Sheard.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Cura.Task.Sheard.DTOs;

namespace Cura.Task.Sheard.EntityFramework
{
    public static class RepositoryExtensions
    {
        public static IQueryable<TEntity> OrderEntities<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, object>> orderBy, OrderingType orderType) where TEntity : class
        {
            if (orderBy != null)
            {
                switch (orderType)
                {
                    case OrderingType.Ascending:
                        return query.OrderBy(orderBy);
                    case OrderingType.Descending:
                        return query.OrderByDescending(orderBy);
                    default:
                        return query.OrderBy(orderBy);
                }
            }

            return query;
        }

        public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query, int? pageSize, int? pageNumber) where TEntity : class
        {
            if (pageNumber != null && pageSize != null)
            {
                return query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }

        public static IQueryable<TEntity> Includes<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }


            return query;
        }

        public static IQueryable<T> DynamicOrderBy<T>(this IQueryable<T> source, IEnumerable<SortModel> sortModels)
        {
            var expression = source.Expression;
            int count = 0;
            foreach (var item in sortModels)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, item.ColId);
                var method = string.Equals(item.Sort, "desc", StringComparison.OrdinalIgnoreCase) ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }

        //public static long GetNextSequenceValue(this DbContext context, string name, string schema = null)
        //{
        //    var sqlGenerator = context.GetService<IUpdateSqlGenerator>();
        //    var sql = sqlGenerator.GenerateNextSequenceValueOperation(name, schema ?? context.Model.GetDefaultSchema());
        //    var rawCommandBuilder = context.GetService<IRawSqlCommandBuilder>();
        //    var command = rawCommandBuilder.Build(sql);
        //    var connection = context.GetService<IRelationalConnection>();
        //    var logger = context.GetService<IDiagnosticsLogger<DbLoggerCategory.Database.Command>>();
        //    var parameters = new RelationalCommandParameterObject(connection, null, null, context, logger);
        //    var result = command.ExecuteScalar(parameters);
        //    return Convert.ToInt64(result, CultureInfo.InvariantCulture);
        //}
    }
}
