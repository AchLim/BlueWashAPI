using System.ComponentModel.DataAnnotations;

namespace PurchaseAPI.Models.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int UnitOfMeasureId { get; set; }
    }
}
