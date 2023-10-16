using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("supplier")]
    [Index(nameof(SupplierCode), IsUnique = true)]
    public class Supplier : AuditableEntity
    {
        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Kode Pemasok tidak boleh kosong.")]
        [DisplayName("Kode Pemasok")]
        public string SupplierCode { get; set; } = default!;

        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nama Pemasok tidak boleh kosong.")]
        [DisplayName("Nama Pemasok")]
        public string SupplierName { get; set; } = default!;

        [DisplayName("Alamat Pemasok")]
        public string? SupplierAddress { get; set; }

        [DisplayName("Mata Uang")]
        public virtual Currency? Currency { get; set; }
        public Guid? CurrencyId { get; set; }

        // FK - Purchase Header
        public ICollection<PurchaseHeader>? PurchaseHeaders { get; set; }
    }
}
