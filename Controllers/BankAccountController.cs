//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebAPI.Data;
//using WebAPI.Models;
//using WebAPI.Models.DTO;
//using WebAPI.Models.Mapper;
//using WebAPI.Utility;

//namespace WebAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class BankAccountController : ControllerBase
//    {
//        private readonly ILogger<BankAccountController> _logger;
//        private readonly ApplicationContext _context;

//        public BankAccountController(ILogger<BankAccountController> logger, ApplicationContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        [HttpGet("all")]
//        public async Task<ActionResult<BankAccount>> GetAllBankAccounts()
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    var bankAccounts = await _context.BankAccounts.Include(b => b.Bank).ToListAsync();
//                    await transaction.CommitAsync();
//                    return bankAccounts == null ? NotFound(bankAccounts) : Ok(bankAccounts);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<BankAccount>> GetBankAccountById(int id)
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    var bankAccount = await _context.BankAccounts.Include(b => b.Bank).Where(b => b.Id == id).FirstOrDefaultAsync();
//                    await transaction.CommitAsync();

//                    return bankAccount == null ? NotFound(bankAccount) : Ok(bankAccount);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpPost("insert")]
//        public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccountDto bankAccountDto)
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    var bankAccountMapper = new BankAccountMapper();
//                    BankAccount bankAccount = bankAccountMapper.BankAccountDtoToBankAccount(bankAccountDto);

//                    _context.BankAccounts.Add(bankAccount);
//                    await _context.SaveChangesAsync();

//                    await transaction.CommitAsync();

//                    return CreatedAtAction(nameof(GetBankAccountById), new { id = bankAccount.Id }, bankAccount);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpPut("update/{id}")]
//        public async Task<ActionResult<BankAccount>> UpdateBankAccount(int id, BankAccountUpdateDto bankAccountUpdateDto)
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    if (id != bankAccountUpdateDto.Id)
//                        return BadRequest("Bank Account ID mismatch!");

//                    BankAccount? bankAccountToUpdate = await _context.BankAccounts.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
//                    if (bankAccountToUpdate == null)
//                        return NotFound("Bank Account is not found!");

//                    var bankAccountMapper = new BankAccountMapper();
//                    BankAccount bankAccount = bankAccountMapper.BankAccountUpdateDtoToBankAccount(bankAccountUpdateDto);

//                    _context.BankAccounts.Update(bankAccount);

//                    await _context.SaveChangesAsync();
//                    await transaction.CommitAsync();

//                    return CreatedAtAction(nameof(GetBankAccountById), new { id = bankAccount.Id }, bankAccount);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpDelete("delete/{id}")]
//        public async Task<ActionResult> DeleteBankAccount(int id)
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    BankAccount? bankAccountToDelete = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == id);
//                    if (bankAccountToDelete == null)
//                        return NotFound("Bank Account is not found!");

//                    _context.BankAccounts.Remove(bankAccountToDelete);

//                    await _context.SaveChangesAsync();
//                    await transaction.CommitAsync();

//                    return NoContent();
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }
//    }
//}