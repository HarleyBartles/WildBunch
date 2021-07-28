using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System;

namespace WildBunch.Data.DbContexts
{
    /// <summary>
    /// Despite microsofts documentation spawled with examples of using private member data contexts via Dep Inj, it actually currently
    /// introduces a memory leak. Using the factory pattern below removes the need for private members and we can safely 
    /// create data contexts within using statements as and when needed
    /// </summary>
    public class DataContextFactory<TContext> where TContext : DbContext
    {
        private DbContextOptionsBuilder<TContext> _dbOptions { get; set; }
        private IOptions<OperationalStoreOptions> _operationalStoreOptions { get; set; }

        public DataContextFactory(DbContextOptionsBuilder<TContext> dbOptions,
            IOptions<OperationalStoreOptions> storeOptions)
        {
            _dbOptions = dbOptions;
            _operationalStoreOptions = storeOptions;
        }

        public TContext Create()
        {
            return Activator.CreateInstance(typeof(TContext), _dbOptions.Options, _operationalStoreOptions) as TContext;
        }

        public static Action<DbContextOptionsBuilder> GetOptions(string connectionString, Action<MySqlDbContextOptionsBuilder> sqlServerOptionsAction = null)
        {
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            // returning action as that's whats generally needed. this way if we just need the options
            // we can invoke the builder and then retreieve the options from that
            return (DbContextOptionsBuilder options) =>
            {
                options.UseMySql(connectionString, serverVersion, sqlServerOptionsAction)
                    .EnableSensitiveDataLogging() // <-- These two calls are optional but help
                    .EnableDetailedErrors();       // <-- with debugging (remove for production).
            };
        }
    }
}
