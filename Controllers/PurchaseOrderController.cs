using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseAPI.Data;
using PurchaseAPI.Models;
using PurchaseAPI.Models.DTO;
using PurchaseAPI.Models.Mapper;
using PurchaseAPI.Utility;

namespace PurchaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly ILogger<PurchaseOrderController> _logger;
        private readonly PurchaseDbContext _context;

        public PurchaseOrderController(ILogger<PurchaseOrderController> logger, PurchaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<PurchaseOrder>> GetAllPurchaseOrders()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var purchaseOrders = await _context.PurchaseOrders
                                                .Include(po => po.Vendor)
                                                .Include(po => po.Currency)
                                                //.Include(po => po.PurchaseOrderLines)
                                                .ToListAsync();
                    await transaction.CommitAsync();
                    return purchaseOrders == null ? NotFound(purchaseOrders) : Ok(purchaseOrders);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrderById(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var po = await _context.PurchaseOrders
                                            .Include(po => po.Vendor)
                                            .Include(po => po.Currency)
                                            //.Include(po => po.PurchaseOrderLines)
                                            .Where(po => po.Id == id).FirstOrDefaultAsync();
                    await transaction.CommitAsync();

                    return po == null ? NotFound(po) : Ok(po);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("insert_purchase_order")]
        public async Task<ActionResult<PurchaseOrder>> PostPurchaseOrder(PurchaseOrderDto purchaseOrderDto)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    PurchaseOrder po = purchaseOrderDto.Convert();

                    _context.PurchaseOrders.Add(po);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetPurchaseOrderById), new { id = po.Id }, po);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        [HttpPut("update_purchase_order/{id}")]
        public async Task<ActionResult<PurchaseOrder>> UpdatePurchaseOrder(int id, PurchaseOrder po)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (id != po.Id)
                        return BadRequest("Purchase Order ID mismatch!");

                    PurchaseOrder? poToUpdate = await _context.PurchaseOrders
                                                                //.Include(po => po.PurchaseOrderLines)
                                                                //.Include(po => po.VendorId)
                                                                .FirstOrDefaultAsync(po => po.Id == id);
                    if (poToUpdate == null)
                        return NotFound("Purchase Order is not found!");

                    _context.PurchaseOrders.Update(po);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetPurchaseOrderById), new { id = poToUpdate.Id }, poToUpdate);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("delete_purchase_order/{id}")]
        public async Task<ActionResult> DeletePurchaseOrder(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    PurchaseOrder? poToDelete = await _context.PurchaseOrders.FirstOrDefaultAsync(po => po.Id == id);
                    if (poToDelete == null)
                        return NotFound("Purchase Order is not found!");

                    _context.PurchaseOrders.Remove(poToDelete);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}