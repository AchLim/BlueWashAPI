using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyController(ILogger<CurrencyController> logger, ICurrencyRepository currencyRepository)
        {
            _logger = logger;
            _currencyRepository = currencyRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Currency>> GetAllCurrencies()
        {
            IEnumerable<Currency> currencies = await _currencyRepository.GetAllCurrencies();
            return Ok(currencies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrencyById(Guid id)
        {
            Currency? currency = await _currencyRepository.GetCurrencyById(id);
            return Ok(currency);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Currency>> PostCurrency([FromBody] CurrencyDto currencyDto)
        {
            CurrencyMapper currencyMapper = new();
            Currency currency = currencyMapper.CurrencyDtoToCurrency(currencyDto);
            await _currencyRepository.InsertCurrency(currency);

            return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Currency>> UpdateCurrency(Guid id, [FromBody] CurrencyUpdateDto currencyDto)
        {
            if (id != currencyDto.Id)
                return BadRequest("ID Mata Uang tidak cocok!");

            Currency? currency = await _currencyRepository.GetCurrencyById(id);
            if (currency is null)
                return BadRequest($"Mata Uang dengan id: {id} tidak ditemukan");

            currencyDto.PassData(ref currency);
            await _currencyRepository.UpdateCurrency(currency);

            return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCurrency(Guid id)
        {
            Currency? currency = await _currencyRepository.GetCurrencyById(id);
            if (currency is null)
                return BadRequest($"Data Currency dengan id: {id} tidak ditemukan!");

            await _currencyRepository.DeleteCurrency(currency);

            return Ok();
        }
    }
}