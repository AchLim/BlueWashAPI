using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class SupplierDto
    {
        public required string SupplierCode { get; set; }
        public required string SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public Guid? CurrencyId { get; set; }
    }
    public class SupplierUpdateDto
    {
        public Guid Id { get; set; }
        public required string SupplierCode { get; set; }
        public required string SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public Guid? CurrencyId { get; set; }
    }
}
