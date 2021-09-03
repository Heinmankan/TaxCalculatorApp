using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculatorAPI.Models;
using TaxCalculatorDB;
using TaxCalculatorDB.Models;

namespace TaxCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostalCodeTaxCalculationsController : ControllerBase
    {
        private readonly ILogger<PostalCodeTaxCalculationsController> _logger;
        private readonly TaxCalculationDbContext _taxCalculationDbContext;
        private readonly IMapper _mapper;

        public PostalCodeTaxCalculationsController(ILoggerFactory loggerFactory, TaxCalculationDbContext taxCalculationDbContext, IMapper mapper)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<PostalCodeTaxCalculationsController>();
            _taxCalculationDbContext = taxCalculationDbContext ?? throw new ArgumentNullException(nameof(taxCalculationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PostalCodeTaxCalculationFullDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taxCalculationDbContext.PostalCodeTaxCalculationLinks.Select(x => _mapper.Map<PostalCodeTaxCalculationFullDto>(x)).ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostalCodeTaxCalculationFullDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _taxCalculationDbContext.PostalCodeTaxCalculationLinks.FindAsync(id);

            if (result == null)
            {
                return NoContent();
            }

            var resultDto = _mapper.Map<PostalCodeTaxCalculationFullDto>(result);
            return Ok(resultDto);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(PostalCodeTaxCalculationDto postalCodeTaxCalculationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postalCodeTaxCalculation = _mapper.Map<PostalCodeTaxCalculation>(postalCodeTaxCalculationDto);
            _taxCalculationDbContext.PostalCodeTaxCalculationLinks.Add(postalCodeTaxCalculation);
            await _taxCalculationDbContext.SaveChangesAsync();

            return Ok(postalCodeTaxCalculation.Id);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(long id, PostalCodeTaxCalculationFullDto postalCodeTaxCalculationDto)
        {
            if (id != postalCodeTaxCalculationDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _taxCalculationDbContext.Entry(_mapper.Map<PostalCodeTaxCalculation>(postalCodeTaxCalculationDto)).State = EntityState.Modified;

            try
            {
                await _taxCalculationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostalCodeTaxCalculationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePostalCodeTaxCalculation(long id)
        {
            var postalCodeTaxCalculation = await _taxCalculationDbContext.PostalCodeTaxCalculationLinks.FindAsync(id);

            if (postalCodeTaxCalculation == null)
            {
                return NotFound();
            }

            _taxCalculationDbContext.PostalCodeTaxCalculationLinks.Remove(postalCodeTaxCalculation);
            await _taxCalculationDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PostalCodeTaxCalculationExists(long id)
        {
            return _taxCalculationDbContext.PostalCodeTaxCalculationLinks.Any(x => x.Id == id);
        }
    }
}
