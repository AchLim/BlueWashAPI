using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;
using WebAPI.Data.Enum;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("closing-entry")]
    public sealed class ClosingEntryController : ControllerBase
    {
        private readonly ILogger<ClosingEntryController> _logger;
        private readonly IClosingEntryRepository _closingEntryRepository;

        public ClosingEntryController(ILogger<ClosingEntryController> logger, IClosingEntryRepository closingEntryRepository)
        {
            _logger = logger;
            _closingEntryRepository = closingEntryRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ClosingEntry>> GetAllJournalEntriesAsync()
        {
            IEnumerable<ClosingEntry> journalEntries = await _closingEntryRepository.GetAllClosingEntries();
            return Ok(journalEntries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClosingEntry>> GetClosingEntryById(Guid id)
        {
            ClosingEntry? closingEntry = await _closingEntryRepository.GetClosingEntryById(id);
            return Ok(closingEntry);
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteClosingEntry(Guid id)
        {
            ClosingEntry? closingEntry = await _closingEntryRepository.GetClosingEntryById(id);
            if (closingEntry is null)
                return BadRequest($"Data jurnal penutup dengan id: {id} tidak ditemukan!");

            await _closingEntryRepository.DeleteClosingEntry(closingEntry);

            return Ok();
        }

        [HttpPost("close-entry")]
        public async Task<ActionResult<ClosingEntry>> CloseEntry(EntryOption option)
        {
            bool entryExist = await _closingEntryRepository.ClosingEntryExist(option.StartDate, option.EndDate);
            if (entryExist)
                throw new DatabaseUniqueConstraintException("Jurnal Penutup pada periode yang terpilih sudah ada!");

            bool unpostedEntryExist = await _closingEntryRepository.UnpostedEntryExist(option.StartDate, option.EndDate);
            if (unpostedEntryExist)
                throw new DatabaseUniqueConstraintException("Tidak dapat menutup jurnal karena terdapat jurnal / pembelian / penjualan yang belum diposting pada periode yang terpilih!");


            ClosingEntry closingEntry = await _closingEntryRepository.CreateClosingEntry(option.StartDate, option.EndDate);

            return CreatedAtAction(nameof(GetClosingEntryById), new { id = closingEntry.Id }, closingEntry);
        }
    }

    public class EntryOption
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}