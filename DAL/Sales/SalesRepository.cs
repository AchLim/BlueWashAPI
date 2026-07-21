using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Enum;
using WebAPI.Exception;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public sealed class SalesRepository : ISalesRepository
    {
        private readonly ApplicationContext _context;

        public SalesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<string> GetNextSequenceSalesNo(string salesDate)
        {
            var result = (await _context.Database.SqlQuery<string>($"EXECUTE GetSalesNo {salesDate};").ToListAsync()).FirstOrDefault();
            return result!;
        }

        public async Task<IEnumerable<SalesHeader>> GetAllSalesHeaders()
        {
            IEnumerable<SalesHeader> salesHeaders = Enumerable.Empty<SalesHeader>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    salesHeaders = await _context.SalesHeaders
                                                            .Include(header => header.SalesDetails)
                                                            .Include(header => header.Customer!)
                                                            .ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data penjualan.", ex);
                }
            }

            return salesHeaders;
        }

        public async Task<SalesHeader?> GetSalesHeaderById(Guid id)
        {
            SalesHeader? salesHeader = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    salesHeader = await _context.SalesHeaders
                                                            .Include(header => header.Customer)
                                                            .Include(header => header.SalesDetails!)
                                                            .ThenInclude(detail => detail.PriceMenu)
                                                            .ThenInclude(detail => detail.LaundryService)
                                                            .ThenInclude(detail => detail.PriceMenus)
                                                            .Include(header => header.SalesPayments!.OrderBy(sp => sp.PaymentDate))
                                                            .FirstOrDefaultAsync(header => header.Id == id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data penjualan dengan id: {id}", ex);
                }
            }

            return salesHeader;
        }

        public async Task InsertSalesHeader(SalesHeader salesHeader)
        {
            if (salesHeader.SalesNo.Trim() == string.Empty)
                throw new DatabaseInsertException("Nomor Penjualan tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    salesHeader.SalesNo = "/";
                    await _context.SalesHeaders.AddAsync(salesHeader);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data penjualan dengan nomor penjualan: {salesHeader.SalesNo}.
                        Nomor transaksi '{salesHeader.SalesNo}' sudah digunakan. Pastikan anda menggunakan nomor penjualan yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data penjualan dengan nomor transaksi: {salesHeader.SalesNo}", ex);
                }
            }
        }
        public async Task UpdateSalesHeader(SalesHeader salesHeader)
        {
            if (salesHeader.SalesNo.Trim() == string.Empty)
                throw new DatabaseInsertException("Nomor Transaksi tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (salesHeader.SalesNo == "/" && salesHeader.Status == EntryStatus.Posted.ToString())
                    {
                        var newSalesNo = await GetNextSequenceSalesNo(salesHeader.SalesDate.ToString("yyyy-MM-dd"));
                        bool exist = await _context.SalesHeaders.AnyAsync(sh => sh.SalesNo == newSalesNo);
                        if (exist)
                            throw new UniqueConstraintException();

                        salesHeader.SalesNo = newSalesNo;
                    }

                    _context.SalesHeaders.Update(salesHeader);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data penjualan dengan nomor penjualan: {salesHeader.SalesNo}.
                        Nomor penjualan '{salesHeader.SalesNo}' sudah digunakan. Pastikan anda menggunakan nomor penjualan yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data penjualan dengan nomor penjualan: {salesHeader.SalesNo}", ex);
                }
            }
        }
        public async Task DeleteSalesHeader(SalesHeader salesHeader)
        {

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.SalesHeaders.Remove(salesHeader);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data penjualan dengan nomor penjualan: {salesHeader.SalesNo}", ex);
                }
            }
        }

        public async Task<IEnumerable<SalesPayment>> GetAllSalesPayments()
        {
            IEnumerable<SalesPayment> purchasePayments = Enumerable.Empty<SalesPayment>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    purchasePayments = await _context.SalesPayments
                                                     .Include(pp => pp.SalesHeader)
                                                     .ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data pembayaran penjualan.", ex);
                }
            }

            return purchasePayments;
        }

        //public async Task<IEnumerable<ItemSalesContainer>> GetTotalItemSalesData()
        //{
        //    IEnumerable<ItemSalesContainer> itemSalesData = Enumerable.Empty<ItemSalesContainer>();
        //    await using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            itemSalesData = await GetTotalItemSalesQuery().ToListAsync();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data penjualan barang.", ex);
        //        }
        //    }

        //    return itemSalesData;
        //}

        //public async Task<IEnumerable<SalesPaymentContainer>> GetTotalSalesPaymentData()
        //{
        //    IEnumerable<SalesPaymentContainer> salesPaymentData = Enumerable.Empty<SalesPaymentContainer>();
        //    await using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            salesPaymentData = await GetTotalSalesPaymentQuery().ToListAsync();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data pembayaran penjualan.", ex);
        //        }
        //    }

        //    return salesPaymentData;
        //}
        //public async Task<IEnumerable<SalesPerInvoiceContainer>> GetTotalSalesPerInvoiceData()
        //{
        //    IEnumerable<SalesPerInvoiceContainer> salesPerInvoice = Enumerable.Empty<SalesPerInvoiceContainer>();
        //    await using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            salesPerInvoice = await GetTotalSalesPerInvoiceQuery().ToListAsync();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data penjualan per invoice.", ex);
        //        }
        //    }

        //    return salesPerInvoice;
        //}

        //public async Task<IEnumerable<AccountReceivableBalanceContainer>> GetAccountReceivableBalanceData()
        //{
        //    IEnumerable<AccountReceivableBalanceContainer> accountReceivableBalance = Enumerable.Empty<AccountReceivableBalanceContainer>();
        //    await using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            accountReceivableBalance = await GetAccountReceivableBalanceQuery().ToListAsync();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data saldo piutang.", ex);
        //        }
        //    }

        //    return accountReceivableBalance;
        //}

        //public async Task<IEnumerable<SalesReportContainer>> GetSalesData()
        //{
        //    IEnumerable<SalesReportContainer> salesReport = Enumerable.Empty<SalesReportContainer>();

        //    await using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            salesReport = await GetSalesQuery().ToListAsync();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan laporan penjualan.", ex);
        //        }
        //    }


        //    return salesReport;
        //}


        //    #region Queries

        //    private IQueryable<ItemSalesContainer> GetTotalItemSalesQuery()
        //    {
        //        return _context.Customers
        //                        .Join(_context.SalesHeaders,
        //                                customer => customer.Id,
        //                                header => header.CustomerId,
        //                                (customer, headers) => new
        //                                {
        //                                    SalesHeaderId = headers.Id,
        //                                    SalesNo = headers.SalesNo
        //                                })
        //                        .Join(_context.SalesDetails,
        //                                customerSalesHeader => customerSalesHeader.SalesHeaderId,
        //                                detail => detail.SalesHeaderId,
        //                                (customerSalesHeader, detail) => new
        //                                {
        //                                    SalesNo = customerSalesHeader.SalesNo,
        //                                    PriceMenuId = detail.PriceMenuId,
        //                                    Quantity = detail.Quantity,
        //                                    Price = detail.Price,
        //                                    Subtotal = detail.Quantity * detail.Price
        //                                })
        //                        .Join(_context.PriceMenus,
        //                                customerHeaderDetail => customerHeaderDetail.PriceMenuId,
        //                                priceMenu => priceMenu.PriceMenuId,
        //                                (customerHeaderDetail, priceMenus) => new
        //                                {
        //                                    SalesNo = customerHeaderDetail.SalesNo,
        //                                    MenuName = priceMenus.Name,
        //                                    Quantity = customerHeaderDetail.Quantity,
        //                                    Price = customerHeaderDetail.Price,
        //                                    Subtotal = customerHeaderDetail.Subtotal
        //                                })
        //                        .GroupBy(result => new
        //                        {
        //                            result.SalesNo,
        //                            result.MenuName,
        //                            result.Quantity,
        //                            result.Price,
        //                            result.Subtotal
        //                        })
        //                        .Select(group => new ItemSalesContainer
        //                        {
        //                            SalesNo = group.Key.SalesNo,
        //                            MenuName = group.Key.MenuName,
        //                            Quantity = group.Key.Quantity,
        //                            Price = group.Key.Price,
        //                            Subtotal = group.Key.Subtotal
        //                        });
        //    }

        //    private IQueryable<SalesPaymentContainer> GetTotalSalesPaymentQuery()
        //    {
        //        return _context.SalesHeaders
        //                                .Join(_context.SalesPayments,
        //                                    header => header.Id,
        //                                    payment => payment.SalesHeaderId,
        //                                    (header, payment) => new
        //                                    {
        //                                        SalesNo = header.SalesNo,
        //                                        SumOfAmount = payment.Amount
        //                                    })
        //                                .GroupBy(result => new { result.SalesNo })
        //                                .Select(group => new SalesPaymentContainer
        //                                {
        //                                    SalesNo = group.Key.SalesNo,
        //                                    SumOfAmount = group.Sum(item => item.SumOfAmount)
        //                                });
        //    }

        //    private IQueryable<SalesPerInvoiceContainer> GetTotalSalesPerInvoiceQuery()
        //    {
        //        return _context.Customers.Join(GetTotalItemSalesQuery().Join(_context.SalesHeaders,
        //                                                                     itemSales => itemSales.SalesNo,
        //                                                                     header => header.SalesNo,
        //                                                                     (itemSales, header) => new
        //                                                                     {
        //                                                                         SalesNo = header.SalesNo,
        //                                                                         SalesDate = header.SalesDate,
        //                                                                         CustomerId = header.CustomerId,
        //                                                                         Subtotal = itemSales.Subtotal,
        //                                                                     }),
        //                                       customer => customer.Id,
        //                                       itemSalesHeader => itemSalesHeader.CustomerId,
        //                                       (customer, itemSalesHeader) => new
        //                                       {
        //                                           SalesNo = itemSalesHeader.SalesNo,
        //                                           SalesDate = itemSalesHeader.SalesDate,
        //                                           CustomerCode = customer.CustomerCode,
        //                                           CustomerName = customer.CustomerName,
        //                                           Subtotal = itemSalesHeader.Subtotal,
        //                                       })
        //                                .GroupBy(result => new
        //                                {
        //                                    result.SalesNo,
        //                                    result.SalesDate,
        //                                    result.CustomerCode,
        //                                    result.CustomerName,
        //                                })
        //                                .OrderBy(result => result.Key.SalesNo)
        //                                .Select(group => new SalesPerInvoiceContainer
        //                                {
        //                                    SalesNo = group.Key.SalesNo,
        //                                    SalesDate = group.Key.SalesDate,
        //                                    CustomerCode = group.Key.CustomerCode,
        //                                    CustomerName = group.Key.CustomerName,
        //                                    SumOfSubtotal = group.Sum(item => item.Subtotal)
        //                                });
        //    }

        //    private IQueryable<AccountReceivableBalanceContainer> GetAccountReceivableBalanceQuery()
        //    {
        //        return GetTotalSalesPerInvoiceQuery().GroupJoin(GetTotalSalesPaymentQuery(),
        //                                                        salesPerInvoice => salesPerInvoice.SalesNo,
        //                                                        salesPayment => salesPayment.SalesNo,
        //                                                        (salesPerInvoice, salesPayments) => new
        //                                                        {
        //                                                            SalesNo = salesPerInvoice.SalesNo,
        //                                                            SalesDate = salesPerInvoice.SalesDate,
        //                                                            CustomerCode = salesPerInvoice.CustomerCode,
        //                                                            CustomerName = salesPerInvoice.CustomerName,
        //                                                            Subtotal = salesPerInvoice.SumOfSubtotal,
        //                                                            SalesPayment = salesPayments
        //                                                        })
        //                                            .SelectMany(sales => sales.SalesPayment.DefaultIfEmpty(),
        //                                                        (salesPerInvoice, salesPayment) => new
        //                                                        {
        //                                                            SalesNo = salesPerInvoice.SalesNo,
        //                                                            SalesDate = salesPerInvoice.SalesDate,
        //                                                            CustomerCode = salesPerInvoice.CustomerCode,
        //                                                            CustomerName = salesPerInvoice.CustomerName,
        //                                                            Subtotal = salesPerInvoice.Subtotal,
        //                                                            PaidAmount = salesPayment != null ? (decimal?)salesPayment.SumOfAmount : null
        //                                                        })
        //                                            .GroupBy(result => new
        //                                            {
        //                                                result.SalesNo,
        //                                                result.SalesDate,
        //                                                result.CustomerCode,
        //                                                result.CustomerName,
        //                                                result.Subtotal,
        //                                                result.PaidAmount
        //                                            })
        //                                            .Select(group => new AccountReceivableBalanceContainer
        //                                            {
        //                                                SalesNo = group.Key.SalesNo,
        //                                                SalesDate = group.Key.SalesDate,
        //                                                CustomerCode = group.Key.CustomerCode,
        //                                                CustomerName = group.Key.CustomerName,
        //                                                SumOfSubtotal = group.Sum(item => item.Subtotal),
        //                                                SumOfPaidAmount = group.Sum(item => item.PaidAmount) ?? 0,
        //                                            });
        //    }

        //    private IQueryable<SalesReportContainer> GetSalesQuery()
        //    {
        //        return _context.SalesHeaders.GroupJoin(_context.SalesDetails,
        //                                                header => header.Id,
        //                                                detail => detail.SalesHeaderId,
        //                                                (header, details) => new
        //                                                {
        //                                                    SalesNo = header.SalesNo,
        //                                                    SalesDate = header.SalesDate,
        //                                                    Description = header.Description,
        //                                                    Details = details
        //                                                })
        //                                        .SelectMany(header => header.Details.DefaultIfEmpty(),
        //                                                    (header, detail) => new
        //                                                    {
        //                                                        SalesNo = header.SalesNo,
        //                                                        SalesDate = header.SalesDate,
        //                                                        Description = header.Description,
        //                                                        MenuName = detail != null ? (string?)detail.PriceMenu.Name : null,
        //                                                        Quantity = detail != null ? (decimal?)detail.Quantity : null,
        //                                                        Price = detail != null ? (decimal?)detail.Price : null,
        //                                                        Total = detail != null ? (decimal?)(detail.Quantity * detail.Price) : null,
        //                                                    })
        //                                        .GroupBy(result => new
        //                                        {
        //                                            SalesNo = result.SalesNo,
        //                                            SalesDate = result.SalesDate,
        //                                            Description = result.Description,
        //                                            MenuName = result.MenuName,
        //                                            Quantity = result.Quantity,
        //                                            Price = result.Price,
        //                                        })
        //                                        .Select(data => new SalesReportContainer
        //                                        {
        //                                            SalesNo = data.Key.SalesNo,
        //                                            SalesDate = data.Key.SalesDate,
        //                                            Description = data.Key.Description,
        //                                            MenuName = data.Key.MenuName,
        //                                            Quantity = data.Key.Quantity ?? 0,
        //                                            Price = data.Key.Price ?? 0,
        //                                            Total = data.Sum(item => item.Total) ?? 0,
        //                                        });
        //    }

        //    #endregion
        //}
        //public class ItemSalesContainer
        //{
        //    public string SalesNo { get; set; } = default!;
        //    public string MenuName { get; set; } = default!;

        //    [Precision(19, 3)]
        //    public decimal Quantity { get; set; }

        //    [Precision(19, 3)]
        //    public decimal Price { get; set; }

        //    [Precision(19, 3)]
        //    public decimal Subtotal { get; set; }
        //}
        //public class SalesPaymentContainer
        //{
        //    public string SalesNo { get; set; } = default!;

        //    [Precision(19, 3)]
        //    public decimal SumOfAmount { get; set; }
        //}
        //public class SalesReportContainer
        //{
        //    public string SalesNo { get; set; } = default!;
        //    public DateOnly SalesDate { get; set; }
        //    public string? Description { get; set; }
        //    public string? MenuName { get; set; }

        //    [Precision(19, 3)]
        //    public decimal Quantity { get; set; }

        //    [Precision(19, 3)]
        //    public decimal Price { get; set; }

        //    [Precision(19, 3)]
        //    public decimal Total { get; set; }
        //}

        //public class SalesPerInvoiceContainer
        //{
        //    public string SalesNo { get; set; } = default!;
        //    public DateOnly SalesDate { get; set; }
        //    public string CustomerCode { get; set; } = default!;
        //    public string CustomerName { get; set; } = default!;

        //    [Precision(19, 3)]
        //    public decimal SumOfSubtotal { get; set; }
        //}

        //public class AccountReceivableBalanceContainer : SalesPerInvoiceContainer
        //{

        //    [Precision(19, 2)]
        //    public decimal SumOfPaidAmount { get; set; }

        //    [Precision(19, 2)]
        //    public decimal AccountReceivableBalance => SumOfSubtotal - SumOfPaidAmount;
        //}
    }
}
