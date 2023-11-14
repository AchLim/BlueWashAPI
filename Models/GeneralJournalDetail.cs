using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("general_journal_detail")]
    [PrimaryKey(nameof(GeneralJournalHeaderId), nameof(GeneralJournalDetailId))]
    public class GeneralJournalDetail : IAuditable
    {
        [Column(Order = 0)]
        public Guid GeneralJournalHeaderId { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GeneralJournalDetailId { get; set; }

        [Required]
        public virtual GeneralJournalHeader GeneralJournalHeader { get; set; } = default!;

        [Required]
        public virtual ChartOfAccount ChartOfAccount { get; set; } = default!;
        public Guid ChartOfAccountId { get; set; }


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
