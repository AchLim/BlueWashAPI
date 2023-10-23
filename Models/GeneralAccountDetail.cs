using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("general_account_detail")]
    [PrimaryKey(nameof(GeneralAccountHeaderId), nameof(GeneralAccountDetailId))]
    public class GeneralAccountDetail : IAuditable
    {
        [Column(Order = 0)]
        public Guid GeneralAccountHeaderId { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GeneralAccountDetailId { get; set; }

        [Required]
        public virtual GeneralAccountHeader GeneralAccountHeader { get; set; } = default!;

        [DisplayName("Nomor Transaksi")]
        public string TransactionNo => GeneralAccountHeader.TransactionNo;


        [Required]
        public virtual ChartOfAccount ChartOfAccount { get; set; } = default!;
        public Guid ChartOfAccountId { get; set; }

        [DisplayName("Nomor Akun")]
        public int AccountNo => ChartOfAccount.AccountNo;


        [Precision(19, 4)]
        [DisplayName("Debit")]
        public decimal Debit { get; set; }

        [Precision(19, 4)]
        [DisplayName("Credit")]
        public decimal Credit { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
