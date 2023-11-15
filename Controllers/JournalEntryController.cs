using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DAL;
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
            await _journalEntryRepository.UpdateJournalEntry(journalEntry);

            return CreatedAtAction(nameof(GetJournalEntryById), new { id = journalEntry.Id }, journalEntry);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteJournalEntry(Guid id)
        {
            JournalEntry? journalEntry = await _journalEntryRepository.GetJournalEntryById(id);
            if (journalEntry is null)
                return BadRequest($"Data jurnal dengan id: {id} tidak ditemukan!");

            await _journalEntryRepository.DeleteJournalEntry(journalEntry);

            return Ok();
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