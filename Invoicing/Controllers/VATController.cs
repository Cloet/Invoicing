using AutoMapper;
using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services;
using Invoicing.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VATController: ControllerBase
    {

        private readonly ILogger<VATController> _logger;
        private readonly IVATService _vatService;
        private readonly IMapper _mapper;

        public VATController(IMapper mapper, ILogger<VATController> logger, IVATService vatService)
        {
            _mapper = mapper;
            _logger = logger;
            _vatService = vatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVAT()
        {
            try
            {
                var vats = await _vatService.GetAllAsync();

                if (!vats.Any())
                    return NoContent();

                var dto = _mapper.Map<IEnumerable<VatDTO>>(vats);

                return Ok(dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

        [HttpGet("{id}", Name ="vat")]
        public async Task<IActionResult> GetVAT(int id)
        {
            try
            {
                var vat = await _vatService.GetOneAsync(id);

                if (vat == null)
                    return NotFound(ValidationError.CreateError($"VAT with Id = {id} not found."));

                var dto = _mapper.Map<VatDTO>(vat);

                return Ok(dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostVAT(VAT vat)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var v = await _vatService.FilterAsync(x => x.Code == vat.Code, 1);

                if (v.Count() > 0)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, ValidationError.CreateError($"VAT with code = {vat.Code} already exists."));

                await _vatService.CreateOneAsync(vat);
                await _vatService.SaveAsync();

                vat = await _vatService.GetOneAsync(vat.Id);
                var dto = _mapper.Map<VatDTO>(vat);

                return CreatedAtRoute("vat", new { id = vat.Id }, dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVAT(int id, VAT vat)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                vat.Id = id;
                var v = await _vatService.GetOneAsync(vat.Id);

                if (v == null)
                    return NotFound(ValidationError.CreateError($"No vat found with id {vat.Id}."));

                await _vatService.UpdateOneAsync(vat);
                await _vatService.SaveAsync();

                vat = await _vatService.GetOneAsync(vat.Id);
                var dto = _mapper.Map<VatDTO>(vat);

                return CreatedAtRoute("vat", new { id = vat.Id }, dto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVAT(int id)
        {
            try
            {
                var vat = await _vatService.GetOneAsync(id);

                if (vat == null)
                    return NotFound(ValidationError.CreateError($"VAT with Id = {id} not found."));

                await _vatService.DeleteOneAsync(id);
                await _vatService.SaveAsync();

                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ValidationError.CreateError(ex.Message));
            }
        }

    }
}
