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