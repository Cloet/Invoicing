using Autofac;
using Invoicing.EntityFramework;
using Invoicing.EntityFramework.Services;

namespace Invoicing.Helpers
{
    public class AutofacServiceModule: Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<CityService>().As<ICityService>();
        }

    }
}
