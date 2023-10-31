using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class CustomerDto
    {
        public required string CustomerCode { get; set; }
        public required string CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public Guid? CurrencyId { get; set; }
    }
    public class CustomerUpdateDto
    {
        public Guid Id { get; set; }
        public required string CustomerCode { get; set; }
        public required string CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public Guid? CurrencyId { get; set; }
    }
}
