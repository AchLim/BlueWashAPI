using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("inventory")]
    [Index(nameof(ItemNo), IsUnique = true)]
    public class Inventory : AuditableEntity
    {
        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nomor Barang tidak boleh kosong.")]
        [DisplayName("Nomor Barang")]
        public string ItemNo { get; set; } = default!;

        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nama Barang tidak boleh kosong.")]
        [DisplayName("Name Barang")]
        public string ItemName { get; set; } = default!;

        [Precision(19, 4)]
        [DisplayName("Harga Barang")]
        public decimal ItemPrice { get; set; }

        // FK - Purchase Detail
        public ICollection<PurchaseDetail>? PurchaseDetails { get; set; }

        // FK - Sales Detail
        public ICollection<SalesDetail>? SalesDetails { get; set; }
    }
}
