using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models.DTO
{
    public class SalesPaymentDto
    {
        public DateOnly PaymentDate { get; set; }
        public string Type { get; set; } = default!;
        public string? ReferenceNumber { get; set; }

        [Precision(19, 4)]
        public decimal Amount { get; set; }
    }
    public class SalesPaymentUpdateDto
    {
        public Guid? SalesHeaderId { get; set; }

        public Guid SalesPaymentId { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string Type { get; set; } = default!;
        public string? ReferenceNumber { get; set; }

        [Precision(19, 4)]
        public decimal Amount { get; set; }
    }
}
