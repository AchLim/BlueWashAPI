using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseAPI.Data;
using PurchaseAPI.Models;
using PurchaseAPI.Models.DTO;
using PurchaseAPI.Models.Mapper;
using PurchaseAPI.Utility;

namespace PurchaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly PurchaseDbContext _context;

        public CurrencyController(ILogger<CurrencyController> logger, PurchaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Currency>> GetAllCurrencies()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var currencies = await _context.Currencies.ToListAsync();
                    await transaction.CommitAsync();
                    return currencies == null ? NotFound(currencies) : Ok(currencies);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrencyById(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var currency = await _context.Currencies.Where(c => c.Id == id).FirstOrDefaultAsync();
                    await transaction.CommitAsync();

                    return currency == null ? NotFound(currency) : Ok(currency);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Currency>> PostCurrency(CurrencyDto currencyDto)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var currencyMapper = new CurrencyMapper();
                    Currency currency = currencyMapper.CurrencyDtoToCurrency(currencyDto);

                    _context.Currencies.Add(currency);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Currency>> UpdateCurrency(int id, Currency currency)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (id != currency.Id)
                        return BadRequest("Currency ID mismatch!");

                    Currency? currencyToUpdate = await _context.Currencies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                    if (currencyToUpdate == null)
                        return NotFound("Currency is not found!");

                    _context.Currencies.Update(currency);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

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
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Currency? currencyToDelete = await _context.Currencies.FirstOrDefaultAsync(c => c.Id == id);
                    if (currencyToDelete == null)
                        return NotFound("Currency is not found!");

                    _context.Currencies.Remove(currencyToDelete);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

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