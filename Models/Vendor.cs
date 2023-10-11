using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PurchaseAPI.Models
{
    public class Vendor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name = "Nama")]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name = "Kode")]
        public string? Reference { get; set; }

        [StringLength(120)]
        [Display(Name = "Alamat")]
        public string? Address { get; set; }

        [StringLength(20)]
        [Display(Name = "Telepon")]
        public string? PhoneNumber { get; set; }

        [StringLength(20)]
        [Display(Name = "Nomor Handphone")]
        public string? MobileNumber { get; set; }

        [StringLength(60)]
        [EmailAddress]
        [Display(Name = "Email Pemasok")]
        public string? VendorEmail { get; set; }
        
        [StringLength(60)]
        [EmailAddress]
        [Display(Name = "Email Sales Pemasok")]
        public string? VendorSalesEmail { get; set; }

        [Display(Name = "Akun Bank")]
        public BankAccount? BankAccount { get; set; }
        public int? BankAccountId { get; set; }
        // Add Vendor Bill?

        [Display(Name = "Mata Uang")]
        [JsonIgnore]
        public Currency? Currency { get; set; }
        public int? CurrencyId { get; set; }
    }
}
