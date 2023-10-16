//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebAPI.Data;
//using WebAPI.Models;
//using WebAPI.Models.DTO;
//using WebAPI.Models.Mapper;
//using WebAPI.Utility;
//using System.Numerics;

//namespace WebAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class VendorController : ControllerBase
//    {
//        private readonly ILogger<VendorController> _logger;
//        private readonly ApplicationContext _context;

//        public VendorController(ILogger<VendorController> logger, ApplicationContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        [HttpGet("all")]
//        public async Task<ActionResult<Vendor>> GetAllVendors()
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    var vendors = await _context.Vendors
//                                                .Include(v => v.BankAccount)
//                                                .ThenInclude(ba => ba != null ? ba.Bank : null)
//                                                .Include(v => v.Currency)
//                                                .ToListAsync();
//                    await transaction.CommitAsync();
//                    return vendors == null ? NotFound(vendors) : Ok(vendors);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Vendor>> GetVendorById(int id)
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    var vendor = await _context.Vendors
//                                                .Include(v => v.BankAccount)
//                                                .ThenInclude(ba => ba != null ? ba.Bank : null)
//                                                .Include(v => v.Currency).Where(v => v.Id == id).FirstOrDefaultAsync();
//                    await transaction.CommitAsync();

//                    return vendor == null ? NotFound(vendor) : Ok(vendor);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpPost("insert")]
//        public async Task<ActionResult<Vendor>> PostVendor(VendorDto vendorDto)
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    var vendorMapper = new VendorMapper();
//                    Vendor vendor = vendorMapper.VendorDtoToVendor(vendorDto);

//                    _context.Vendors.Add(vendor);
//                    await _context.SaveChangesAsync();

//                    await transaction.CommitAsync();

//                    return CreatedAtAction(nameof(GetVendorById), new { id = vendor.Id }, vendor);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpPut("update/{id}")]
//        public async Task<ActionResult<Vendor>> UpdateVendor(int id, VendorUpdateDto vendorUpdateDto)
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    if (id != vendorUpdateDto.Id)
//                        return BadRequest("Vendor ID mismatch!");

//                    Vendor? vendorToUpdate = await _context.Vendors.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
//                    if (vendorToUpdate == null)
//                        return NotFound("Vendor is not found!");

//                    var vendorMapper = new VendorMapper();
//                    Vendor vendor = vendorMapper.VendorUpdateDtoToVendor(vendorUpdateDto);

//                    _context.Vendors.Update(vendor);

//                    await _context.SaveChangesAsync();
//                    await transaction.CommitAsync();

//                    return CreatedAtAction(nameof(GetVendorById), new { id = vendor.Id }, vendor);
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpDelete("delete/{id}")]
//        public async Task<ActionResult> DeleteVendor(int id)
//        {
//            await using (var transaction = await _context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    Vendor? vendorToDelete = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
//                    if (vendorToDelete == null)
//                        return NotFound("Vendor is not found!");

//                    _context.Vendors.Remove(vendorToDelete);

//                    await _context.SaveChangesAsync();
//                    await transaction.CommitAsync();

//                    return NoContent();
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }
//    }
//}