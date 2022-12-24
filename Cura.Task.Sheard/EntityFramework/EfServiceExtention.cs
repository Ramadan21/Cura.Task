using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cura.Task.Sheard.EntityFramework
{
    public static class EfServiceExtention
    {
        public static IServiceCollection AddGenreicDbContext<TContext>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction = null) where TContext : DbContext
        {
            services.AddDbContext<TContext>(optionsAction);

            services.AddScoped<DbContext, TContext>();

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<TContext>));

            return services;
        }
    }
}
