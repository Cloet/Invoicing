using AutoMapper;
using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CountryController : ControllerBase
    {

        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(IMapper mapper,ILogger<CountryController> logger, ICountryService countryService)
        {
            _mapper = mapper;
            _logger = logger;
            _countryService = countryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = (await _countryService.GetAllAsync()).ToList();

            var dto = _mapper.Map<IEnumerable<CountryDTO>>(countries);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _countryService.GetOneAsync(id);

            if (country == null)
                return NotFound();

            var dto = _mapper.Map<CountryDTO>(country);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> PostCountry(Country country) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            country = await _countryService.CreateOneAsync(country);

            var dto = new CountryDTO
            {
                CountryCode = country.CountryCode,
                Name = country.Name,
                Id = country.Id
            };

            return CreatedAtRoute("DefaultApi", new { id = country.Id }, dto);
        }

    }
}
