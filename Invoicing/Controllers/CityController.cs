using AutoMapper;
using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services;
using Invoicing.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CityController : ControllerBase    
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;


        public CityController(IMapper mapper, ILogger<CityController> logger, ICityService cityService)
        {
            _mapper = mapper;
            _logger = logger;
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                var cities = await _cityService.GetAllAsync();

                if (!cities.Any())
                    return NoContent();
                    // return NotFound(new ValidationError("", $"No cities found..."));

                var dto = _mapper.Map<IEnumerable<CityDTO>>(cities);

                return Ok(dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("", ex.Message));
            }
        }

        [HttpGet("{id}", Name="city")]
        public async Task<IActionResult> GetCity(int id)
        {
            try
            {
                var city = await _cityService.GetOneAsync(id);

                if (city == null)
                    return NotFound(new ValidationError("", $"City with Id = {id} not found."));

                var dto = _mapper.Map<CityDTO>(city);

                return Ok(dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("", ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCity(City city)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var c = await _cityService.FilterAsync(x => x.Id == city.Id || (x.Name == city.Name && x.Postal == city.Postal && x.Country == city.Country),null, 1);

                if (c.Count() > 0)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ValidationError("", $"A city with the same name and postal code already exists for this country."));
                
                city = await _cityService.CreateOneAsync(city);
                var dto = _mapper.Map<CityDTO>(city);

                return CreatedAtRoute("city", new { id = city.Id }, dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("", ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                city.Id = id;
                var c = await _cityService.GetOneAsync(city.Id);

                if (c == null)
                    return NotFound(new ValidationError("", $"No city was found with id = {id}."));

                city = await _cityService.UpdateOneAsync(city);
                var dto = _mapper.Map<CityDTO>(city);

                return CreatedAtRoute("city", new { id = city.Id }, dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("", ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            try
            {
                var city = await _cityService.GetOneAsync(id);

                if (city == null)
                    return NotFound(new ValidationError("", $"City with Id = {id} not found."));

                await _cityService.DeleteOneAsync(id);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ValidationError("", ex.Message));
            }
        }

    }
}
