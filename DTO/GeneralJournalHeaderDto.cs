using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class GeneralJournalHeaderDto
    {
        public string TransactionNo { get; set; } = default!;
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public ICollection<GeneralJournalDetailDto>? GeneralJournalDetails { get; set; }
    }
    public class GeneralJournalHeaderUpdateDto
    {
        public Guid Id { get; set; }
        public string TransactionNo { get; set; } = default!;
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public ICollection<GeneralJournalDetailDto>? GeneralJournalDetails { get; set; }
    }
}
