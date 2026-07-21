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

        [HttpGet("header/all")]
        public async Task<ActionResult<SalesHeader>> GetAllSalesHeaders()
        {
            IEnumerable<SalesHeader> salesHeaders = await _salesRepository.GetAllSalesHeaders();
            return Ok(salesHeaders);
        }

        [HttpGet("header/{id}")]
        public async Task<ActionResult<SalesHeader>> GetSalesHeaderById(Guid id)
        {
            SalesHeader? salesHeader = await _salesRepository.GetSalesHeaderById(id);
            return Ok(salesHeader);
        }

        [HttpPost("header/insert")]
        public async Task<ActionResult<SalesHeader>> PostSalesHeader([FromBody] SalesHeaderDto salesHeaderDto)
        {
            SalesHeader salesHeader = SalesHeaderMapper.SalesHeaderDtoToSalesHeader(salesHeaderDto);
            await _salesRepository.InsertSalesHeader(salesHeader);

            return CreatedAtAction(nameof(GetSalesHeaderById), new { id = salesHeader.Id }, salesHeader);
        }

        [HttpPut("header/update/{id}")]
        public async Task<ActionResult<SalesHeader>> UpdateSalesHeader(Guid id, [FromBody] SalesHeaderUpdateDto salesHeaderUpdateDto)
        {
            if (id != salesHeaderUpdateDto.Id)
                return BadRequest("ID data pembelian tidak cocok!");

            SalesHeader? salesHeader = await _salesRepository.GetSalesHeaderById(id);
            if (salesHeader is null)
                return BadRequest($"Data pembelian dengan id: {id} tidak ditemukan");

            salesHeaderUpdateDto.PassData(ref salesHeader);
            await _salesRepository.UpdateSalesHeader(salesHeader);

            // Re-query to include all data.
            salesHeader = await _salesRepository.GetSalesHeaderById(id);

            return CreatedAtAction(nameof(GetSalesHeaderById), new { id = salesHeader!.Id }, salesHeader);
        }

        [HttpDelete("header/delete/{id}")]
        public async Task<ActionResult> DeleteSalesHeader(Guid id)
        {
            SalesHeader? salesHeader = await _salesRepository.GetSalesHeaderById(id);
            if (salesHeader is null)
                return BadRequest($"Data penjualan dengan id: {id} tidak ditemukan!");

            if (salesHeader.Status == EntryStatus.Posted.ToString())
            {
                throw new DatabaseDeleteException("Tidak dapat menghapus penjualan yang sudah di posting!");
            }

            await _salesRepository.DeleteSalesHeader(salesHeader);

            return Ok();
        }

        [HttpGet("payment/all")]
        public async Task<ActionResult<SalesPayment>> GetAllSalesPayments()
        {
            IEnumerable<SalesPayment> salesPayments = await _salesRepository.GetAllSalesPayments();
            return Ok(salesPayments);
        }


        //[HttpGet("sales_report")]
        //[AllowAnonymous]
        //public async Task<ActionResult<SalesReportContainer>> GetSalesReport()
        //{
        //    IEnumerable<SalesReportContainer> salesReport = await _salesRepository.GetSalesData();
        //    return Ok(salesReport);
        //}

        //[HttpGet("get_total_item_sales")]
        //[AllowAnonymous]
        //public async Task<ActionResult<ItemSalesContainer>> GetTotalItemSales()
        //{
        //    IEnumerable<ItemSalesContainer> totalItemSales = await _salesRepository.GetTotalItemSalesData();
        //    return Ok(totalItemSales);
        //}

        //[HttpGet("get_total_sales_per_invoice")]
        //[AllowAnonymous]
        //public async Task<ActionResult<SalesPerInvoiceContainer>> GetTotalSalesPerInvoice()
        //{
        //    IEnumerable<SalesPerInvoiceContainer> salesPerInvoices = await _salesRepository.GetTotalSalesPerInvoiceData();
        //    return Ok(salesPerInvoices);
        //}

        //[HttpGet("get_total_sales_payment")]
        //[AllowAnonymous]
        //public async Task<ActionResult<SalesPaymentContainer>> GetTotalSalesPayment()
        //{
        //    IEnumerable<SalesPaymentContainer> totalSalesPayment = await _salesRepository.GetTotalSalesPaymentData();
        //    return Ok(totalSalesPayment);
        //}

        //[HttpGet("get_ar_balance_report")]
        //[AllowAnonymous]
        //public async Task<ActionResult<AccountReceivableBalanceContainer>> GetAccountReceivableBalanceReport()
        //{
        //    IEnumerable<AccountReceivableBalanceContainer> accountReceivableBalanceReport = await _salesRepository.GetAccountReceivableBalanceData();
        //    return Ok(accountReceivableBalanceReport);
        //}
    }
}