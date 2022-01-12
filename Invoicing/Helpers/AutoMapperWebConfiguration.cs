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
                cfg.AddProfile<ArticleProfile>();
                cfg.AddProfile<VATProfile>();
                cfg.AddProfile<AddressProfile>();
                cfg.AddProfile<CustomerProfile>();
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

    public class ArticleProfile: Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDTO>();
        }
    }

    public class VATProfile: Profile
    {
        public VATProfile()
        {
            CreateMap<VAT, VatDTO>();
        }
    }

    public class AddressProfile: Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>();
        }
    }

    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>();
        }
    }

}
