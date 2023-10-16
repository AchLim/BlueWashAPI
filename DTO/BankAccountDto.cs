using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class BankAccountDto
    {
        public int BankId { get; set; }
        public string AccountNumber { get; set; }
    }

    public class BankAccountUpdateDto
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public required string AccountNumber { get; set; }
    }
}
