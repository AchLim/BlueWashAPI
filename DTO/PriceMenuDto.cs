using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class PriceMenuDto
    {
        public Guid PriceMenuId { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public string PricingOption { get; set; } = default!;
        public int ProcessingTime { get; set; }
        public string TimeUnit { get; set; } = default!;
        public string DeliveryOption { get; set; } = default!;
    }
    public class PriceMenuUpdateDto
    {
        public Guid? LaundryServiceId { get; set; }
        public Guid PriceMenuId { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public string PricingOption { get; set; } = default!;
        public int ProcessingTime { get; set; }
        public string TimeUnit { get; set; } = default!;
        public string DeliveryOption { get; set; } = default!;
    }
}
