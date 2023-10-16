using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("sales_header")]
    [Index(nameof(SalesNo), IsUnique = true)]
    public class SalesHeader : AuditableEntity
    {
        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nomor Penjualan tidak boleh kosong.")]
        [DisplayName("Nomor Penjualan")]
        public string SalesNo { get; set; } = default!;

        [DisplayName("Tanggal Penjualan")]
        public DateOnly SalesDate { get; set; }

        [Required]
        public virtual Customer Customer { get; set; } = default!;
        public Guid CustomerId { get; set; }

        [DisplayName("Kode Pelanggan")]
        public string CustomerCode => Customer.CustomerCode;

        [DisplayName("Deskripsi")]
        public string? Description { get; set; }

        // FK - Sales Detail
        public ICollection<SalesDetail>? SalesDetails { get; set; }

        // FK - Sales Payment
        public ICollection<SalesPayment>? SalesPayments { get; set; }

        [DisplayName("Mata Uang")]
        public virtual Currency? Currency { get; set; }
        public Guid? CurrencyId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Customer.Currency is not null && Currency is null)
            {
                yield return new ValidationResult("Mata Uang tidak boleh kosong apabila mata uang pelanggan telah ditentukan.", new string[] { nameof(Customer.Currency), nameof(Currency) });
            }
        }
    }
}
