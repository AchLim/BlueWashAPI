using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public sealed class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly ApplicationContext _context;

        public JournalEntryRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<JournalEntry>> GetAllJournalEntries()
        {
            IEnumerable<JournalEntry> journalEntries = Enumerable.Empty<JournalEntry>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    journalEntries = await _context.JournalEntries
                                                            .Include(entry => entry.JournalItems!)
                                                            .ThenInclude(items => items.ChartOfAccount)
                                                            .ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data jurnal.", ex);
                }
            }

            return journalEntries;
        }

        public async Task<JournalEntry?> GetJournalEntryById(Guid id)
        {
            JournalEntry? journalEntry = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    journalEntry = await _context.JournalEntries
                                                            .Include(entry => entry.JournalItems!)
                                                            .ThenInclude(detail => detail.ChartOfAccount)
                                                            .FirstOrDefaultAsync(header => header.Id == id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data jurnal dengan id: {id}", ex);
                }
            }

            return journalEntry;
        }

        public async Task InsertJournalEntry(JournalEntry journalEntry)
        {
            if (journalEntry.TransactionNo.Trim() == string.Empty)
                throw new DatabaseInsertException("Nomor Transaksi tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.JournalEntries.AddAsync(journalEntry);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data jurnal dengan nomor transaksi: {journalEntry.TransactionNo}.
                        Nomor transaksi '{journalEntry.TransactionNo}' sudah digunakan. Pastikan anda menggunakan nomor transaksi yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan jurnal dengan nomor transaksi: {journalEntry.TransactionNo}", ex);
                }
            }
        }
        public async Task UpdateJournalEntry(JournalEntry journalEntry)
        {
            if (journalEntry.TransactionNo.Trim() == string.Empty)
                throw new DatabaseInsertException("Nomor Transaksi tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.JournalEntries.Update(journalEntry);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data jurnal dengan nomor transaksi: {journalEntry.TransactionNo}.
                        Nomor transaksi '{journalEntry.TransactionNo}' sudah digunakan. Pastikan anda menggunakan nomor transaksi yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data jurnal dengan nomor transaksi: {journalEntry.TransactionNo}", ex);
                }
            }
        }
        public async Task DeleteJournalEntry(JournalEntry journalEntry)
        {

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.JournalEntries.Remove(journalEntry);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data jurnal dengan nomor transaksi: {journalEntry.TransactionNo}", ex);
                }
            }
        }



        public async Task<IEnumerable<JournalEntryContainer>> GetJournalEntryData()
        {
            IEnumerable<JournalEntryContainer> generalJournalData = Enumerable.Empty<JournalEntryContainer>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // EF bug. Need to convert to AsEnumerable for first query.
                    generalJournalData = (await GetJournalEntryQuery().ToListAsync())
                                        .Concat(GetPurchaseDebitQuery())
                                        .Concat(GetPurchaseCreditQuery())
                                        .Concat(GetSalesDebitQuery())
                                        .Concat(GetSalesCreditQuery())
                                        .OrderBy(journal => journal.TransactionNo);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data jurnal", ex);
                }
            }

            return generalJournalData;
        }

        public IQueryable<JournalEntryContainer> GetJournalEntryQuery()
        {
            return _context.JournalEntries.GroupJoin(_context.JournalItems.Join(_context.ChartOfAccounts,
                                                                                item => item.ChartOfAccountId,
                                                                                coa => coa.Id,
                                                                                (item, coa) => new
                                                                                {
                                                                                    JournalEntryId = item.JournalEntryId,
                                                                                    AccountNo = coa.AccountNo,
                                                                                    Debit = item.Debit,
                                                                                    Credit = item.Credit,
                                                                                }),
                                                                              header => header.Id,
                                                                              item => item.JournalEntryId,
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
                                                                    .Select(group => new JournalEntryContainer
                                                                    {
                                                                        TransactionNo = group.Key.TransactionNo,
                                                                        TransactionDate = group.Key.TransactionDate,
                                                                        Description = group.Key.Description,
                                                                        AccountNo = group.Key.AccountNo,
                                                                        Debit = group.Sum(item => item.Debit) ?? 0m,
                                                                        Credit = group.Sum(item => item.Credit) ?? 0m
                                                                    });
        }

        public IQueryable<JournalEntryContainer> GetPurchaseDebitQuery()
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
                                                                    .Select(group => new JournalEntryContainer
                                                                    {
                                                                        TransactionNo = group.Key.TransactionNo,
                                                                        TransactionDate = group.Key.TransactionDate,
                                                                        Description = group.Key.Description,
                                                                        AccountNo = group.Key.AccountNo,
                                                                        Debit = group.Sum(item => item.Debit) ?? 0m,
                                                                        Credit = 0m
                                                                    });
        }

        public IQueryable<JournalEntryContainer> GetPurchaseCreditQuery()
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
                                                                    .Select(group => new JournalEntryContainer
                                                                    {
                                                                        TransactionNo = group.Key.TransactionNo,
                                                                        TransactionDate = group.Key.TransactionDate,
                                                                        Description = group.Key.Description,
                                                                        AccountNo = group.Key.AccountNo,
                                                                        Debit = 0m,
                                                                        Credit = group.Sum(item => item.Credit) ?? 0m,
                                                                    });
        }

        public IQueryable<JournalEntryContainer> GetSalesDebitQuery()
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
                                                   .Select(group => new JournalEntryContainer
                                                   {
                                                       TransactionNo = group.Key.TransactionNo,
                                                       TransactionDate = group.Key.TransactionDate,
                                                       Description = group.Key.Description,
                                                       AccountNo = group.Key.AccountNo,
                                                       Debit = group.Sum(item => item.Debit) ?? 0m,
                                                       Credit = 0m,
                                                   });

        }

        public IQueryable<JournalEntryContainer> GetSalesCreditQuery()
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
                                                       .Select(group => new JournalEntryContainer
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

    public class JournalEntryContainer
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
