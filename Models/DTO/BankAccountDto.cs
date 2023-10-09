using System.ComponentModel.DataAnnotations;

namespace PurchaseAPI.Models.DTO
{
    public class BankAccountDto
    {
        public int BankId { get; set; }
        public string AccountNumber { get; set; }
    }
}
