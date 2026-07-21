using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Route("journal-entry")]
    public sealed class JournalEntryController : ControllerBase
    {
        private readonly ILogger<JournalEntryController> _logger;
        private readonly IJournalEntryRepository _journalEntryRepository;

        public JournalEntryController(ILogger<JournalEntryController> logger, IJournalEntryRepository journalEntryRepository)
        {
            _logger = logger;
            _journalEntryRepository = journalEntryRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<JournalEntry>> GetAllJournalEntriesAsync()
        {
            IEnumerable<JournalEntry> journalEntries = await _journalEntryRepository.GetAllJournalEntries();
            return Ok(journalEntries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JournalEntry>> GetJournalEntryById(Guid id)
        {
            JournalEntry? journalEntry = await _journalEntryRepository.GetJournalEntryById(id);
            return Ok(journalEntry);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<JournalEntry>> PostJournalEntry([FromBody] JournalEntryDto journalEntryDto)
        {
            JournalEntry journalEntry = JournalEntryMapper.JournalEntryDtoToJournalEntry(journalEntryDto);

            bool closingEntryExist = await _journalEntryRepository.ClosingEntryExist(journalEntry.TransactionDate);
            if (closingEntryExist)
                throw new DatabaseInsertException("Periode yang terpilih sudah ditutup, tidak dapat melakukan penambahan data.");

            await _journalEntryRepository.InsertJournalEntry(journalEntry);

            return CreatedAtAction(nameof(GetJournalEntryById), new { id = journalEntry.Id }, journalEntry);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<JournalEntry>> UpdateJournalEntry(Guid id, [FromBody] JournalEntryUpdateDto journalEntryUpdateDto)
        {
            if (id != journalEntryUpdateDto.Id)
                return BadRequest("ID entri jurnal tidak cocok!");

            JournalEntry? journalEntry = await _journalEntryRepository.GetJournalEntryById(id);
            if (journalEntry is null)
                return BadRequest($"Entri jurnal dengan id: {id} tidak ditemukan");

            journalEntryUpdateDto.PassData(ref journalEntry);

            bool closingEntryExist = await _journalEntryRepository.ClosingEntryExist(journalEntry.TransactionDate);
            if (closingEntryExist)
                throw new DatabaseUpdateException("Periode yang terpilih sudah ditutup, tidak dapat melakukan perubahan data.");

            await _journalEntryRepository.UpdateJournalEntry(journalEntry);

            journalEntry = await _journalEntryRepository.GetJournalEntryById(id);

            return CreatedAtAction(nameof(GetJournalEntryById), new { id = journalEntry!.Id }, journalEntry!);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteJournalEntry(Guid id)
        {
            JournalEntry? journalEntry = await _journalEntryRepository.GetJournalEntryById(id);
            if (journalEntry is null)
                return BadRequest($"Data jurnal dengan id: {id} tidak ditemukan!");

            bool closingEntryExist = await _journalEntryRepository.ClosingEntryExist(journalEntry.TransactionDate);
            if (closingEntryExist)
                throw new DatabaseDeleteException("Periode yang terpilih sudah ditutup, tidak dapat menghapus entri.");

            if (journalEntry.Status == EntryStatus.Posted.ToString())
                throw new DatabaseDeleteException("Tidak dapat menghapus entri yang sudah di posting!");

            await _journalEntryRepository.DeleteJournalEntry(journalEntry);

            return Ok();
        }

        [HttpGet("purchase/{purchaseHeaderId}")]
        public async Task<ActionResult<PurchaseEntryContainer>> GetPurchaseReportById(Guid purchaseHeaderId)
        {
            IEnumerable<PurchaseEntryContainer> purchaseReportData = await _journalEntryRepository.GetPurchaseQueryById(purchaseHeaderId);
            return Ok(purchaseReportData);
        }

        [HttpGet("sales/{salesHeaderId}")]
        public async Task<ActionResult<SalesEntryContainer>> GetSalesReportById(Guid salesHeaderId)
        {
            IEnumerable<SalesEntryContainer> salesReportData = await _journalEntryRepository.GetSalesQueryById(salesHeaderId);
            return Ok(salesReportData);
        }


        // This should be All Journal Data, please rename it lim.
        //[HttpGet("get_general_journal")]
        //[AllowAnonymous]
        //public async Task<ActionResult<JournalEntryContainer>> GetGeneralJournalReport()
        //{
        //    IEnumerable<JournalEntryContainer> generalJournalData = await _journalEntryRepository.GetJournalEntryData();
        //    return Ok(generalJournalData);
        //}
    }
}