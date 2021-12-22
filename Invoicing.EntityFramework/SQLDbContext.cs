using Invoicing.Base.Logging.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Invoicing.EntityFramework
{
    public abstract class SQLDbContext : DbContext
    {
        public SQLDbContext(DbContextOptions options) : base(options)
        {
        }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .SetMinimumLevel(LogLevel.Debug)
                .AddCLogger();
        });

        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseLoggerFactory(loggerFactory)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();

    }
}
