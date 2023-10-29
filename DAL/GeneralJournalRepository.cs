using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;

namespace WebAPI.DAL
{
    public sealed class GeneralJournalRepository : IGeneralJournalRepository
    {
        private readonly ApplicationContext _context;

        public GeneralJournalRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GeneralJournalContainer>> GetGeneralJournalData()
        {
            IEnumerable<GeneralJournalContainer> generalJournalData = Enumerable.Empty<GeneralJournalContainer>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // EF bug. Need to convert to AsEnumerable for first query.
                    generalJournalData = (await GetGeneralJournalQuery().ToListAsync())
                                        .Concat(GetPurchaseDebitQuery())
                                        .Concat(GetPurchaseCreditQuery())
                                        .Concat(GetSalesDebitQuery())
                                        .Concat(GetSalesCreditQuery())
                                        .OrderBy(journal => journal.TransactionNo);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data jurnal umum", ex);
                }
            }

            return generalJournalData;
        }

        public IQueryable<GeneralJournalContainer> GetGeneralJournalQuery()
        {
            return _context.GeneralAccountHeaders.GroupJoin(_context.GeneralAccountDetails.Join(_context.ChartOfAccounts,
                                                                                                                   detail => detail.ChartOfAccountId,
                                                                                                                   coa => coa.Id,
                                                                                                                   (detail, coa) => new
                                                                                                                   {
                                                                                                                       GeneralAccountHeaderId = detail.GeneralAccountHeaderId,
                                                                                                                       AccountNo = coa.AccountNo,
                                                                                                                       Debit = detail.Debit,
                                                                                                                       Credit = detail.Credit,
                                                                                                                   }),
                                                                              header => header.Id,
                                                                              detail => detail.GeneralAccountHeaderId,
                                                                              (header, details) => new
                                                                              {
                                                                                  TransactionNo = header.TransactionNo,
                                                                                  TransactionDate = header.TransactionDate,
                                                                                  TransactionDetails = details,
                                                                                  Description = header.Description
                                                                              })
                                                                    .SelectMany(header => header.TransactionDetails.DefaultIfEmpty(),
                                                                                (header, detail) => new
                                                                                {
                                                                                    TransactionNo = header.TransactionNo,
                                                                                    TransactionDate = header.TransactionDate,
                                                                                    Description = header.Description,
                                                                                    AccountNo = detail != null ? (int?)detail.AccountNo : null,
                                                                                    Debit = detail != null ? (decimal?)detail.Debit : null,
                                                                                    Credit = detail != null ? (decimal?)detail.Credit : null,
                                                                                })
                                                                    .GroupBy(result => new
                                                                    {
                                                                        result.TransactionNo,
                                                                        result.TransactionDate,
                                                                        result.AccountNo,
                                                                        result.Description
                                                                    })
                                                                    .Select(group => new GeneralJournalContainer
                                                                    {
                                                                        TransactionNo = group.Key.TransactionNo,
                                                                        TransactionDate = group.Key.TransactionDate,
                                                                        Description = group.Key.Description,
                                                                        AccountNo = group.Key.AccountNo,
                                                                        Debit = group.Sum(item => item.Debit) ?? 0m,
                                                                        Credit = group.Sum(item => item.Credit) ?? 0m
                                                                    });
        }

        public IQueryable<GeneralJournalContainer> GetPurchaseDebitQuery()
        {
            return _context.PurchaseHeaders.GroupJoin(_context.PurchaseDetails,
                                                                              header => header.Id,
                                                                              detail => detail.PurchaseHeaderId,
                                                                              (header, details) => new
                                                                              {
                                                                                  TransactionNo = header.PurchaseNo,
                                                                                  TransactionDate = header.PurchaseDate,
                                                                                  TransactionDetails = details,
                                                                                  Description = header.Description
                                                                              })
                                                                    .SelectMany(header => header.TransactionDetails.DefaultIfEmpty(),
                                                                                (header, detail) => new
                                                                                {
                                                                                    TransactionNo = header.TransactionNo,
                                                                                    TransactionDate = header.TransactionDate,
                                                                                    Description = header.Description,
                                                                                    AccountNo = detail != null ? (int?)113 : null,
                                                                                    Debit = detail != null ? (decimal?)(detail.Price * detail.Quantity) : null,
                                                                                    Credit = detail != null ? (decimal?)0m : null,
                                                                                })
                                                                    .GroupBy(result => new
                                                                    {
                                                                        result.TransactionNo,
                                                                        result.TransactionDate,
                                                                        result.AccountNo,
                                                                        result.Description,
                                                                    })
                                                                    .Select(group => new GeneralJournalContainer
                                                                    {
                                                                        TransactionNo = group.Key.TransactionNo,
                                                                        TransactionDate = group.Key.TransactionDate,
                                                                        Description = group.Key.Description,
                                                                        AccountNo = group.Key.AccountNo,
                                                                        Debit = group.Sum(item => item.Debit) ?? 0m,
                                                                        Credit = 0m
                                                                    });
        }

        public IQueryable<GeneralJournalContainer> GetPurchaseCreditQuery()
        {
            return _context.PurchaseHeaders.GroupJoin(_context.PurchaseDetails,
                                                                              header => header.Id,
                                                                              detail => detail.PurchaseHeaderId,
                                                                              (header, details) => new
                                                                              {
                                                                                  TransactionNo = header.PurchaseNo,
                                                                                  TransactionDate = header.PurchaseDate,
                                                                                  TransactionDetails = details,
                                                                                  Description = header.Description
                                                                              })
                                                                    .SelectMany(header => header.TransactionDetails.DefaultIfEmpty(),
                                                                                (header, detail) => new
                                                                                {
                                                                                    TransactionNo = header.TransactionNo,
                                                                                    TransactionDate = header.TransactionDate,
                                                                                    Description = header.Description,
                                                                                    AccountNo = detail != null ? (int?)111 : null,
                                                                                    Debit = detail != null ? (decimal?)0m : null,
                                                                                    Credit = detail != null ? (decimal?)(detail.Price * detail.Quantity) : null,
                                                                                })
                                                                    .GroupBy(result => new
                                                                    {
                                                                        result.TransactionNo,
                                                                        result.TransactionDate,
                                                                        result.AccountNo,
                                                                        result.Description,
                                                                    })
                                                                    .Select(group => new GeneralJournalContainer
                                                                    {
                                                                        TransactionNo = group.Key.TransactionNo,
                                                                        TransactionDate = group.Key.TransactionDate,
                                                                        Description = group.Key.Description,
                                                                        AccountNo = group.Key.AccountNo,
                                                                        Debit = 0m,
                                                                        Credit = group.Sum(item => item.Credit) ?? 0m,
                                                                    });
        }

        public IQueryable<GeneralJournalContainer> GetSalesDebitQuery()
        {

            return _context.SalesHeaders.GroupJoin(_context.SalesDetails,
                                                              header => header.Id,
                                                              detail => detail.SalesHeaderId,
                                                              (header, details) => new
                                                              {
                                                                  TransactionNo = header.SalesNo,
                                                                  TransactionDate = header.SalesDate,
                                                                  TransactionDetails = details,
                                                                  Description = header.Description
                                                              })
                                                   .SelectMany(header => header.TransactionDetails.DefaultIfEmpty(),
                                                               (header, detail) => new
                                                               {
                                                                   TransactionNo = header.TransactionNo,
                                                                   TransactionDate = header.TransactionDate,
                                                                   Description = header.Description,
                                                                   AccountNo = detail != null ? (int?)111 : null,
                                                                   Debit = detail != null ? (decimal?)(detail.Price * detail.Quantity) : null,
                                                                   Credit = detail != null ? (decimal?)0m : null,
                                                               })
                                                   .GroupBy(result => new
                                                   {
                                                       result.TransactionNo,
                                                       result.TransactionDate,
                                                       result.AccountNo,
                                                       result.Description,
                                                   })
                                                   .Select(group => new GeneralJournalContainer
                                                   {
                                                       TransactionNo = group.Key.TransactionNo,
                                                       TransactionDate = group.Key.TransactionDate,
                                                       Description = group.Key.Description,
                                                       AccountNo = group.Key.AccountNo,
                                                       Debit = group.Sum(item => item.Debit) ?? 0m,
                                                       Credit = 0m,
                                                   });

        }

        public IQueryable<GeneralJournalContainer> GetSalesCreditQuery()
        {
            return _context.SalesHeaders.GroupJoin(_context.SalesDetails,
                                                                  header => header.Id,
                                                                  detail => detail.SalesHeaderId,
                                                                  (header, details) => new
                                                                  {
                                                                      TransactionNo = header.SalesNo,
                                                                      TransactionDate = header.SalesDate,
                                                                      TransactionDetails = details,
                                                                      Description = header.Description
                                                                  })
                                                       .SelectMany(header => header.TransactionDetails.DefaultIfEmpty(),
                                                                   (header, detail) => new
                                                                   {
                                                                       TransactionNo = header.TransactionNo,
                                                                       TransactionDate = header.TransactionDate,
                                                                       Description = header.Description,
                                                                       AccountNo = detail != null ? (int?)401 : null,
                                                                       Debit = detail != null ? (decimal?)0m : null,
                                                                       Credit = detail != null ? (decimal?)(detail.Price * detail.Quantity) : null,
                                                                   })
                                                       .GroupBy(result => new
                                                       {
                                                           result.TransactionNo,
                                                           result.TransactionDate,
                                                           result.AccountNo,
                                                           result.Description,
                                                       })
                                                       .Select(group => new GeneralJournalContainer
                                                       {
                                                           TransactionNo = group.Key.TransactionNo,
                                                           TransactionDate = group.Key.TransactionDate,
                                                           Description = group.Key.Description,
                                                           AccountNo = group.Key.AccountNo,
                                                           Debit = 0m,
                                                           Credit = group.Sum(item => item.Credit) ?? 0m,
                                                       });
        }

    }

    public class GeneralJournalContainer
    {
        public string TransactionNo { get; set; } = default!;
        public DateOnly TransactionDate { get; set; }
        public string? Description { get; set; }
        public int? AccountNo { get; set; }

        [Precision(19, 3)]
        public Decimal? Debit { get; set; }

        [Precision(19, 3)]
        public Decimal? Credit { get; set; }
    }
}
