using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class JournalEntryDto
    {
        public string TransactionNo { get; set; } = default!;
        public DateOnly TransactionDate { get; set; }
        public string? Description { get; set; }
        public ICollection<JournalItemDto>? JournalItems { get; set; }
    }
    public class JournalEntryUpdateDto
    {
        public Guid Id { get; set; }
        public string TransactionNo { get; set; } = default!;
        public DateOnly TransactionDate { get; set; }
        public string? Description { get; set; }
        public ICollection<JournalItemUpdateDto>? JournalItems { get; set; }
    }
}
