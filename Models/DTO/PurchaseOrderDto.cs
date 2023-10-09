using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PurchaseAPI.Models.DTO
{
    public class PurchaseOrderDto
    {
        public string Name { get; set; }
        public int VendorId { get; set; }
        public DateTime Date { get; set; }
        public string? VendorReference { get; set; }
        public virtual ICollection<PurchaseOrderLineDto>? PurchaseOrderLines { get; set; }
        public int CurrencyId { get; set; }
        public bool TaxInclusive { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class PurchaseOrderLineDto
    {
        public virtual int PurchaseOrderId { get; set; }
        public DateTime Date { get; set; }
        public virtual int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public virtual int UnitOfMeasureId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
