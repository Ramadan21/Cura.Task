using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cura.Task.Sheard.EntityFramework
{
    public class Repository<TEntity> : EfBaseRepository<TEntity, Guid>, IRepository<TEntity> where TEntity : class
    {
        public Repository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
