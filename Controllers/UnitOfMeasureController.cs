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
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly ILogger<UnitOfMeasureController> _logger;
        private readonly PurchaseDbContext _context;

        public UnitOfMeasureController(ILogger<UnitOfMeasureController> logger, PurchaseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<UnitOfMeasure>> GetAllUnitOfMeasures()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var unitOfMeasures = await _context.UnitOfMeasures.ToListAsync();
                    await transaction.CommitAsync();
                    return unitOfMeasures == null ? NotFound(unitOfMeasures) : Ok(unitOfMeasures);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitOfMeasure>> GetUnitOfMeasureById(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var uom = await _context.UnitOfMeasures.Where(uom => uom.Id == id).FirstOrDefaultAsync();
                    await transaction.CommitAsync();

                    return uom == null ? NotFound(uom) : Ok(uom);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("insert_unit_of_measure")]
        public async Task<ActionResult<UnitOfMeasure>> PostUnitOfMeasure(UnitOfMeasureDto unitOfMeasureDto)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var unitOfMeasureMapper = new UnitOfMeasureMapper();
                    UnitOfMeasure uom = unitOfMeasureMapper.UnitOfMeasureDtoToUnitOfMeasure(unitOfMeasureDto);

                    _context.UnitOfMeasures.Add(uom);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetUnitOfMeasureById), new { id = uom.Id }, uom);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("update_unit_of_measure/{id}")]
        public async Task<ActionResult<UnitOfMeasure>> UpdateProduct(int id, UnitOfMeasure uom)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (id != uom.Id)
                        return BadRequest("Unit Of Measure ID mismatch!");

                    UnitOfMeasure? uomToUpdate = await _context.UnitOfMeasures.FirstOrDefaultAsync(uom => uom.Id == id);
                    if (uomToUpdate == null)
                        return NotFound("Unit Of Measure is not found!");

                    uom.PassValues(ref uomToUpdate);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetUnitOfMeasureById), new { id = uomToUpdate.Id }, uomToUpdate);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("delete_unit_of_measure/{id}")]
        public async Task<ActionResult> DeleteUnitOfMeasure(int id)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    UnitOfMeasure? uomToDelete = await _context.UnitOfMeasures.FirstOrDefaultAsync(uom => uom.Id == id);
                    if (uomToDelete == null)
                        return NotFound("Unit Of Measure is not found!");

                    _context.UnitOfMeasures.Remove(uomToDelete);

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