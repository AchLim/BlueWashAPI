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
    public class ReceiptController : ControllerBase
    {
        private readonly ILogger<ReceiptController> _logger;
        private readonly PurchaseDbContext _context;

        public ReceiptController(ILogger<ReceiptController> logger, PurchaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Receipt>> GetAllReceipts()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var receipts = await _context.Receipts
                                                .Include(r => r.Vendor)
                                                .Include(r => r.Currency)
                                                //.Include(po => po.ReceiptLines)
                                                .ToListAsync();
                    await transaction.CommitAsync();
                    return receipts == null ? NotFound(receipts) : Ok(receipts);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceiptById(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var receipt = await _context.Receipts
                                            .Include(receipt => receipt.Vendor)
                                            .Include(receipt => receipt.Currency)
                                            //.Include(receipt => receipt.PurchaseOrderLines)
                                            .Where(receipt => receipt.Id == id).FirstOrDefaultAsync();
                    await transaction.CommitAsync();

                    return receipt == null ? NotFound(receipt) : Ok(receipt);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Receipt>> PostReceipt(ReceiptDto receiptDto)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ReceiptMapper receiptMapper = new();
                    Receipt receipt = receiptMapper.ReceiptDtoToReceipt(receiptDto);

                    _context.Receipts.Add(receipt);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetReceiptById), new { id = receipt.Id }, receipt);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Receipt>> UpdateReceipt(int id, Receipt receipt)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (id != receipt.Id)
                        return BadRequest("Receipt ID mismatch!");

                    Receipt? receiptToUpdate = await _context.Receipts
                                                                //.Include(receipt => receipt.PurchaseOrderLines)
                                                                //.Include(receipt => receipt.VendorId)
                                                                .FirstOrDefaultAsync(receipt => receipt.Id == id);
                    if (receiptToUpdate == null)
                        return NotFound("Receipt is not found!");

                    _context.Receipts.Update(receipt);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetReceiptById), new { id = receiptToUpdate.Id }, receiptToUpdate);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteReceipt(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Receipt? poToDelete = await _context.Receipts.FirstOrDefaultAsync(po => po.Id == id);
                    if (poToDelete == null)
                        return NotFound("Receipt is not found!");

                    _context.Receipts.Remove(poToDelete);

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