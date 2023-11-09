using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("general_journal")]
    public sealed class GeneralJournalController : ControllerBase
    {
        private readonly ILogger<GeneralJournalController> _logger;
        private readonly IGeneralJournalRepository _generalJournalRepository;

        public GeneralJournalController(ILogger<GeneralJournalController> logger, IGeneralJournalRepository generalJournalRepository)
        {
            _logger = logger;
            _generalJournalRepository = generalJournalRepository;
        }

        [HttpGet("get_general_journal")]
        [AllowAnonymous]
        public async Task<ActionResult<GeneralJournalContainer>> GetGeneralJournalReport()
        {
            IEnumerable<GeneralJournalContainer> generalJournalData = await _generalJournalRepository.GetGeneralJournalData();
            return Ok(generalJournalData);
        }
    }
}