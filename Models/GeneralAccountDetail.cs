using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("general_account_detail")]
    public class GeneralAccountDetail : AuditableEntity
    {
        [Required]
        public virtual GeneralAccountHeader GeneralAccountHeader { get; set; } = default!;
        public Guid GeneralAccountHeaderId { get; set; }

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
    }
}
