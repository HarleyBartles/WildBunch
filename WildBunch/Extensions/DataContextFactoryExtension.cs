using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WildBunch.Data.DbContexts;

namespace WildBunch.Extensions
{
    public static class DataContextFactoryExtension
    {
        public static IServiceCollection AddDataContextFactory<TContext>(this IServiceCollection services, string connectionString, Action<MySqlDbContextOptionsBuilder> sqlOptions = null)
           where TContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            MySqlDbContextOptionsBuilder mySqlOptionsBuilder = new(optionsBuilder);

            sqlOptions ??= ((MySqlDbContextOptionsBuilder options) =>
            {
                options.EnableRetryOnFailure();
            });

            DataContextFactory<TContext>.GetOptions(connectionString, sqlOptions).Invoke(optionsBuilder);

            services.AddSingleton(optionsBuilder);
            services.AddTransient<DataContextFactory<TContext>>();

            return services;
        }
    }
}
