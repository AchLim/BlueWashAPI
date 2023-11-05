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
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController(ILogger<SupplierController> logger, ISupplierRepository supplierRepository)
        {
            _logger = logger;
            _supplierRepository = supplierRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Supplier>> GetAllSuppliers()
        {
            IEnumerable<Supplier> suppliers = await _supplierRepository.GetAllSuppliers();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplierById(Guid id)
        {
            Supplier? supplier = await _supplierRepository.GetSupplierById(id);
            return Ok(supplier);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Supplier>> PostSupplier([FromBody] SupplierDto supplierDto)
        {
            SupplierMapper supplierMapper = new();
            Supplier supplier = supplierMapper.SupplierDtoToSupplier(supplierDto);
            await _supplierRepository.InsertSupplier(supplier);

            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.Id }, supplier);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Supplier>> UpdateSupplier(Guid id, [FromBody] SupplierUpdateDto supplierDto)
        {
            if (id != supplierDto.Id)
                return BadRequest("ID Pemasok tidak cocok!");

            Supplier? supplier = await _supplierRepository.GetSupplierById(id);
            if (supplier is null)
                return BadRequest($"Pemasok dengan id: {id} tidak ditemukan");

            supplierDto.PassData(ref supplier);
            await _supplierRepository.UpdateSupplier(supplier);

            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.Id }, supplier);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteSupplier (Guid id)
        {
            Supplier? supplier = await _supplierRepository.GetSupplierById(id);
            if (supplier is null)
                return BadRequest($"Data Pemasok dengan id: {id} tidak ditemukan!");

            await _supplierRepository.DeleteSupplier(supplier);

            return Ok();
        }
    }
}