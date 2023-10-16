using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("sales_payment")]
    public class SalesPayment : AuditableEntity
    {
        [Required]
        public virtual SalesHeader SalesHeader { get; set; } = default!;
        public Guid SalesHeaderId { get; set; }

        [DisplayName("Nomor Penjualan")]
        public string SalesNo => SalesHeader.SalesNo;

        [DisplayName("Nomor Referensi")]
        public string? ReferenceNumber { get; set; }

        [Precision(19, 4)]
        [DisplayName("Jumlah")]
        public decimal Amount { get; set; }

    }
}
