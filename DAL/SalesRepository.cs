using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ApplicationContext _context;

        public SalesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesReportContainer>> GetSalesReport()
        {
            IEnumerable<SalesReportContainer> salesReport = Enumerable.Empty<SalesReportContainer>();

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    salesReport = await _context.SalesHeaders.GroupJoin(_context.SalesDetails,
                                                                        header => header.Id,
                                                                        detail => detail.SalesHeaderId,
                                                                        (header, details) => new
                                                                        {
                                                                            SalesNo = header.SalesNo,
                                                                            SalesDate = header.SalesDate,
                                                                            Description = header.Description,
                                                                            Details = details
                                                                        })
                                                             .SelectMany(header => header.Details.DefaultIfEmpty(),
                                                                         (header, detail) => new
                                                                         {
                                                                             SalesNo = header.SalesNo,
                                                                             SalesDate = header.SalesDate,
                                                                             Description = header.Description,
                                                                             InventoryNo = detail != null ? (string?)detail.Inventory.ItemNo : null,
                                                                             Quantity = detail != null ? (decimal?)detail.Quantity : null,
                                                                             Price = detail != null ? (decimal?)detail.Price : null,
                                                                             Total = detail != null ? (decimal?)(detail.Quantity * detail.Price) : null,
                                                                         })
                                                             .GroupBy(result => new
                                                             {
                                                                 SalesNo = result.SalesNo,
                                                                 SalesDate = result.SalesDate,
                                                                 Description = result.Description,
                                                                 InventoryNo = result.InventoryNo,
                                                                 Quantity = result.Quantity,
                                                                 Price = result.Price,
                                                             })
                                                             .Select(data => new SalesReportContainer
                                                             {
                                                                 SalesNo = data.Key.SalesNo,
                                                                 SalesDate = data.Key.SalesDate,
                                                                 Description = data.Key.Description,
                                                                 InventoryNo = data.Key.InventoryNo,
                                                                 Quantity  = data.Key.Quantity,
                                                                 Price  = data.Key.Price,
                                                                 Total  = data.Sum(item => item.Total),
                                                             })
                                                             .ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan laporan penjualan.", ex);
                }
            }
            

            return salesReport;
        }

        //public async Task<IEnumerable<SalesReportContainer>> GetSalesReport(DateOnly startDate, DateOnly endDate)
        //{
        //    IEnumerable<SalesReportContainer> salesReport = Enumerable.Empty<SalesReportContainer>();

        //    await using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            salesReport = await _context.Database.SqlQuery<SalesReportContainer>($"GetSalesReportWithDateSP {startDate}, {endDate}").ToListAsync();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan laporan penjualan.", ex);
        //        }
        //    }


        //    return salesReport;
        //}
    }
    public class SalesReportContainer
    {
        public string SalesNo { get; set; } = default!;
        public DateOnly SalesDate { get; set; }
        public string? Description { get; set; }
        public string? InventoryNo { get; set; }
        public Decimal? Quantity { get; set; }
        public Decimal? Price { get; set; }
        public Decimal? Total { get; set; }
    }
}
