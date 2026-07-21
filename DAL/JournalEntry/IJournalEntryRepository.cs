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


        //Task<IEnumerable<JournalEntryContainer>> GetJournalEntryData();

        Task<IEnumerable<PurchaseEntryContainer>> GetPurchaseQueryById(Guid purchaseHeaderId);
        Task<IEnumerable<SalesEntryContainer>> GetSalesQueryById(Guid salesHeaderId);
        Task<bool> ClosingEntryExist(DateOnly transactionDate);
    }
}