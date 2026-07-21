using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Data.Enum;

namespace WebAPI.Models
{
    [Table("journal_entry")]
    public class JournalEntry : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 1, ErrorMessage = "Nomor Transaksi tidak boleh kosong.")]
        public string TransactionNo { get; set; } = default!;

        [Required]
        public DateOnly TransactionDate { get; set; }

        [Required]
        [EnumDataType(typeof(EntryStatus))]
        public string Status { get; set; } = EntryStatus.Draft.ToString();

        [DisplayName("Deskripsi")]
        public string? Description { get; set; }

        // FK - Closing Entry
        public virtual ClosingEntry? ClosingEntry { get; set; }
        public Guid? ClosingEntryId { get; set; }

        // FK - Journal Item
        public ICollection<JournalItem>? JournalItems { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
