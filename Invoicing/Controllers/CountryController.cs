using AutoMapper;
using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services;
using Invoicing.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CountryController : BaseController
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
            try {
                var countries = await _countryService.GetAllAsync();

                if (!countries.Any())
                    return NoContent();

                var dto = _mapper.Map<IEnumerable<CountryDTO>>(countries);

                return Ok(dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpGet("{id}", Name="country")]
        public async Task<IActionResult> GetCountry(int id)
        {
            try {
                var country = await _countryService.GetOneAsync(id);

                if (country == null)
                    return NotFound(new ValidationError($"Country with Id = {id} not found."));

                var dto = _mapper.Map<CountryDTO>(country);

                return Ok(dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PostCountry(Country country) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                var c = (await _countryService.FilterAsync(x => x.CountryCode == country.CountryCode || x.Id == country.Id,null, 1));

                if (c.Count() > 0)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ValidationError($"A country with code {country.CountryCode} already exists."));
                }

                await _countryService.CreateOneAsync(country);
                await _countryService.SaveAsync();

                var dto = _mapper.Map<CountryDTO>(country);

                return CreatedAtRoute("country", new { id = country.Id }, dto);
            }
            catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                country.Id = id;
                var c = await _countryService.GetOneAsync(country.Id);

                if (c == null)
                    return NotFound(new ValidationError($"No country was found with countrycode {country.CountryCode}"));

                await _countryService.UpdateOneAsync(country);
                await _countryService.SaveAsync();

                country = await _countryService.GetOneAsync(country.Id);

                var dto = _mapper.Map<CountryDTO>(country);

                return CreatedAtRoute("country", new { id = country.Id }, dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                var country = await _countryService.GetOneAsync(id);

                if (country == null)
                {
                    return NotFound(new ValidationError($"Country with Id = {id} not found."));
                }

                await _countryService.DeleteOneAsync(id);
                await _countryService.SaveAsync();

                return NoContent();
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

    }
}
