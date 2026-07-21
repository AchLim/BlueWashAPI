using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IReportRepository
    {
        Task<IEnumerable<GeneralLedgerContainer>> GetGeneralLedgerData(DateOnly dateFrom, DateOnly dateTo);
        Task<IEnumerable<TrialBalanceContainer>> GetTrialBalanceData(DateOnly dateFrom, DateOnly dateTo);
        Task<IEnumerable<IncomeStatementContainer>> GetIncomeStatementData(DateOnly dateFrom, DateOnly dateTo);
        Task<IEnumerable<BalanceSheetContainer>> GetBalanceSheetData(DateOnly dateFrom, DateOnly dateTo);
        Task<IEnumerable<PurchaseReportContainer>> GetPurchaseReportData(DateOnly dateFrom, DateOnly dateTo);
        Task<IEnumerable<SalesReportContainer>> GetSalesReportData(DateOnly dateFrom, DateOnly dateTo);
    }
}