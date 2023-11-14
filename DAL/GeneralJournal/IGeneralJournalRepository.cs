using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IGeneralJournalRepository
    {
        Task<IEnumerable<GeneralJournalHeader>> GetAllGeneralJournalHeaders();
        Task<GeneralJournalHeader?> GetGeneralJournalHeaderById(Guid id);
        Task InsertGeneralJournalHeader(GeneralJournalHeader generalJournalHeader);
        Task UpdateGeneralJournalHeader(GeneralJournalHeader generalJournalHeader);
        Task DeleteGeneralJournalHeader(GeneralJournalHeader generalJournalHeader);



        Task<IEnumerable<GeneralJournalContainer>> GetGeneralJournalData();
    }
}
