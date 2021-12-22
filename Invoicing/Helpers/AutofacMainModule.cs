
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Invoicing.EntityFramework;

namespace Invoicing.Helpers
{
    public class AutofacMainModule: Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Automapper
            builder.RegisterAutoMapper(typeof(AutoMapperWebConfiguration).Assembly);

            // Context
            builder.RegisterType<InvoicingDbContextFactory>().As<IDbContextFactory<InvoicingDbContext>>();
        }

    }
}
