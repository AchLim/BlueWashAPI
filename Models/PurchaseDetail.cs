using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("purchase_detail")]
    [PrimaryKey(nameof(PurchaseHeaderId), nameof(PurchaseDetailId))]
    public class PurchaseDetail : IAuditable
    {
        [Column(Order = 0)]
        public Guid PurchaseHeaderId { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PurchaseDetailId { get; set; }

        [Required]
        public virtual PurchaseHeader PurchaseHeader { get; set; } = default!;

        [DisplayName("Nomor Pembelian")]
        public string PurchaseNo => PurchaseHeader.PurchaseNo;

        [Required]
        public virtual Inventory Inventory { get; set; } = default!;
        public Guid InventoryId { get; set; }

        [DisplayName("Nomor Pembelian")]
        public string ItemNo => Inventory.ItemNo;


        [Precision(19, 4)]
        [DisplayName("Kuantitas")]
        public decimal Quantity { get; set; }

        [Precision(19, 4)]
        [DisplayName("Harga")]
        public decimal Price { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
