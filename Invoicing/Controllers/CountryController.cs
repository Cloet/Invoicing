using AutoMapper;
using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services;
using Invoicing.Helpers;
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
            try {
                var countries = (await _countryService.GetAllAsync()).ToList();

                if (!countries.Any())
                    return NoContent();

                var dto = _mapper.Map<IEnumerable<CountryDTO>>(countries);

                return Ok(dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("", ex.Message));
            }
        }

        [HttpGet("{id}", Name="country")]
        public async Task<IActionResult> GetCountry(int id)
        {
            try {
                var country = await _countryService.GetOneAsync(id);

                if (country == null)
                    return NotFound(new ValidationError("", $"Country with Id = {id} not found."));

                var dto = _mapper.Map<CountryDTO>(country);

                return Ok(dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("",ex.Message));
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
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ValidationError("CountryCode", $"A country with code {country.CountryCode} already exists."));
                }

                country = await _countryService.CreateOneAsync(country);

                var dto = _mapper.Map<CountryDTO>(country);

                return CreatedAtRoute("country", new { id = country.Id }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("", ex.Message));
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
                    return NotFound(new ValidationError("", $"No country was found with countrycode {country.CountryCode}"));

                country = await _countryService.UpdateOneAsync(country);
                var dto = _mapper.Map<CountryDTO>(country);

                return CreatedAtRoute("country", new { id = country.Id }, dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("", ex.Message));
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
                    return NotFound(new ValidationError("", $"Country with Id = {id} not found."));
                }

                await _countryService.DeleteOneAsync(id);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("", ex.Message));
            }
        }

    }
}
