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
    public sealed class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseController(ILogger<PurchaseController> logger, IPurchaseRepository purchaseRepository)
        {
            _logger = logger;
            _purchaseRepository = purchaseRepository;
        }

        [HttpGet("header/all")]
        public async Task<ActionResult<PurchaseHeader>> GetAllJournalEntriesAsync()
        {
            IEnumerable<PurchaseHeader> journalEntries = await _purchaseRepository.GetAllPurchaseHeaders();
            return Ok(journalEntries);
        }

        [HttpGet("header/{id}")]
        public async Task<ActionResult<PurchaseHeader>> GetPurchaseHeaderById(Guid id)
        {
            PurchaseHeader? purchaseHeader = await _purchaseRepository.GetPurchaseHeaderById(id);
            return Ok(purchaseHeader);
        }

        [HttpPost("header/insert")]
        public async Task<ActionResult<PurchaseHeader>> PostPurchaseHeader([FromBody] PurchaseHeaderDto purchaseHeaderDto)
        {
            PurchaseHeader purchaseHeader = PurchaseHeaderMapper.PurchaseHeaderDtoToPurchaseHeader(purchaseHeaderDto);
            await _purchaseRepository.InsertPurchaseHeader(purchaseHeader);

            return CreatedAtAction(nameof(GetPurchaseHeaderById), new { id = purchaseHeader.Id }, purchaseHeader);
        }

        [HttpPut("header/update/{id}")]
        public async Task<ActionResult<PurchaseHeader>> UpdatePurchaseHeader(Guid id, [FromBody] PurchaseHeaderUpdateDto purchaseHeaderUpdateDto)
        {
            if (id != purchaseHeaderUpdateDto.Id)
                return BadRequest("ID data pembelian tidak cocok!");

            PurchaseHeader? purchaseHeader = await _purchaseRepository.GetPurchaseHeaderById(id);
            if (purchaseHeader is null)
                return BadRequest($"Data pembelian dengan id: {id} tidak ditemukan");

            purchaseHeaderUpdateDto.PassData(ref purchaseHeader);
            await _purchaseRepository.UpdatePurchaseHeader(purchaseHeader);

            // Re-query to include all data.
            purchaseHeader = await _purchaseRepository.GetPurchaseHeaderById(id);

            return CreatedAtAction(nameof(GetPurchaseHeaderById), new { id = purchaseHeader!.Id }, purchaseHeader);
        }

        [HttpDelete("header/delete/{id}")]
        public async Task<ActionResult> DeletePurchaseHeader(Guid id)
        {
            PurchaseHeader? purchaseHeader = await _purchaseRepository.GetPurchaseHeaderById(id);
            if (purchaseHeader is null)
                return BadRequest($"Data pembelian dengan id: {id} tidak ditemukan!");

            await _purchaseRepository.DeletePurchaseHeader(purchaseHeader);

            return Ok();
        }
        
        [HttpGet("get_total_item_purchase")]
        [AllowAnonymous]
        public async Task<ActionResult<ItemPurchaseContainer>> GetTotalItemPurchase()
        {
            IEnumerable<ItemPurchaseContainer> totalItemPurchase = await _purchaseRepository.GetTotalItemPurchaseData();
            return Ok(totalItemPurchase);
        }

        [HttpGet("get_total_purchase_per_invoice")]
        [AllowAnonymous]
        public async Task<ActionResult<SalesPerInvoiceContainer>> GetTotalPurchasePerInvoice()
        {
            IEnumerable<PurchasePerInvoiceContainer> purchasePerInvoices = await _purchaseRepository.GetTotalPurchasePerInvoiceData();
            return Ok(purchasePerInvoices);
        }
    }
}