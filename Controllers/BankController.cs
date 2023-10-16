//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebAPI.DAL;
//using WebAPI.Data;
//using WebAPI.Models;
//using WebAPI.Models.DTO;
//using WebAPI.Models.Mapper;
//using WebAPI.Utility;

//namespace WebAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class BankController : ControllerBase
//    {
//        private readonly ILogger<BankController> _logger;
//        private readonly IRepository<Bank> _bankRepository;

//        public BankController(ILogger<BankController> logger, IRepository<Bank> bankRepository)
//        {
//            _logger = logger;
//            _bankRepository = bankRepository;
//        }

//        [HttpGet("all")]
//        public async Task<ActionResult<Bank>> GetAllBanks()
//        {
//            await using (var transaction = await _bankRepository.UnitOfWork.BeginTransactionAsync())
//            {
//                try
//                {
//                    var banks = await _bankRepository.GetAll().ToListAsync();
//                    await _bankRepository.UnitOfWork.CommitAsync(transaction);

//                    return banks is null ? NotFound(banks) : Ok(banks);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Bank>> GetBankById(int id)
//        {
//            await using (var transaction = await _bankRepository.UnitOfWork.BeginTransactionAsync())
//            {
//                try
//                {
//                    var bank = await _bankRepository.GetByIdAsync(id);
//                    await _bankRepository.UnitOfWork.CommitAsync(transaction);

//                    return bank is null ? NotFound(bank) : Ok(bank);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpPost("insert")]
//        public async Task<ActionResult<Bank>> PostBank([FromBody] BankDto bankDto)
//        {
//            if (bankDto is null)
//                return BadRequest();

//            await using (var transaction = await _bankRepository.UnitOfWork.BeginTransactionAsync())
//            {
//                try
//                {
//                    var bankMapper = new BankMapper();
//                    Bank bank = bankMapper.BankDtoToBank(bankDto);

//                    await _bankRepository.AddAsync(bank);

//                    await _bankRepository.UnitOfWork.CommitAsync(transaction);

//                    return CreatedAtAction(nameof(GetBankById), new { id = bank.Id }, bank);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpPut("update/{id}")]
//        public async Task<ActionResult<Bank>> UpdateBank(int id, [FromBody] BankUpdateDto bankDto)
//        {
//            if (id != bankDto.Id)
//                return BadRequest("Bank ID mismatch!");

//            await using (var transaction = await _bankRepository.UnitOfWork.BeginTransactionAsync())
//            {
//                try
//                {
//                    var bankToUpdate = await _bankRepository.GetByIdAsync(id);
//                    if (bankToUpdate is null)
//                        return NotFound("Bank not found!");

//                    var bankMapper = new BankMapper();
//                    Bank bank = bankMapper.BankUpdateDtoToBank(bankDto);

//                    bank.PassValues(ref bankToUpdate);

//                    await _bankRepository.UpdateAsync(bankToUpdate);

//                    await _bankRepository.UnitOfWork.CommitAsync(transaction);

//                    return CreatedAtAction(nameof(GetBankById), new { id = bankToUpdate.Id }, bankToUpdate);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpDelete("delete/{id}")]
//        public async Task<ActionResult> DeleteBank(int id)
//        {
//            await using (var transaction = await _bankRepository.UnitOfWork.BeginTransactionAsync())
//            {
//                try
//                {
//                    Bank? bankToDelete = await _bankRepository.GetByIdAsync(id);
//                    if (bankToDelete == null)
//                        return NotFound("Bank is not found!");

//                    await _bankRepository.DeleteAsync(bankToDelete);
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