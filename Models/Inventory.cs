using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("inventory")]
    [Index(nameof(ItemNo), IsUnique = true)]
    public class Inventory : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nomor Barang tidak boleh kosong.")]
        [DisplayName("Nomor Barang")]
        public string ItemNo { get; set; } = default!;

        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nama Barang tidak boleh kosong.")]
        [DisplayName("Name Barang")]
        public string ItemName { get; set; } = default!;

        // FK - Purchase Detail
        public ICollection<PurchaseDetail>? PurchaseDetails { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
