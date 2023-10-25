using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface ISalesRepository
    {
        Task<IEnumerable<SalesReportContainer>> GetSalesData();
        Task<IEnumerable<ItemSalesContainer>> GetTotalItemSalesData();
        Task<IEnumerable<SalesPaymentContainer>> GetTotalSalesPaymentData();
        Task<IEnumerable<SalesPerInvoiceContainer>> GetTotalSalesPerInvoiceData();
        Task<IEnumerable<AccountReceivableBalanceContainer>> GetAccountReceivableBalanceData();
    }
}
