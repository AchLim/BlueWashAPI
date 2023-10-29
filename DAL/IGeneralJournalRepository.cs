using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IGeneralJournalRepository
    {
        Task<IEnumerable<GeneralJournalContainer>> GetGeneralJournalData();
    }
}
