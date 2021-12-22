using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Invoicing.EntityFramework
{
    public class InvoicingDbContextFactory : IDbContextFactory<InvoicingDbContext>
    {
        public InvoicingDbContext CreateDBContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<InvoicingDbContext>();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("connections.json", optional: false, reloadOnChange: true).Build();

            optionsBuilder.UseNpgsql(config.GetConnectionString("postgres"));

            return new InvoicingDbContext(optionsBuilder.Options);
        }
    }
}
