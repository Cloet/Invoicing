using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Repositories;
using Invoicing.EntityFramework.Services.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services
{
    public class CityService: GenericService<City>, ICityService
    {

        public CityService(ICityRepository repository) : base(repository)
        {

        }

        protected override City BeforeInsertUpdate(City entity)
        {
            if (entity.Country != null)
                entity.CountryId = entity.Country.Id;
            entity.Country = null;
            return base.BeforeInsertUpdate(entity);
        }


    }
}
