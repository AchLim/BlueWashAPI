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
    [Route("[controller]")]
    public sealed class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly ISalesRepository _salesRepository;

        public SalesController(ILogger<SalesController> logger, ISalesRepository salesRepository)
        {
            _logger = logger;
            _salesRepository = salesRepository;
        }

        [HttpGet("sales_report")]
        [AllowAnonymous]
        public async Task<ActionResult<SalesReportContainer>> GetSalesReport()
        {
            IEnumerable<SalesReportContainer> salesReport = await _salesRepository.GetSalesData();
            return Ok(salesReport);
        }

        [HttpGet("get_total_item_sales")]
        [AllowAnonymous]
        public async Task<ActionResult<ItemSalesContainer>> GetTotalItemSales()
        {
            IEnumerable<ItemSalesContainer> totalItemSales = await _salesRepository.GetTotalItemSalesData();
            return Ok(totalItemSales);
        }

        [HttpGet("get_total_sales_per_invoice")]
        [AllowAnonymous]
        public async Task<ActionResult<SalesPerInvoiceContainer>> GetTotalSalesPerInvoice()
        {
            IEnumerable<SalesPerInvoiceContainer> salesPerInvoices = await _salesRepository.GetTotalSalesPerInvoiceData();
            return Ok(salesPerInvoices);
        }

        [HttpGet("get_total_sales_payment")]
        [AllowAnonymous]
        public async Task<ActionResult<SalesPaymentContainer>> GetTotalSalesPayment()
        {
            IEnumerable<SalesPaymentContainer> totalSalesPayment = await _salesRepository.GetTotalSalesPaymentData();
            return Ok(totalSalesPayment);
        }

        [HttpGet("get_ar_balance_report")]
        [AllowAnonymous]
        public async Task<ActionResult<AccountReceivableBalanceContainer>> GetAccountReceivableBalanceReport()
        {
            IEnumerable<AccountReceivableBalanceContainer> accountReceivableBalanceReport = await _salesRepository.GetAccountReceivableBalanceData();
            return Ok(accountReceivableBalanceReport);
        }
    }
}