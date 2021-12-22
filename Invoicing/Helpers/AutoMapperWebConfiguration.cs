using AutoMapper;
using Invoicing.Domain.Model;

namespace Invoicing.Helpers
{
    public static class AutoMapperWebConfiguration
    {

        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CountryProfile>();
                cfg.AddProfile<CityProfile>();
            });
        }

    }

    public class CountryProfile: Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDTO>();
        }
    }

    public class CityProfile : Profile
    { 
        public CityProfile()
        {
            CreateMap<City, CityDTO>();
        }
    }

}
