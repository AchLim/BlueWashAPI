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
    public class BankAccountController : ControllerBase
    {
        private readonly ILogger<BankAccountController> _logger;
        private readonly PurchaseDbContext _context;

        public BankAccountController(ILogger<BankAccountController> logger, PurchaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<BankAccount>> GetAllBankAccounts()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var bankAccounts = await _context.BankAccounts.Include(b => b.Bank).ToListAsync();
                    await transaction.CommitAsync();
                    return bankAccounts == null ? NotFound(bankAccounts) : Ok(bankAccounts);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccountById(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var bankAccount = await _context.BankAccounts.Include(b => b.Bank).Where(b => b.Id == id).FirstOrDefaultAsync();
                    await transaction.CommitAsync();

                    return bankAccount == null ? NotFound(bankAccount) : Ok(bankAccount);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("insert_bank_account")]
        public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccountDto bankAccountDto)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var bankAccountMapper = new BankAccountMapper();
                    BankAccount bankAccount = bankAccountMapper.BankAccountDtoToBankAccount(bankAccountDto);

                    _context.BankAccounts.Add(bankAccount);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetBankAccountById), new { id = bankAccount.Id }, bankAccount);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("update_bank_account/{id}")]
        public async Task<ActionResult<BankAccount>> UpdateBankAccount(int id, BankAccount bankAccount)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (id != bankAccount.Id)
                        return BadRequest("Bank Account ID mismatch!");

                    BankAccount? bankAccountToUpdate = await _context.BankAccounts.Include(ba => ba.BankId).FirstOrDefaultAsync(b => b.Id == id);
                    if (bankAccountToUpdate == null)
                        return NotFound("Bank Account is not found!");

                    bankAccount.PassValues(ref bankAccountToUpdate);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetBankAccountById), new { id = bankAccountToUpdate.Id }, bankAccountToUpdate);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("delete_bank_account/{id}")]
        public async Task<ActionResult> DeleteBankAccount(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    BankAccount? bankAccountToDelete = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == id);
                    if (bankAccountToDelete == null)
                        return NotFound("Bank Account is not found!");

                    _context.BankAccounts.Remove(bankAccountToDelete);

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