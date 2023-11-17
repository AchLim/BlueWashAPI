using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.DTO
{
    public class PurchaseDetailDto
    {
        public Guid InventoryId { get; set; }

        [Precision(19, 4)]
        public decimal Quantity { get; set; }

        [Precision(19, 4)]
        public decimal Price { get; set; }
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
    }
}
