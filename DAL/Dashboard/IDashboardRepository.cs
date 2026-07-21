namespace WebAPI.DAL
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<Decimal>> GetCashSalesDashboardData(DateOnly dateFrom, DateOnly dateTo);
        Task<IEnumerable<Decimal>> GetBankSalesDashboardData(DateOnly dateFrom, DateOnly dateTo);
        Task<IEnumerable<int>> GetSalesOrderDashboardData(DateOnly dateFrom, DateOnly dateTo);
        Task<IEnumerable<PaymentByTypeContainer>> GetPaymentByTypeDashboardData(DateOnly dateFrom, DateOnly dateTo);


        Task<ExpenseDashboardContainer> GetExpenseDashboardData(DateOnly dateFrom, DateOnly dateTo);
        Task<IncomeDashboardContainer> GetIncomeDashboardData(DateOnly dateFrom, DateOnly dateTo);
        Task<TotalOrderDashboardContainer> GetTotalOrderDashboardData(DateOnly dateFrom, DateOnly dateTo);
        Task<AccountReceivableDashboardContainer> GetAccountReceivableDashboardData(DateOnly dateFrom, DateOnly dateTo);
    }
}