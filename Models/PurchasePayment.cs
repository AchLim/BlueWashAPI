using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Data.Enum;

namespace WebAPI.Models
{
    [Table("purchase_payment")]
    [PrimaryKey(nameof(PurchasePaymentId), nameof(PurchaseHeaderId))]
    public class PurchasePayment : IAuditable
    {
        [Key]
        [Column(Order = 0)]
        public Guid PurchaseHeaderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PurchasePaymentId { get; set; }

        [Required]
        public virtual PurchaseHeader PurchaseHeader { get; set; } = default!;

        public DateOnly PaymentDate { get; set; }

        [Required]
        [EnumDataType(typeof(PaymentType))]
        public string Type { get; set; } = PaymentType.Cash.ToString();

        [DisplayName("Nomor Referensi")]
        public string? ReferenceNumber { get; set; }

        [Precision(19, 4)]
        [DisplayName("Jumlah")]
        public decimal Amount { get; set; }


        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
