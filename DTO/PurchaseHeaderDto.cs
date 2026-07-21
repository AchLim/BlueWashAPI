namespace WebAPI.Models.DTO
{
    public class PurchaseHeaderDto
    {
        public string PurchaseNo { get; set; } = default!;
        public DateOnly PurchaseDate { get; set; }
        public Guid SupplierId { get; set; }
        public string Status { get; set; } = default!;
        public string PaymentTerm { get; set; } = default!;
        public string PaymentStatus { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<PurchaseDetailDto>? PurchaseDetails { get; set; }
        public ICollection<PurchasePaymentDto>? PurchasePayments { get; set; }
    }
    public class PurchaseHeaderUpdateDto
    {
        public Guid Id { get; set; }
        public string PurchaseNo { get; set; } = default!;
        public DateOnly PurchaseDate { get; set; }
        public Guid SupplierId { get; set; }
        public string Status { get; set; } = default!;
        public string PaymentTerm { get; set; } = default!;
        public string PaymentStatus { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<PurchaseDetailUpdateDto>? PurchaseDetails { get; set; }
        public ICollection<PurchasePaymentUpdateDto>? PurchasePayments { get; set; }
    }
}
