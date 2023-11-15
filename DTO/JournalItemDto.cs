using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.DTO
{
    public class JournalItemDto
    {
        public Guid ChartOfAccountId { get; set; }

        [Precision(19, 4)]
        public decimal Debit { get; set; }

        [Precision(19, 4)]
        public decimal Credit { get; set; }
    }
    public class JournalItemUpdateDto
    {
        public Guid? JournalEntryId { get; set; }

        public Guid JournalItemId { get; set; }

        public Guid ChartOfAccountId { get; set; }

        [Precision(19, 4)]
        public decimal Debit { get; set; }

        [Precision(19, 4)]
        public decimal Credit { get; set; }
    }
}
