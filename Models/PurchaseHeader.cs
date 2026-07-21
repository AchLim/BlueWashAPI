using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Data.Enum;

namespace WebAPI.Models
{
    [Table("purchase_header")]
    public class PurchaseHeader : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nomor Pembelian tidak boleh kosong.")]
        [DisplayName("Nomor Pembelian")]
        public string PurchaseNo { get; set; } = default!;

        [DisplayName("Tanggal Pembelian")]
        public DateOnly PurchaseDate { get; set; }

        [Required]
        [EnumDataType(typeof(EntryStatus))]
        public string Status { get; set; } = EntryStatus.Draft.ToString();

        [Required]
        [EnumDataType(typeof(StatusPayment))]
        public string PaymentStatus { get; set; } = StatusPayment.Unpaid.ToString();

        [Required]
        public virtual Supplier Supplier { get; set; } = default!;
        public Guid SupplierId { get; set; }

        [DisplayName("Deskripsi")]
        public string? Description { get; set; }

        [Required]
        [EnumDataType(typeof(PaymentTerm))]
        public string PaymentTerm { get; set; } = default!;

        public ICollection<PurchaseDetail>? PurchaseDetails { get; set; }
        public ICollection<PurchasePayment>? PurchasePayments { get; set; }

        [DisplayName("Mata Uang")]
        public virtual Currency? Currency { get; set; }
        public Guid? CurrencyId { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
