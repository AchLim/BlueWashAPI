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
    public class SalesController : ControllerBase
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

        //[HttpGet("all")]
        //public async Task<ActionResult<Currency>> GetAllCurrencies()
        //{
        //    IEnumerable<Currency> currencies = await _currencyRepository.GetAllCurrencies();
        //    return Ok(currencies);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Currency>> GetCurrencyById(Guid id)
        //{
        //    Currency? currency = await _currencyRepository.GetCurrencyById(id);
        //    return Ok(currency);
        //}

        //[HttpPost("insert")]
        //public async Task<ActionResult<Currency>> PostCurrency([FromBody] CurrencyDto currencyDto)
        //{
        //    CurrencyMapper currencyMapper = new();
        //    Currency currency = currencyMapper.CurrencyDtoToCurrency(currencyDto);
        //    await _currencyRepository.InsertCurrency(currency);

        //    return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
        //}

        //[HttpPut("update/{id}")]
        //public async Task<ActionResult<Currency>> UpdateCurrency(Guid id, [FromBody] CurrencyUpdateDto currencyDto)
        //{
        //    if (id != currencyDto.Id)
        //        return BadRequest("ID Mata Uang tidak cocok!");

        //    Currency? currency = await _currencyRepository.GetCurrencyById(id);
        //    if (currency is null)
        //        return BadRequest($"Mata Uang dengan id: {id} tidak ditemukan");

        //    currencyDto.PassData(ref currency);
        //    await _currencyRepository.UpdateCurrency(currency);

        //    return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
        //}

        //[HttpDelete("delete/{id}")]
        //public async Task<ActionResult> DeleteCurrency(Guid id)
        //{
        //    Currency? currency = await _currencyRepository.GetCurrencyById(id);
        //    if (currency is null)
        //        return BadRequest($"Data Currency dengan id: {id} tidak ditemukan!");

        //    await _currencyRepository.DeleteCurrency(currency);

        //    return Ok();
        //}
    }
}