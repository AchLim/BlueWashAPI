using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("chart_of_account")]
    [Index(nameof(AccountNo), IsUnique = true)]
    public class ChartOfAccount : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DisplayName("Nomor Header Akun")]
        public int AccountHeaderNo { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nama Header Akun tidak boleh kosong.")]
        [DisplayName("Nama Header Akun")]
        public string AccountHeaderName { get; set; } = default!;

        [DisplayName("Nomor Akun")]
        public int AccountNo { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nama Akun tidak boleh kosong.")]
        [DisplayName("Nama Akun")]
        public string AccountName { get; set; } = default!;

        [DisplayName("Mata Uang")]
        public virtual Currency? Currency { get; set; }
        public Guid? CurrencyId { get; set; }

        // FK - General Account Detail
        public ICollection<GeneralAccountDetail>? GeneralAccountDetails { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
