using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IJournalEntryRepository
    {
        Task<IEnumerable<JournalEntry>> GetAllJournalEntries();
        Task<JournalEntry?> GetJournalEntryById(Guid id);
        Task InsertJournalEntry(JournalEntry journalEntry);
        Task UpdateJournalEntry(JournalEntry journalEntry);
        Task DeleteJournalEntry(JournalEntry journalEntry);


        Task<IEnumerable<JournalEntryContainer>> GetJournalEntryData();
    }
}