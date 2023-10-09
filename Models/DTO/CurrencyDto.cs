using System.ComponentModel.DataAnnotations;

namespace PurchaseAPI.Models.DTO
{
    public class CurrencyDto
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
