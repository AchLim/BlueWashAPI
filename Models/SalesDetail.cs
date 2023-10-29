using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("sales_detail")]
    [PrimaryKey(nameof(SalesHeaderId), nameof(SalesDetailId))]
    public class SalesDetail : IAuditable
    {
        [Column(Order = 0)]
        public Guid SalesHeaderId { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SalesDetailId { get; set; }

        [Required]
        public virtual SalesHeader SalesHeader { get; set; } = default!;

        [Required]
        public virtual LaundryService LaundryService { get; set; } = default!;

        public Guid LaundryServiceId { get; set; }

        [Required]
        public virtual PriceMenu PriceMenu { get; set; } = default!;

        public Guid PriceMenuId { get; set; }


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
