using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class InventoryDto
    {
        public required string ItemNo { get; set; }
        public required string ItemName { get; set; }
    }
    public class InventoryUpdateDto
    {
        public Guid Id { get; set; }
        public required string ItemNo { get; set; }
        public required string ItemName { get; set; }
    }
}
