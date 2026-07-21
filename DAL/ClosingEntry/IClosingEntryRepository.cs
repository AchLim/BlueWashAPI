using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IClosingEntryRepository
    {
        Task<IEnumerable<ClosingEntry>> GetAllClosingEntries();
        Task<ClosingEntry?> GetClosingEntryById(Guid id);
        Task DeleteClosingEntry(ClosingEntry closingEntry);

        Task<bool> ClosingEntryExist(DateOnly dateFrom, DateOnly dateTo);
        Task<bool> UnpostedEntryExist(DateOnly dateFrom, DateOnly dateTo);
        Task<ClosingEntry> CreateClosingEntry(DateOnly dateFrom, DateOnly dateTo);
    }
}