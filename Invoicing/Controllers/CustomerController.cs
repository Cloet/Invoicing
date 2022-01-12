using AutoMapper;
using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services;
using Invoicing.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomerController: BaseController
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(IMapper mapper, ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _mapper = mapper;
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();

                if (!customers.Any())
                    return NoContent();

                var dto = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

                return Ok(dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpGet("{id}", Name ="customer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerService.GetOneAsync(id);

                if (customer == null)
                    return NotFound(ValidationError.CreateError($"Customer with Id = {id} not found."));

                var dto = _mapper.Map<CustomerDTO>(customer);

                return Ok(dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var c = await _customerService.FilterAsync(x => x.CustomerCode == customer.CustomerCode);

                if (c.Count() > 0)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, ValidationError.CreateError($"A customer with code = {customer.CustomerCode} already exists."));

                await _customerService.CreateOneAsync(customer);
                await _customerService.SaveAsync();

                customer = await _customerService.GetOneAsync(customer.Id);

                var dto = _mapper.Map<CustomerDTO>(customer);

                return CreatedAtRoute("customer", new { id = customer.Id }, dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                customer.Id = id;
                var c = await _customerService.GetOneAsync(customer.Id);

                if (c == null)
                    return NotFound(ValidationError.CreateError($"No customer found with id {customer.Id}"));

                await _customerService.UpdateOneAsync(customer);
                await _customerService.SaveAsync();

                customer = await _customerService.GetOneAsync(customer.Id);

                var dto = _mapper.Map<CustomerDTO>(customer);

                return CreatedAtRoute("customer", new { id = customer.Id }, dto);
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _customerService.GetOneAsync(id);

                if (customer == null)
                    return NotFound(ValidationError.CreateError($"Customer with Id = {id} not found."));

                await _customerService.DeleteOneAsync(id);
                await _customerService.SaveAsync();

                return NoContent();
            } catch (Exception ex)
            {
                return Generate500ServerError(ex);
            }
        }

    }
}
