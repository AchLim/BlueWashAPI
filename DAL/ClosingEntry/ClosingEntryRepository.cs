using EntityFramework.Exceptions.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Globalization;
using WebAPI.Data;
using WebAPI.Data.Enum;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Utility;

namespace WebAPI.DAL
{
    public sealed class ClosingEntryRepository : IClosingEntryRepository
    {
        private readonly ApplicationContext _context;

        public ClosingEntryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClosingEntry>> GetAllClosingEntries()
        {
            IEnumerable<ClosingEntry> closingEntries = Enumerable.Empty<ClosingEntry>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    closingEntries = await _context.ClosingEntries.ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data jurnal penutup.", ex);
                }
            }

            return closingEntries;
        }

        public async Task<ClosingEntry?> GetClosingEntryById(Guid id)
        {
            ClosingEntry? closingEntry = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    closingEntry = await _context.ClosingEntries
                                                            .Include(entry => entry.JournalEntries!)
                                                            .ThenInclude(je => je.JournalItems!)
                                                            .ThenInclude(ji => ji.ChartOfAccount)
                                                            .FirstOrDefaultAsync(entry => entry.Id == id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data penutup dengan id: {id}", ex);
                }
            }

            return closingEntry;
        }

        public async Task DeleteClosingEntry(ClosingEntry closingEntry)
        {

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.ClosingEntries.Remove(closingEntry);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data jurnal penutup dengan nama: {closingEntry.Name}", ex);
                }
            }
        }

        public async Task<bool> ClosingEntryExist(DateOnly dateFrom, DateOnly dateTo)
        {
            bool exist = false;

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    exist = await _context.ClosingEntries.AnyAsync(ce => ((dateFrom >= ce.StartDate) && (dateFrom <= ce.EndDate)) || ((dateTo >= ce.StartDate) && (dateTo <= ce.EndDate)));
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam mengecek data jurnal penutup pada tahun: {dateTo.Year}", ex);
                }
            }

            return exist;
        }

        public async Task<bool> UnpostedEntryExist(DateOnly dateFrom, DateOnly dateTo)
        {
            bool exist = false;

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    exist = await _context.JournalEntries.AnyAsync(je => je.Status == EntryStatus.Draft.ToString() && je.TransactionDate >= dateFrom && je.TransactionDate <= dateTo);
                    
                    if (!exist)
                        exist = await _context.PurchaseHeaders.AnyAsync(ph => ph.Status == EntryStatus.Draft.ToString() && ph.PurchaseDate >= dateFrom && ph.PurchaseDate <= dateTo);
                    
                    if (!exist)
                        exist = await _context.SalesHeaders.AnyAsync(sh => sh.Status == EntryStatus.Draft.ToString() && sh.SalesDate >= dateFrom && sh.SalesDate <= dateTo);

                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam mengecek data jurnal entry pada tahun: {dateTo.Year}", ex);
                }
            }

            return exist;
        }

        public async Task<ClosingEntry> CreateClosingEntry(DateOnly dateFrom, DateOnly dateTo)
        {
            ClosingEntry createdEntry = new ClosingEntry
            {
                StartDate = dateFrom,
                EndDate = dateTo,
                Name = $"Closing Entry for Period {dateFrom.Year}",
                Description = $"Closing Entry for Period {dateFrom.Year}",
            };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    decimal totalIncome = await _context.SalesHeaders.AsNoTracking()
                                                                      .Join(_context.SalesDetails,
                                                                                 header => header.Id,
                                                                                 detail => detail.SalesHeaderId,
                                                                                 (header, detail) => new
                                                                                 {
                                                                                     header.SalesDate,
                                                                                     Amount = detail.Quantity * detail.Price
                                                                                 })
                                                                      .Where(header => header.SalesDate >= dateFrom && header.SalesDate <= dateTo)
                                                                      .SumAsync(sd => sd.Amount);

                    decimal totalPurchaseDiscount = await _context.PurchaseHeaders.AsNoTracking().GroupJoin(_context.PurchaseDetails,
                                                                                                           header => header.Id,
                                                                                                           detail => detail.PurchaseHeaderId,
                                                                                                           (header, details) => new
                                                                                                           {
                                                                                                               header.PurchaseDate,
                                                                                                               header.Status,
                                                                                                               Details = details
                                                                                                           })
                                                                                                 .SelectMany(header => header.Details.DefaultIfEmpty(),
                                                                                                             (header, detail) => new
                                                                                                             {
                                                                                                                 header.PurchaseDate,
                                                                                                                 header.Status,
                                                                                                                 Discount = detail != null ? detail.Discount : 0m
                                                                                                             })
                                                                                                 .Where(result => result.PurchaseDate >= dateFrom && result.PurchaseDate <= dateTo && result.Status == EntryStatus.Posted.ToString())
                                                                                                 .SumAsync(detail => detail.Discount);

                    decimal totalSalesDiscount = await _context.SalesHeaders.AsNoTracking().GroupJoin(_context.SalesDetails,
                                                                                                      header => header.Id,
                                                                                                      detail => detail.SalesHeaderId,
                                                                                                      (header, details) => new
                                                                                                      {
                                                                                                          header.SalesDate,
                                                                                                          header.Status,
                                                                                                          Details = details
                                                                                                      })
                                                                                            .SelectMany(header => header.Details.DefaultIfEmpty(),
                                                                                                        (header, detail) => new
                                                                                                        {
                                                                                                            header.SalesDate,
                                                                                                            header.Status,
                                                                                                            Discount = detail != null ? detail.Discount : 0m
                                                                                                        })
                                                                                            .Where(result => result.SalesDate >= dateFrom && result.SalesDate <= dateTo && result.Status == EntryStatus.Posted.ToString())
                                                                                            .SumAsync(detail => detail.Discount);

                    ChartOfAccount? retainedEarningsCoa = await _context.ChartOfAccounts.AsNoTracking().FirstOrDefaultAsync(coa => coa.AccountNo == 310);
                    ChartOfAccount? incomeSummaryCoa = await _context.ChartOfAccounts.AsNoTracking().FirstOrDefaultAsync(coa => coa.AccountNo == 320);
                    ChartOfAccount? salesIncomeCoa = await _context.ChartOfAccounts.AsNoTracking().FirstOrDefaultAsync(coa => coa.AccountNo == 401);
                    ChartOfAccount? salesDiscountCoa = await _context.ChartOfAccounts.AsNoTracking().FirstOrDefaultAsync(coa => coa.AccountNo == 420);
                    ChartOfAccount? purchaseDiscountCoa = await _context.ChartOfAccounts.AsNoTracking().FirstOrDefaultAsync(coa => coa.AccountNo == 502);

                    if (retainedEarningsCoa == null)
                        throw new DatabaseReadException("Kode Akun - Laba Ditahan 310 tidak ditemukan!");

                    if (incomeSummaryCoa == null)
                        throw new DatabaseReadException("Kode Akun - Ikhtisar Laba-Rugi 320 tidak ditemukan!");

                    if (salesIncomeCoa == null)
                        throw new DatabaseReadException("Kode Akun - Pendapatan Penjualan 401 tidak ditemukan!");

                    if (salesDiscountCoa == null)
                        throw new DatabaseReadException("Kode Akun - Potongan Penjualan 420 tidak ditemukan!");

                    if (purchaseDiscountCoa == null)
                        throw new DatabaseReadException("Kode Akun - Potongan Pembelian 502 tidak ditemukan!");

                    DateOnly dateToday = DateOnly.FromDateTime(DateTime.Now);

                    decimal remainingIncomeSummary = totalIncome;

                    JournalEntry incomeToSummary = new JournalEntry
                    {
                        TransactionNo = await GetNextSequenceTransactionNo(dateToday.ToString("yyyy-MM-dd")),
                        TransactionDate = dateToday,
                        Status = EntryStatus.Posted.ToString(),
                        JournalItems = new List<JournalItem>(2)
                        {
                            new JournalItem { ChartOfAccountId = salesIncomeCoa.Id, Debit = remainingIncomeSummary, Credit = 0 },
                            new JournalItem { ChartOfAccountId = incomeSummaryCoa.Id, Debit = 0, Credit = remainingIncomeSummary },
                        },
                        Description = "Tutup Saldo Pendapatan"
                    };

                    JournalEntry summaryToPurchaseDiscount = new JournalEntry
                    {
                        TransactionNo = await GetNextSequenceTransactionNo(dateToday.ToString("yyyy-MM-dd")),
                        TransactionDate = dateToday,
                        Status = EntryStatus.Posted.ToString(),
                        JournalItems = new List<JournalItem>(2)
                        {
                            new JournalItem { ChartOfAccountId = purchaseDiscountCoa.Id, Debit = totalPurchaseDiscount, Credit = 0 },
                            new JournalItem { ChartOfAccountId = incomeSummaryCoa.Id, Debit = 0, Credit = totalPurchaseDiscount },
                        },
                        Description = "Tutup Akun Diskon Pembelian"
                    };

                    JournalEntry summaryToSalesDiscount = new JournalEntry
                    {
                        TransactionNo = await GetNextSequenceTransactionNo(dateToday.ToString("yyyy-MM-dd")),
                        TransactionDate = dateToday,
                        Status = EntryStatus.Posted.ToString(),
                        JournalItems = new List<JournalItem>(2)
                        {
                            new JournalItem { ChartOfAccountId = incomeSummaryCoa.Id, Debit = totalSalesDiscount, Credit = 0 },
                            new JournalItem { ChartOfAccountId = salesDiscountCoa.Id, Debit = 0, Credit = totalSalesDiscount },
                        },
                        Description = "Tutup Akun Diskon Penjualan"
                    };

                    // Debitkan akun diskon pembelian = Creditkan akun income summary = bertambah
                    remainingIncomeSummary += totalPurchaseDiscount;

                    // Creditkan akun diskon penjualan = Debitkan akun income summary = berkurang
                    remainingIncomeSummary -= totalSalesDiscount;

                    List<ExpenseContainer> expenseAccounts = await _context.JournalEntries.Join(_context.JournalItems,
                                                                                je => je.Id,
                                                                                ji => ji.JournalEntryId,
                                                                                (entry, item) => new
                                                                                {
                                                                                    entry.TransactionDate,
                                                                                    entry.Status,
                                                                                    item.ChartOfAccountId,
                                                                                    item.Debit,
                                                                                    item.Credit
                                                                                })
                                                                        .Join(_context.ChartOfAccounts,
                                                                                entry => entry.ChartOfAccountId,
                                                                                coa => coa.Id,
                                                                                (entry, coa) => new
                                                                                {
                                                                                    entry.TransactionDate,
                                                                                    entry.Status,
                                                                                    ChartOfAccountId = coa.Id,
                                                                                    coa.AccountHeaderNo,
                                                                                    coa.AccountNo,
                                                                                    coa.AccountName,

                                                                                    entry.Debit,
                                                                                    entry.Credit
                                                                                })
                                                                        .Where(result => result.Status == EntryStatus.Posted.ToString() &&
                                                                                         result.TransactionDate >= dateFrom &&
                                                                                         result.TransactionDate <= dateTo &&
                                                                                         result.AccountHeaderNo == 600)
                                                                        .GroupBy(result => new
                                                                        {
                                                                            result.ChartOfAccountId,
                                                                            result.AccountNo,
                                                                            result.AccountName,
                                                                        })
                                                                        .Where(group => group.Sum(item => item.Credit - item.Debit) != 0m)
                                                                        .Select(group => new ExpenseContainer
                                                                        {
                                                                            ChartOfAccountId = group.Key.ChartOfAccountId,
                                                                            AccountNo = group.Key.AccountNo,
                                                                            AccountName = group.Key.AccountName,
                                                                            Balance = group.Sum(item => item.Credit - item.Debit)
                                                                        }).ToListAsync();

                    createdEntry.JournalEntries = new List<JournalEntry>(3 + expenseAccounts.Count + 1)
                    {
                        incomeToSummary,
                        summaryToPurchaseDiscount,
                        summaryToSalesDiscount
                    };

                    // Tutup seluruh akun beban (Ikhitsar Laba-Rugi pada Beban)
                    foreach (var expenseAcc in expenseAccounts)
                    {
                        decimal tempBalance = Math.Abs(expenseAcc.Balance);
                        remainingIncomeSummary -= tempBalance;

                        createdEntry.JournalEntries.Add(new JournalEntry
                        {
                            TransactionNo = await GetNextSequenceTransactionNo(dateToday.ToString("yyyy-MM-dd")),
                            TransactionDate = dateToday,
                            Status = EntryStatus.Posted.ToString(),
                            JournalItems = new List<JournalItem>(2)
                            {
                                new JournalItem { ChartOfAccountId = incomeSummaryCoa.Id, Debit = tempBalance, Credit = 0 },
                                new JournalItem { ChartOfAccountId = expenseAcc.ChartOfAccountId, Debit = 0, Credit = tempBalance },
                            },
                            Description = $"Tutup Akun {expenseAcc.AccountName}"
                        });
                    }

                    JournalEntry? closingIncomeSummaryEntry = null;

                    // Jika laba: Modal pada Ikhitisar Laba-Rugi
                    if (remainingIncomeSummary > 0)
                    {
                        closingIncomeSummaryEntry = new JournalEntry
                        {
                            TransactionNo = await GetNextSequenceTransactionNo(dateToday.ToString("yyyy-MM-dd")),
                            TransactionDate = dateToday,
                            Status = EntryStatus.Posted.ToString(),
                            JournalItems = new List<JournalItem>(2)
                            {
                                new JournalItem { ChartOfAccountId = retainedEarningsCoa.Id, Debit = remainingIncomeSummary, Credit = 0 },
                                new JournalItem { ChartOfAccountId = incomeSummaryCoa.Id, Debit = 0, Credit = remainingIncomeSummary },
                            },
                            Description = "Mengalami Laba, Tutup Akun Ikhtisar Laba-Rugi"
                        };
                    }
                    // Jika rugi: Ikhitisar Laba-Rugi pada Modal
                    else if (remainingIncomeSummary < 0)
                    {
                        closingIncomeSummaryEntry = new JournalEntry
                        {
                            TransactionNo = await GetNextSequenceTransactionNo(dateToday.ToString("yyyy-MM-dd")),
                            TransactionDate = dateToday,
                            Status = EntryStatus.Posted.ToString(),
                            JournalItems = new List<JournalItem>(2)
                            {
                                new JournalItem { ChartOfAccountId = incomeSummaryCoa.Id, Debit = remainingIncomeSummary, Credit = 0 },
                                new JournalItem { ChartOfAccountId = retainedEarningsCoa.Id, Debit = 0, Credit = remainingIncomeSummary },
                            },
                            Description = "Mengalami Rugi, Tutup Akun Ikhtisar Laba-Rugi"
                        };
                    }

                    if (closingIncomeSummaryEntry is not null)
                    {
                        createdEntry.JournalEntries.Add(closingIncomeSummaryEntry);
                    }

                    await _context.ClosingEntries.AddAsync(createdEntry);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam mengecek data jurnal penutup pada tahun: {dateTo.Year}", ex);
                }
            }

            return createdEntry;
        }

        public async Task<string> GetNextSequenceTransactionNo(string entryDate)
        {
            var result = (await _context.Database.SqlQuery<string>($"EXECUTE GetJournalEntryNo {entryDate};").ToListAsync()).FirstOrDefault();
            return result!;
        }
    }

    public class ExpenseContainer
    {
        public Guid ChartOfAccountId { get; set; }
        public int AccountNo { get; set; }
        public string AccountName { get; set; } = default!;
        public decimal Balance { get; set; }
    }
}
