using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models.DTO
{
    public class PurchaseDetailDto
    {
        public Guid InventoryId { get; set; }

        [Precision(19, 4)]
        public decimal Quantity { get; set; }

        [Precision(19, 4)]
        public decimal Price { get; set; }

        [Precision(19, 4)]
        public decimal Discount { get; set; }
    }
    public class PurchaseDetailUpdateDto
    {
        public Guid? PurchaseHeaderId { get; set; }

        public Guid PurchaseDetailId { get; set; }

        public Guid InventoryId { get; set; }

        [Precision(19, 4)]
        public decimal Quantity { get; set; }

        [Precision(19, 4)]
        public decimal Price { get; set; }

        [Precision(19, 4)]
        public decimal Discount { get; set; }
    }
}
