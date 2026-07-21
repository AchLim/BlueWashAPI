using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Route("[controller]")]
    public sealed class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportRepository _reportRepository;

        public ReportController(ILogger<ReportController> logger, IReportRepository reportRepository)
        {
            _logger = logger;
            _reportRepository = reportRepository;
        }

        [HttpPost("general-ledger")]
        public async Task<ActionResult<GeneralLedgerContainer>> GetGeneralLedgerReport([FromBody] ReportOption option)
        {
            IEnumerable<GeneralLedgerContainer> generalLedgerData = await _reportRepository.GetGeneralLedgerData(option.DateFrom, option.DateTo);
            return Ok(generalLedgerData);
        }

        [HttpPost("trial-balance")]
        public async Task<ActionResult<TrialBalanceContainer>> GetTrialBalanceReport([FromBody] ReportOption option)
        {
            IEnumerable<TrialBalanceContainer> trialBalanceData = await _reportRepository.GetTrialBalanceData(option.DateFrom, option.DateTo);
            return Ok(trialBalanceData);
        }

        [HttpPost("income-statement")]
        public async Task<ActionResult<IncomeStatementContainer>> GetIncomeStatementReport([FromBody] ReportOption option)
        {
            IEnumerable<IncomeStatementContainer> incomeStatementData = await _reportRepository.GetIncomeStatementData(option.DateFrom, option.DateTo);
            return Ok(incomeStatementData);
        }

        [HttpPost("balance-sheet")]
        public async Task<ActionResult<BalanceSheetContainer>> GetBalanceSheetReport([FromBody] ReportOption option)
        {
            IEnumerable<BalanceSheetContainer> incomeStatementData = await _reportRepository.GetBalanceSheetData(option.DateFrom, option.DateTo);
            return Ok(incomeStatementData);
        }

        [HttpPost("purchase")]
        public async Task<ActionResult<PurchaseReportContainer>> GetPurchaseReport([FromBody] ReportOption option)
        {
            IEnumerable<PurchaseReportContainer> purchaseReportData = await _reportRepository.GetPurchaseReportData(option.DateFrom, option.DateTo);
            return Ok(purchaseReportData);
        }

        [HttpPost("sales")]
        public async Task<ActionResult<SalesReportContainer>> GetSalesReport([FromBody] ReportOption option)
        {
            IEnumerable<SalesReportContainer> salesReportData = await _reportRepository.GetSalesReportData(option.DateFrom, option.DateTo);
            return Ok(salesReportData);
        }
    }

    public class ReportOption
    {
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
    }
}