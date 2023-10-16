using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int UnitOfMeasureId { get; set; }
    }

    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int UnitOfMeasureId { get; set; }
    }
}
