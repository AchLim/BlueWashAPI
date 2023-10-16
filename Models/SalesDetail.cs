using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("sales_detail")]
    public class SalesDetail : AuditableEntity
    {
        [Required]
        public virtual SalesHeader SalesHeader { get; set; } = default!;
        public Guid SalesHeaderId { get; set; }

        [DisplayName("Nomor Sales")]
        public string SalesNo => SalesHeader.SalesNo;

        [Required]
        public virtual Inventory Inventory { get; set; } = default!;
        public Guid InventoryId { get; set; }

        [DisplayName("Nomor Barang")]
        public string ItemNo => Inventory.ItemNo;


        [Precision(19, 4)]
        [DisplayName("Kuantitas")]
        public decimal Quantity { get; set; }

        [Precision(19, 4)]
        [DisplayName("Harga")]
        public decimal Price { get; set; }
    }
}
