using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
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
        public PricingOption PricingOption { get; set; }


        public int ProcessingTime { get; set; }

        [Required]
        [EnumDataType(typeof(TimeUnit))]
        public TimeUnit TimeUnit { get; set; }

        [Required]
        [EnumDataType(typeof(DeliveryOption))]
        public DeliveryOption DeliveryOption { get; set; }

        public ICollection<SalesDetail>? SalesDetails { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }

        // Helper

        [NotMapped]
        public string PricingOptionDisplay => PricingOption switch
        {
            PricingOption.Package => "Paket",
            _ => PricingOption.ToString(),
        };

        [NotMapped]
        public string TimeUnitDisplay => TimeUnit switch
        {
            TimeUnit.Hour => "Jam",
            TimeUnit.Day => "Hari",
            _ => "-"
        };

        [NotMapped]
        public string DeliveryOptionDisplay => DeliveryOption switch
        {
            DeliveryOption.None => "-",
            DeliveryOption.OneDay => "One Day",
            _ => DeliveryOption.ToString(),
        };

        [NotMapped]
        public string ProcessingTimeDisplay => (ProcessingTime, TimeUnit) switch
        {
            (_, TimeUnit.None) => "-",
            (_, _) => $"{ProcessingTime} {TimeUnitDisplay}",
        };

        [NotMapped]
        public string PriceDisplay => string.Format(new CultureInfo("id-ID"), "{0:C}", Price);

    }

    public enum PricingOption : ushort
    {
        Unit = 1,
        Kilogram = 2,
        Set = 4,
        Package = 8,
    }

    public enum TimeUnit : ushort
    {
        None = 0,
        Hour = 1,
        Day = 2,
    }

    public enum DeliveryOption : ushort
    {
        None = 0,
        Reguler = 1,
        OneDay = 2,
        Express = 4,
    }
}
