using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("[controller]")]
    public sealed class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardRepository _reportRepository;

        public DashboardController(ILogger<DashboardController> logger, IDashboardRepository reportRepository)
        {
            _logger = logger;
            _reportRepository = reportRepository;
        }

        [HttpPost("summary")]
        public async Task<ActionResult<object[]>> GetSummaryDashboard([FromBody] DashboardOption option)
        {
            IncomeDashboardContainer incomeDashboardContainer = await _reportRepository.GetIncomeDashboardData(option.DateFrom, option.DateTo);
            ExpenseDashboardContainer expenseDashboardContainer = await _reportRepository.GetExpenseDashboardData(option.DateFrom, option.DateTo);
            TotalOrderDashboardContainer totalOrderDashboardContainer = await _reportRepository.GetTotalOrderDashboardData(option.DateFrom, option.DateTo);
            AccountReceivableDashboardContainer accountReceivableDashboardContainer = await _reportRepository.GetAccountReceivableDashboardData(option.DateFrom, option.DateTo);

            return Ok(new object[] { incomeDashboardContainer, expenseDashboardContainer, totalOrderDashboardContainer, accountReceivableDashboardContainer });
        }

        [HttpPost("sales")]
        public async Task<ActionResult<IEnumerable<IEnumerable<Decimal>>>> GetSalesDashboard([FromBody] DashboardOption option)
        {
            IEnumerable<Decimal> cashSalesDashboardData = await _reportRepository.GetCashSalesDashboardData(option.DateFrom, option.DateTo);
            IEnumerable<Decimal> bankSalesDashboardData = await _reportRepository.GetBankSalesDashboardData(option.DateFrom, option.DateTo);
            List<IEnumerable<Decimal>> salesDashboardData = new(2)
            {
                cashSalesDashboardData,
                bankSalesDashboardData
            };
            return Ok(salesDashboardData);
        }

        [HttpPost("sales-order")]
        public async Task<ActionResult<IEnumerable<IEnumerable<int>>>> GetSalesOrderDashboard([FromBody] DashboardOption option)
        {
            IEnumerable<int> salesOrderDashboardData = await _reportRepository.GetSalesOrderDashboardData(option.DateFrom, option.DateTo);
            return Ok(salesOrderDashboardData);
        }

        [HttpPost("payment-by-type")]
        public async Task<ActionResult<IEnumerable<PaymentByTypeContainer>>> GetPaymentByTypeDashboard([FromBody] DashboardOption option)
        {
            IEnumerable<PaymentByTypeContainer> paymentByTypeDashboard = await _reportRepository.GetPaymentByTypeDashboardData(option.DateFrom, option.DateTo);
            return Ok(paymentByTypeDashboard);
        }
    }

    public class DashboardOption
    {
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
    }
}