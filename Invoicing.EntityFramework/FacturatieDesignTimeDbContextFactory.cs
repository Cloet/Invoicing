using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework
{
    public class InvoicingDesignTimeDbContextFactory : IDesignTimeDbContextFactory<InvoicingDbContext>
    {

        public InvoicingDbContext CreateDbContext(string[] args = null)
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
