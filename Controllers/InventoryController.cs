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
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(ILogger<InventoryController> logger, IInventoryRepository inventoryRepository)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Inventory>> GetAllInventories()
        {
            IEnumerable<Inventory> inventories = await _inventoryRepository.GetAllInventories();
            return Ok(inventories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventoryById(Guid id)
        {
            Inventory? inventory = await _inventoryRepository.GetInventoryById(id);
            return Ok(inventory);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Inventory>> PostInventory([FromBody] InventoryDto inventoryDto)
        {
            InventoryMapper inventoryMapper = new();
            Inventory inventory = inventoryMapper.InventoryDtoToInventory(inventoryDto);
            await _inventoryRepository.InsertInventory(inventory);

            return CreatedAtAction(nameof(GetInventoryById), new { id = inventory.Id }, inventory);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Inventory>> UpdateInventory(Guid id, [FromBody] InventoryUpdateDto inventoryDto)
        {
            if (id != inventoryDto.Id)
                return BadRequest("ID persediaan tidak cocok!");

            Inventory? inventory = await _inventoryRepository.GetInventoryById(id);
            if (inventory is null)
                return BadRequest($"persediaan dengan id: {id} tidak ditemukan");

            inventoryDto.PassData(ref inventory);
            await _inventoryRepository.UpdateInventory(inventory);

            return CreatedAtAction(nameof(GetInventoryById), new { id = inventory.Id }, inventory);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteInventory(Guid id)
        {
            Inventory? inventory = await _inventoryRepository.GetInventoryById(id);
            if (inventory is null)
                return BadRequest($"Data Inventory dengan id: {id} tidak ditemukan!");

            await _inventoryRepository.DeleteInventory(inventory);

            return Ok();
        }
    }
}