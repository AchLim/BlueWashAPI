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
    [Route("chart-of-account")]
    public class ChartOfAccountController : ControllerBase
    {
        private readonly ILogger<ChartOfAccountController> _logger;
        private readonly IChartOfAccountRepository _chartOfAccountRepository;

        public ChartOfAccountController(ILogger<ChartOfAccountController> logger, IChartOfAccountRepository chartOfAccountRepository)
        {
            _logger = logger;
            _chartOfAccountRepository = chartOfAccountRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ChartOfAccount>> GetAllChartOfAccounts()
        {
            IEnumerable<ChartOfAccount> chartOfAccounts = await _chartOfAccountRepository.GetAllChartOfAccounts();
            return Ok(chartOfAccounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChartOfAccount>> GetChartOfAccountById(Guid id)
        {
            ChartOfAccount? chartOfAccount = await _chartOfAccountRepository.GetChartOfAccountById(id);
            return Ok(chartOfAccount);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<ChartOfAccount>> PostChartOfAccount([FromBody] ChartOfAccountDto chartOfAccountDto)
        {
            ChartOfAccountMapper chartOfAccountMapper = new();
            ChartOfAccount chartOfAccount = chartOfAccountMapper.ChartOfAccountDtoToChartOfAccount(chartOfAccountDto);
            await _chartOfAccountRepository.InsertChartOfAccount(chartOfAccount);

            return CreatedAtAction(nameof(GetChartOfAccountById), new { id = chartOfAccount.Id }, chartOfAccount);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<ChartOfAccount>> UpdateChartOfAccount(Guid id, [FromBody] ChartOfAccountUpdateDto chartOfAccountDto)
        {
            if (id != chartOfAccountDto.Id)
                return BadRequest("ID Pelanggan tidak cocok!");

            ChartOfAccount? chartOfAccount = await _chartOfAccountRepository.GetChartOfAccountById(id);
            if (chartOfAccount is null)
                return BadRequest($"Pelanggan dengan id: {id} tidak ditemukan");

            chartOfAccountDto.PassData(ref chartOfAccount);
            await _chartOfAccountRepository.UpdateChartOfAccount(chartOfAccount);

            return CreatedAtAction(nameof(GetChartOfAccountById), new { id = chartOfAccount.Id }, chartOfAccount);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteChartOfAccount (Guid id)
        {
            ChartOfAccount? chartOfAccount = await _chartOfAccountRepository.GetChartOfAccountById(id);
            if (chartOfAccount is null)
                return BadRequest($"Data Pelanggan dengan id: {id} tidak ditemukan!");

            await _chartOfAccountRepository.DeleteChartOfAccount(chartOfAccount);

            return Ok();
        }
    }
}