using AutoMapper;
using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services;
using Invoicing.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AddressController: BaseController
    {

        private readonly ILogger<AddressController> _logger;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IMapper mapper, ILogger<AddressController> logger, IAddressService addressService)
        {
            _mapper = mapper;
            _logger = logger;
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                var addresses = await _addressService.GetAllAsync();

                if (!addresses.Any())
                    return NoContent();

                var dto = _mapper.Map<IEnumerable<AddressDTO>>(addresses);

                return Ok(dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpGet("{id}", Name="address")]
        public async Task<IActionResult> GetAddress(int id)
        {
            try
            {
                var address = await _addressService.GetOneAsync(id);

                if (address == null)
                    return NotFound(ValidationError.CreateError($"Address with Id = {id} not found."));

                var dto = _mapper.Map<AddressDTO>(address);

                return Ok(dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAddress(Address address)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var a = await _addressService.FilterAsync(x => x.CustomerId == address.CustomerId && x.Name == address.Name, 1);

                if (a.Count() > 0)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, ValidationError.CreateError($"An address with the same name already exists for this customer"));

                await _addressService.CreateOneAsync(address);
                await _addressService.SaveAsync();

                address = await _addressService.GetOneAsync(address.Id);

                var dto = _mapper.Map<AddressDTO>(address);

                return CreatedAtRoute("address", new { id = address.Id }, dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                address.Id = id;
                var a = await _addressService.GetOneAsync(address.Id);

                if (a == null)
                    return NotFound(ValidationError.CreateError($"No address found with id {address.Id}"));

                await _addressService.UpdateOneAsync(address);
                await _addressService.SaveAsync();

                address = await _addressService.GetOneAsync(address.Id);

                var dto = _mapper.Map<AddressDTO>(address);

                return CreatedAtRoute("address", new { id = address.Id }, dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                var address = await _addressService.GetOneAsync(id);

                if (address == null)
                    return NotFound(ValidationError.CreateError($"Address with Id = {id} not found."));

                await _addressService.DeleteOneAsync(id);
                await _addressService.SaveAsync();

                return NoContent();
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }


    }
}
