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
            builder.RegisterType<VATService>().As<IVATService>();
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<AddressService>().As<IAddressService>();
        }

    }
}
