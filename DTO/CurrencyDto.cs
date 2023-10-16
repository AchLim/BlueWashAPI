using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class CurrencyDto
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string CultureName { get; set; }
    }
    public class CurrencyUpdateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string CultureName { get; set; }
    }
}
