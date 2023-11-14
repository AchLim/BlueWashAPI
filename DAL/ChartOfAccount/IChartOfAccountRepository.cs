using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IChartOfAccountRepository
    {
        Task<IEnumerable<ChartOfAccount>> GetAllChartOfAccounts();
        Task<ChartOfAccount?> GetChartOfAccountById(Guid id);
        Task InsertChartOfAccount(ChartOfAccount chartOfAccount);
        Task UpdateChartOfAccount(ChartOfAccount chartOfAccount);
        Task DeleteChartOfAccount(ChartOfAccount chartOfAccount);
    }
}
