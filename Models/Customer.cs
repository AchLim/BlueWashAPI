using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("customer")]
    [Index(nameof(CustomerCode), IsUnique = true)]
    public class Customer : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Kode Pelanggan tidak boleh kosong.")]
        [DisplayName("Kode Pelanggan")]
        public string CustomerCode { get; set; } = default!;

        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nama Pelanggan tidak boleh kosong.")]
        [DisplayName("Nama Pelanggan")]
        public string CustomerName { get; set; } = default!;

        [StringLength(150)]
        [DisplayName("Alamat Pelanggan")]
        public string? CustomerAddress { get; set; }

        [DisplayName("Mata Uang")]
        public virtual Currency? Currency { get; set; }
        public Guid? CurrencyId { get; set; }

        // FK - Sales Detail
        public ICollection<SalesHeader>? SalesHeaders { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
