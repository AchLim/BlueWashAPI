using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("sales_payment")]
    [PrimaryKey(nameof(SalesPaymentId), nameof(SalesHeaderId))]
    public class SalesPayment : IAuditable
    {
        [Key]
        [Column(Order = 0)]
        public Guid SalesHeaderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SalesPaymentId { get; set; }

        [Required]
        public virtual SalesHeader SalesHeader { get; set; } = default!;

        [DisplayName("Nomor Penjualan")]
        public string SalesNo => SalesHeader.SalesNo;

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
