using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models.DTO
{
    public class SalesDetailDto
    {
        public Guid LaundryServiceId { get; set; }
        public Guid PriceMenuId { get; set; }

        [Precision(19, 4)]
        public decimal Quantity { get; set; }

        [Precision(19, 4)]
        public decimal Price { get; set; }

        [Precision(19, 4)]
        public decimal Discount { get; set; }
    }
    public class SalesDetailUpdateDto
    {
        public Guid? SalesHeaderId { get; set; }
        public Guid SalesDetailId { get; set; }

        public Guid LaundryServiceId { get; set; }
        public Guid PriceMenuId { get; set; }

        [Precision(19, 4)]
        public decimal Quantity { get; set; }

        [Precision(19, 4)]
        public decimal Price { get; set; }

        [Precision(19, 4)]
        public decimal Discount { get; set; }
    }
}
