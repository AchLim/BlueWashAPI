using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class PurchaseHeaderDto
    {
        public string PurchaseNo { get; set; } = default!;
        public DateOnly PurchaseDate { get; set; }
        public Guid SupplierId { get; set; }
        public string? Description { get; set; }
        public ICollection<PurchaseDetail>? PurchaseDetails { get; set; }
    }
    public class PurchaseHeaderUpdateDto
    {
        public Guid Id { get; set; }
        public string PurchaseNo { get; set; } = default!;
        public DateOnly PurchaseDate { get; set; }
        public Guid SupplierId { get; set; }
        public string? Description { get; set; }
        public ICollection<PurchaseDetailUpdateDto>? PurchaseDetails { get; set; }
    }
}
