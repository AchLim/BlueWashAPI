using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseAPI.Data;
using PurchaseAPI.Models;
using PurchaseAPI.Models.DTO;
using PurchaseAPI.Models.Mapper;
using PurchaseAPI.Utility;
using System.Numerics;

namespace PurchaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly ILogger<VendorController> _logger;
        private readonly PurchaseDbContext _context;

        public VendorController(ILogger<VendorController> logger, PurchaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Vendor>> GetAllVendors()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var vendors = await _context.Vendors
                                                .Include(v => v.BankAccount)
                                                .Include(v => v.Currency)
                                                .ToListAsync();
                    await transaction.CommitAsync();
                    return vendors == null ? NotFound(vendors) : Ok(vendors);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendorById(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var vendor = await _context.Vendors
                                                .Include(v => v.BankAccount)
                                                .Include(v => v.Currency).Where(v => v.Id == id).FirstOrDefaultAsync();
                    await transaction.CommitAsync();

                    return vendor == null ? NotFound(vendor) : Ok(vendor);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("insert_vendor")]
        public async Task<ActionResult<Vendor>> PostVendor(VendorDto vendorDto)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var vendorMapper = new VendorMapper();
                    Vendor vendor = vendorMapper.VendorDtoToVendor(vendorDto);

                    _context.Vendors.Add(vendor);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetVendorById), new { id = vendor.Id }, vendor);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("update_vendor/{id}")]
        public async Task<ActionResult<Vendor>> UpdateVendor(int id, Vendor vendor)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (id != vendor.Id)
                        return BadRequest("Vendor ID mismatch!");

                    Vendor? vendorToUpdate = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
                    if (vendorToUpdate == null)
                        return NotFound("Vendor is not found!");

                    vendor.PassValues(ref vendorToUpdate);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetVendorById), new { id = vendorToUpdate.Id }, vendorToUpdate);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("delete_vendor/{id}")]
        public async Task<ActionResult> DeleteVendor(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Vendor? vendorToDelete = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
                    if (vendorToDelete == null)
                        return NotFound("Vendor is not found!");

                    _context.Vendors.Remove(vendorToDelete);

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