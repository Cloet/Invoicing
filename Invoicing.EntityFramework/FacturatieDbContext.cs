using Invoicing.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Invoicing.EntityFramework
{
    public class InvoicingDbContext : SQLDbContext
    {

        public InvoicingDbContext(DbContextOptions<InvoicingDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceLine> InvoicesLines { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<VAT> VATs { get; set; }

        protected void CountryModel(ModelBuilder builder)
        {
            builder.Entity<Country>(country =>
            {
                country.HasIndex(c => c.CountryCode).IsUnique();
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }

            CountryModel(modelBuilder);
        }

    }
}
