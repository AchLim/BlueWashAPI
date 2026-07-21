namespace WebAPI.Models.DTO
{
    public class SalesHeaderDto
    {
        public string SalesNo { get; set; } = default!;
        public DateOnly SalesDate { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; } = default!;
        public string PaymentTerm { get; set; } = default!;
        public string PaymentStatus { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<SalesDetailDto>? SalesDetails { get; set; }
        public ICollection<SalesPaymentDto>? SalesPayments { get; set; }
    }
    public class SalesHeaderUpdateDto
    {
        public Guid Id { get; set; }
        public string SalesNo { get; set; } = default!;
        public DateOnly SalesDate { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; } = default!;
        public string PaymentTerm { get; set; } = default!;
        public string PaymentStatus { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<SalesDetailUpdateDto>? SalesDetails { get; set; }
        public ICollection<SalesPaymentUpdateDto>? SalesPayments { get; set; }
    }
}
