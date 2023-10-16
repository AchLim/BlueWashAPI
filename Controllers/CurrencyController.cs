using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.DAL;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly IRepository<Currency> _currencyRepository;

        public CurrencyController(ILogger<CurrencyController> logger, IRepository<Currency> currencyRepository)
        {
            _logger = logger;
            _currencyRepository = currencyRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Currency>> GetAllCurrencies()
        {
            await using (var transaction = await _currencyRepository.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var currencies = await _currencyRepository.GetAll().ToListAsync();
                    await _currencyRepository.UnitOfWork.CommitAsync(transaction);
                    return currencies == null ? NotFound(currencies) : Ok(currencies);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrencyById(Guid id)
        {
            await using (var transaction = await _currencyRepository.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var currency = await _currencyRepository.GetByIdAsync(id);
                    await _currencyRepository.UnitOfWork.CommitAsync(transaction);

                    return currency == null ? NotFound(currency) : Ok(currency);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Currency>> PostCurrency([FromBody] CurrencyDto currencyDto)
        {
            await using (var transaction = await _currencyRepository.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var currencyMapper = new CurrencyMapper();
                    Currency currency = currencyMapper.CurrencyDtoToCurrency(currencyDto);
                    await _currencyRepository.AddAsync(currency);
                    await _currencyRepository.UnitOfWork.CommitAsync(transaction);

                    return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Currency>> UpdateCurrency(Guid id, [FromBody] CurrencyUpdateDto currencyDto)
        {
            await using (var transaction = await _currencyRepository.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    if (id != currencyDto.Id)
                        return BadRequest("ID Mata Uang tidak cocok!");

                    Currency? currency = await _currencyRepository.GetByIdAsync(id);
                    if (currency == null)
                        return NotFound("Mata Uang tidak ditemukan!");

                    currencyDto.PassData(ref currency);

                    await _currencyRepository.UpdateAsync(currency);
                    await _currencyRepository.UnitOfWork.CommitAsync(transaction);

                    return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCurrency(int id)
        {
            await using (var transaction = await _currencyRepository.UnitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Currency? currency = await _currencyRepository.GetByIdAsync(id);
                    if (currency == null)
                        return NotFound("Currency is not found!");

                    await _currencyRepository.DeleteAsync(currency);
                    await _currencyRepository.UnitOfWork.CommitAsync(transaction);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}