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
    [Route("general-journal")]
    public sealed class GeneralJournalController : ControllerBase
    {
        private readonly ILogger<GeneralJournalController> _logger;
        private readonly IGeneralJournalRepository _generalJournalRepository;

        public GeneralJournalController(ILogger<GeneralJournalController> logger, IGeneralJournalRepository generalJournalRepository)
        {
            _logger = logger;
            _generalJournalRepository = generalJournalRepository;
        }

        [HttpGet("header/all")]
        public async Task<ActionResult<GeneralJournalHeader>> GetAllGeneralJournalHeaders()
        {
            IEnumerable<GeneralJournalHeader> generalJournals = await _generalJournalRepository.GetAllGeneralJournalHeaders();
            return Ok(generalJournals);
        }

        [HttpGet("header/{id}")]
        public async Task<ActionResult<GeneralJournalHeader>> GetGeneralJournalHeaderById(Guid id)
        {
            GeneralJournalHeader? currency = await _generalJournalRepository.GetGeneralJournalHeaderById(id);
            return Ok(currency);
        }

        [HttpPost("header/insert")]
        public async Task<ActionResult<GeneralJournalHeader>> PostGeneralJournalHeader([FromBody] GeneralJournalHeaderDto generalJournalHeaderDto)
        {
            GeneralJournalHeaderMapper generalJournalHeaderMapper = new();
            GeneralJournalHeader generalJournalHeader = generalJournalHeaderMapper.GeneralJournalHeaderDtoToGeneralJournalHeader(generalJournalHeaderDto);
            await _generalJournalRepository.InsertGeneralJournalHeader(generalJournalHeader);

            return CreatedAtAction(nameof(GetGeneralJournalHeaderById), new { id = generalJournalHeader.Id }, generalJournalHeader);
        }

        [HttpPut("header/update/{id}")]
        public async Task<ActionResult<GeneralJournalHeader>> UpdateGeneralJournalHeader(Guid id, [FromBody] GeneralJournalHeaderUpdateDto generalJournalHeaderDto)
        {
            if (id != generalJournalHeaderDto.Id)
                return BadRequest("ID Mata Uang tidak cocok!");

            GeneralJournalHeader? generalJournalHeader = await _generalJournalRepository.GetGeneralJournalHeaderById(id);
            if (generalJournalHeader is null)
                return BadRequest($"Mata Uang dengan id: {id} tidak ditemukan");

            generalJournalHeaderDto.PassData(ref generalJournalHeader);
            await _generalJournalRepository.UpdateGeneralJournalHeader(generalJournalHeader);

            return CreatedAtAction(nameof(GetGeneralJournalHeaderById), new { id = generalJournalHeader.Id }, generalJournalHeader);
        }

        [HttpDelete("header/delete/{id}")]
        public async Task<ActionResult> DeleteGeneralJournalHeader(Guid id)
        {
            GeneralJournalHeader? generalJournalHeader = await _generalJournalRepository.GetGeneralJournalHeaderById(id);
            if (generalJournalHeader is null)
                return BadRequest($"Data header jurnal umum dengan id: {id} tidak ditemukan!");

            await _generalJournalRepository.DeleteGeneralJournalHeader(generalJournalHeader);

            return Ok();
        }


        // This should be All Journal Data, please rename it lim.
        //[HttpGet("get_general_journal")]
        //[AllowAnonymous]
        //public async Task<ActionResult<GeneralJournalContainer>> GetGeneralJournalReport()
        //{
        //    IEnumerable<GeneralJournalContainer> generalJournalData = await _generalJournalRepository.GetGeneralJournalData();
        //    return Ok(generalJournalData);
        //}
    }
}