using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("general_account_header")]
    [Index(nameof(TransactionNo), IsUnique = true)]
    public class GeneralAccountHeader : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nomor Transaksi tidak boleh kosong.")]
        [DisplayName("Nomor Transaksi")]
        public string TransactionNo { get; set; } = default!;

        [Required]
        [DisplayName("Tanggal Transaksi")]
        public DateOnly TransactionDate { get; set; }

        [DisplayName("Deskripsi")]
        public string? Description { get; set; }

        // FK - General Account Detail
        public ICollection<GeneralAccountDetail>? GeneralAccountDetails { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
