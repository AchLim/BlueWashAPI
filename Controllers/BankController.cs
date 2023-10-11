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
    public class BankController : ControllerBase
    {
        private readonly ILogger<BankController> _logger;
        private readonly PurchaseDbContext _context;

        public BankController(ILogger<BankController> logger, PurchaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Bank>> GetAllBanks()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var banks = await _context.Banks.ToListAsync();
                    await transaction.CommitAsync();

                    return banks == null ? NotFound(banks) : Ok(banks);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBankById(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var bank = await _context.Banks.Where(b => b.Id == id).FirstOrDefaultAsync();
                    await transaction.CommitAsync();

                    return bank == null ? NotFound(bank) : Ok(bank);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Bank>> PostBank(BankDto bankDto)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var bankMapper = new BankMapper();
                    Bank bank = bankMapper.BankDtoToBank(bankDto);

                    _context.Banks.Add(bank);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetBankById), new { id = bank.Id }, bank);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Bank>> UpdateBank(int id, Bank bank)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (id != bank.Id)
                        return BadRequest("Bank ID mismatch!");

                    Bank? bankToUpdate = await _context.Banks.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                    if (bankToUpdate == null)
                        return NotFound("Bank not found!");

                    _context.Banks.Update(bank);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetBankById), new { id = bankToUpdate.Id }, bankToUpdate);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteBank(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Bank? bankToDelete = await _context.Banks.FirstOrDefaultAsync(b => b.Id == id);
                    if (bankToDelete == null)
                        return NotFound("Bank is not found!");

                    _context.Banks.Remove(bankToDelete);

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