﻿using Autofac;
using Invoicing.EntityFramework.Repositories;

namespace Invoicing.Helpers
{
    public class AutofacRepositoryModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<CountryRepository>().As<ICountryRepository>();
            builder.RegisterType<CityRepository>().As<ICityRepository>();
        }

    }
}