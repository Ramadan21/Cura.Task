using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cura.Task.Sheard.EntityFramework
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        public DbContext Context { get; }

        private IServiceProvider _serviceProvider { get; set; }

        private Dictionary<Type, object> _repositories;

        private IDbContextTransaction _transaction;

        public UnitOfWork(DbContext dbContext, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Context = dbContext;
            _transaction = Context.Database.BeginTransaction();
        }
        public void Dispose()
        {
            _transaction.Dispose();
            Context.Dispose();
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TRepository);

            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = _serviceProvider.GetService(typeof(TRepository));
            }

            return (TRepository)_repositories[type];
        }

        public int Commit()
        {
            int rows = 0;
            try
            {
                rows = Context.SaveChanges();

                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
            }

            return rows;
        }

        public async Task<int> CommitAsync()
        {
            int rows = 0;

            try
            {
                rows = await Context.SaveChangesAsync();

                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
            }

            return rows;
        }

        public void Rollback()
        {
            _transaction.Rollback();

            _transaction.Dispose();
        }


    }
}
