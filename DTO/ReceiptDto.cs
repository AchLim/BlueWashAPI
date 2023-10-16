using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.Models.DTO
{
    public class ReceiptDto
    {
        public required string Name { get; set; }
        public int VendorId { get; set; }
        public DateTime Date { get; set; }
        public string? VendorReference { get; set; }
        public virtual ICollection<ReceiptLineDto>? ReceiptLines { get; set; }
        public int CurrencyId { get; set; }
        public bool TaxInclusive { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PurposeOfPurchase { get; set; }
    }

    public class ReceiptLineDto
    {
        public virtual int ReceiptId { get; set; }
        public DateTime Date { get; set; }
        public virtual int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public virtual int UnitOfMeasureId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountRate { get; set; }
        public string? Description { get; set; }
    }


    public class ReceiptUpdateDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int VendorId { get; set; }
        public DateTime Date { get; set; }
        public string? VendorReference { get; set; }
        public virtual ICollection<ReceiptLineDto>? ReceiptLines { get; set; }
        public int CurrencyId { get; set; }
        public bool TaxInclusive { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PurposeOfPurchase { get; set; }
    }
}
