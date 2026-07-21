using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.Common;

namespace WebAPI.Models
{
    [Table("price_menu")]
    [Index(nameof(LaundryServiceId), nameof(Name), nameof(DeliveryOption), IsUnique = true)]
    [PrimaryKey(nameof(LaundryServiceId), nameof(PriceMenuId))]
    public class PriceMenu : IAuditable
    {
        [Column(Order = 0)]
        public Guid LaundryServiceId { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PriceMenuId { get; set; }

        public virtual LaundryService LaundryService { get; set; } = default!;

        [Required]
        public string Name { get; set; } = default!;

        [Precision(19, 3)]
        public decimal Price { get; set; }

        [Required]
        [EnumDataType(typeof(PricingOption))]
        public string PricingOption { get; set; } = Models.PricingOption.Unit.ToString();

        public int ProcessingTime { get; set; }
         
        [Required]
        [EnumDataType(typeof(TimeUnit))]
        public string TimeUnit { get; set; } = Models.TimeUnit.None.ToString();

        [Required]
        [EnumDataType(typeof(DeliveryOption))]
        public string DeliveryOption { get; set; } = Models.DeliveryOption.None.ToString();

        public ICollection<SalesDetail>? SalesDetails { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    public enum PricingOption
    {
        Unit,
        Kilogram,
        Set,
        Package,
    }

    public enum TimeUnit
    {
        None,
        Hour,
        Day,
    }

    public enum DeliveryOption
    {
        None,
        Reguler,
        OneDay,
        Express,
    }
}
