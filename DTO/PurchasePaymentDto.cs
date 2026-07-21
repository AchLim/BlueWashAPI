using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models.DTO
{
    public class PurchasePaymentDto
    {
        public DateOnly PaymentDate { get; set; }
        public string Type { get; set; } = default!;
        public string? ReferenceNumber { get; set; }

        [Precision(19, 4)]
        public decimal Amount { get; set; }
    }
    public class PurchasePaymentUpdateDto
    {
        public Guid? PurchaseHeaderId { get; set; }

        public Guid PurchasePaymentId { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string Type { get; set; } = default!;
        public string? ReferenceNumber { get; set; }

        [Precision(19, 4)]
        public decimal Amount { get; set; }
    }
}
