using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public sealed class GeneralJournalRepository : IGeneralJournalRepository
    {
        private readonly ApplicationContext _context;

        public GeneralJournalRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<GeneralJournalHeader>> GetAllGeneralJournalHeaders()
        {
            IEnumerable<GeneralJournalHeader> generalJournalHeaders = Enumerable.Empty<GeneralJournalHeader>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    generalJournalHeaders = await _context.GeneralJournalHeaders
                                                            .Include(header => header.GeneralJournalDetails!)
                                                            .ThenInclude(detail => detail.ChartOfAccount)
                                                            .ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data header jurnal umum.", ex);
                }
            }

            return generalJournalHeaders;
        }

        public async Task<GeneralJournalHeader?> GetGeneralJournalHeaderById(Guid id)
        {
            GeneralJournalHeader? generalJournalHeader = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    generalJournalHeader = await _context.GeneralJournalHeaders
                                                            .Include(header => header.GeneralJournalDetails!)
                                                            .ThenInclude(detail => detail.ChartOfAccount)
                                                            .FirstOrDefaultAsync(header => header.Id == id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data header jurnal umum dengan id: {id}", ex);
                }
            }

            return generalJournalHeader;
        }

        public async Task InsertGeneralJournalHeader(GeneralJournalHeader generalJournalHeader)
        {
            if (generalJournalHeader.TransactionNo.Trim() == string.Empty)
                throw new DatabaseInsertException("Nomor Transaksi tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.GeneralJournalHeaders.AddAsync(generalJournalHeader);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data header jurnal umum dengan nomor transaksi: {generalJournalHeader.TransactionNo}.
                        Nomor transaksi '{generalJournalHeader.TransactionNo}' sudah digunakan. Pastikan anda menggunakan nomor transaksi yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan header jurnal umum dengan nomor transaksi: {generalJournalHeader.TransactionNo}", ex);
                }
            }
        }
        public async Task UpdateGeneralJournalHeader(GeneralJournalHeader generalJournalHeader)
        {
            if (generalJournalHeader.TransactionNo.Trim() == string.Empty)
                throw new DatabaseInsertException("Nomor Transaksi tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.GeneralJournalHeaders.Update(generalJournalHeader);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data header jurnal umum dengan nomor transaksi: {generalJournalHeader.TransactionNo}.
                        Nomor transaksi '{generalJournalHeader.TransactionNo}' sudah digunakan. Pastikan anda menggunakan nomor transaksi yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data header jurnal umum dengan nomor transaksi: {generalJournalHeader.TransactionNo}", ex);
                }
            }
        }
        public async Task DeleteGeneralJournalHeader(GeneralJournalHeader generalJournalHeader)
        {

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.GeneralJournalHeaders.Remove(generalJournalHeader);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data header jurnal umum dengan nomor transaksi: {generalJournalHeader.TransactionNo}", ex);
                }
            }
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
            return _context.GeneralJournalHeaders.GroupJoin(_context.GeneralJournalDetails.Join(_context.ChartOfAccounts,
                                                                                                                   detail => detail.ChartOfAccountId,
                                                                                                                   coa => coa.Id,
                                                                                                                   (detail, coa) => new
                                                                                                                   {
                                                                                                                       GeneralJournalHeaderId = detail.GeneralJournalHeaderId,
                                                                                                                       AccountNo = coa.AccountNo,
                                                                                                                       Debit = detail.Debit,
                                                                                                                       Credit = detail.Credit,
                                                                                                                   }),
                                                                              header => header.Id,
                                                                              detail => detail.GeneralJournalHeaderId,
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
                                                                                    AccountNo = detail != null ? (int?)501 : null,
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
