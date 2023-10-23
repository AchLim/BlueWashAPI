using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface ISalesRepository
    {
        Task<IEnumerable<SalesReportContainer>> GetSalesReport();
        //Task<IEnumerable<SalesReportContainer>> GetSalesReport(DateOnly startDate, DateOnly endDate);
    }
}
