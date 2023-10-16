using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebAPI.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("purchase_header")]
    [Index(nameof(PurchaseNo), IsUnique = true)]
    public class PurchaseHeader : AuditableEntity, IValidatableObject
    {
        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nomor Pembelian tidak boleh kosong.")]
        [DisplayName("Nomor Pembelian")]
        public string PurchaseNo { get; set; } = default!;

        [DisplayName("Tanggal Pembelian")]
        public DateOnly PurchaseDate { get; set; }

        [Required]
        public virtual Supplier Supplier { get; set; } = default!;
        public Guid SupplierId { get; set; }

        [DisplayName("Kode Supplier")]
        public string SupplierCode => Supplier.SupplierCode;

        [DisplayName("Deskripsi")]
        public string? Description { get; set; }

        // FK - Purchase Detail
        public ICollection<PurchaseDetail>? PurchaseDetails { get; set; }

        [DisplayName("Mata Uang")]
        public virtual Currency? Currency { get; set; }
        public Guid? CurrencyId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Supplier.Currency is not null && Currency is null)
            {
                yield return new ValidationResult("Mata Uang tidak boleh kosong apabila mata uang pemasok telah ditentukan.", new string[] { nameof(Supplier.Currency), nameof(Currency) });
            }
        }
    }
}
