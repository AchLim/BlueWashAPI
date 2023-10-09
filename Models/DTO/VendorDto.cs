using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PurchaseAPI.Models.DTO
{
    public class VendorDto
    {
        public string Name { get; set; }
        public string? Reference { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? VendorEmail { get; set; }
        public string? VendorSalesEmail { get; set; }
        public int? BankAccountId { get; set; }
        public int? CurrencyId { get; set; }
        public decimal AccountPayable { get; set; }
    }
}
