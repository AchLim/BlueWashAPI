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
    public sealed class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly ApplicationContext _context;

        public JournalEntryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<string> GetNextSequenceTransactionNo(string entryDate)
        {
            var result = (await _context.Database.SqlQuery<string>($"EXECUTE GetJournalEntryNo {entryDate};").ToListAsync()).FirstOrDefault();
            return result!;
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
                                                            //.ThenInclude(items => items.ChartOfAccount)
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
            if (journalEntry.JournalItems.IsNotEmpty())
            {
                var result = journalEntry.JournalItems!.Aggregate(
                    new { Debit = 0m, Credit = 0m }, (acc, item) => new
                    {
                        Debit = acc.Debit + item.Debit,
                        Credit = acc.Credit + item.Credit
                    });
                decimal debit = result.Debit;
                decimal credit = result.Credit;

                var debitCurrency = string.Format(new CultureInfo("id-ID"), "{0:C}", debit);
                var creditCurrency = string.Format(new CultureInfo("id-ID"), "{0:C}", credit);

                if (Decimal.Compare(debit, credit) != 0)
                {
                    throw new DatabaseUpdateException(@$"
                        Entri {journalEntry.TransactionNo} tidak balance.
                        Total debit: {debitCurrency} dan total kredit: {creditCurrency}.
                    ");
                }
            }

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    journalEntry.TransactionNo = "/";
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
            if (journalEntry.JournalItems.IsNotEmpty())
            {
                var result = journalEntry.JournalItems!.Aggregate(
                    new { Debit = 0m, Credit = 0m }, (acc, item) => new
                    {
                        Debit = acc.Debit + item.Debit,
                        Credit = acc.Credit + item.Credit
                    });
                decimal debit = result.Debit;
                decimal credit = result.Credit;

                var debitCurrency = string.Format(new CultureInfo("id-ID"), "{0:C}", debit);
                var creditCurrency = string.Format(new CultureInfo("id-ID"), "{0:C}", credit);

                if (Decimal.Compare(debit, credit) != 0)
                {
                    throw new DatabaseUpdateException(@$"
                        Entri {journalEntry.TransactionNo} tidak balance.
                        Total debit: {debitCurrency} dan total kredit: {creditCurrency}.
                    ");
                }
            }

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (journalEntry.TransactionNo == "/" && journalEntry.Status == EntryStatus.Posted.ToString())
                    {
                        var newTransactionNo = await GetNextSequenceTransactionNo(journalEntry.TransactionDate.ToString("yyyy-MM-dd"));
                        bool exist = await _context.JournalEntries.AnyAsync(je => je.TransactionNo == newTransactionNo);
                        if (exist)
                            throw new UniqueConstraintException();

                        journalEntry.TransactionNo = newTransactionNo;
                    }

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

        public async Task<bool> ClosingEntryExist(DateOnly transactionDate)
        {
            bool exist = false;

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    exist = await _context.ClosingEntries.AnyAsync(ce => transactionDate >= ce.StartDate && transactionDate <= ce.EndDate);
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam pengecekan data jurnal penutup pada periode {transactionDate.Year}", ex);
                }
            }

            return exist;
        }

        //public async Task<IEnumerable<JournalEntryContainer>> GetJournalEntryData()
        //{
        //    IEnumerable<JournalEntryContainer> generalJournalData = Enumerable.Empty<JournalEntryContainer>();
        //    await using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            // EF bug. Need to convert to AsEnumerable for first query.
        //            generalJournalData = (await GetJournalEntryQuery().ToListAsync())
        //                                .Concat(GetPurchaseDebitQuery())
        //                                .Concat(GetPurchaseCreditQuery())
        //                                .Concat(GetSalesDebitQuery())
        //                                .Concat(GetSalesCreditQuery())
        //                                .OrderBy(journal => journal.TransactionNo);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data jurnal", ex);
        //        }
        //    }

        //    return generalJournalData;
        //}

        //public async Task<IEnumerable<JournalEntryContainer>> GetPurchaseQuery()
        //{
        //    IEnumerable<JournalEntryContainer> purchaseData = Enumerable.Empty<JournalEntryContainer>();
        //    await using (var transaction = await _context.Database.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            purchaseData = (await GetPurchaseDebitQuery().ToListAsync()).Concat(GetPurchaseCreditQuery());
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan laporan rincian pembelian", ex);
        //        }
        //    }

        //    return purchaseData;
        //}

        public async Task<IEnumerable<PurchaseEntryContainer>> GetPurchaseQueryById(Guid purchaseHeaderId)
        {
            List<PurchaseEntryContainer> purchaseData = new();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            -- Purchase Query
                            SELECT
	                            q.AccountNo,
	                            q.AccountName,
	                            q.Id,
	                            q.PurchaseNo AS TransactionNo,
	                            q.PurchaseDate AS TransactionDate,
	                            q.Description,
	                            q.Reference,
	                            SUM(COALESCE(q.Debit, 0)) AS Debit,
	                            SUM(COALESCE(q.Credit, 0)) AS Credit,
	                            SUM(COALESCE(q.Debit, 0) - COALESCE(q.Credit, 0)) AS Balance
                            FROM (

		                            -- Debit - Supplies
		                            SELECT
			                            coa.AccountNo,
			                            coa.AccountName,
			                            ph.Id,
			                            ph.PurchaseNo,
			                            ph.PurchaseDate,
			                            ph.Description,
			                            CONCAT(s.SupplierCode, ' ', s.SupplierName) AS Reference,
			                            (pd.Quantity * pd.Price) AS Debit,
			                            0 AS Credit
		                            FROM
			                            purchase_header ph
				                            JOIN supplier s ON s.Id = ph.SupplierId
				                            LEFT JOIN purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
				                            JOIN chart_of_account coa ON coa.AccountNo = 113
		                            GROUP BY
			                            ph.Id,
			                            ph.PurchaseNo,
			                            ph.PurchaseDate,
			                            ph.Description,
			                            s.SupplierCode,
			                            s.SupplierName,
			                            coa.AccountNo,
			                            coa.AccountName,
			                            pd.Quantity,
			                            pd.Price

		                            UNION ALL

		                            -- Credit - COGS Discount
		                            SELECT
			                            coa.AccountNo,
			                            coa.AccountName,
			                            ph.Id,
			                            ph.PurchaseNo,
			                            ph.PurchaseDate,
			                            ph.Description,
			                            CONCAT(s.SupplierCode, ' ', s.SupplierName) AS Reference,
			                            0 AS Debit,
			                            pd.Discount AS Credit
		                            FROM
			                            purchase_header ph
				                            JOIN supplier s ON s.Id = ph.SupplierId
				                            LEFT JOIN purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
				                            JOIN chart_of_account coa ON coa.AccountNo = 502
		                            GROUP BY
			                            ph.Id,
			                            ph.PurchaseNo,
			                            ph.PurchaseDate,
			                            ph.Description,
			                            s.SupplierCode,
			                            s.SupplierName,
			                            coa.AccountNo,
			                            coa.AccountName,
			                            pd.Quantity,
			                            pd.Price,
			                            pd.Discount

		                            UNION ALL

		                            -- Credit - Account Payable
		                            SELECT
			                            coa.AccountNo,
			                            coa.AccountName,
			                            ph.Id,
			                            ph.PurchaseNo,
			                            ph.PurchaseDate,
			                            ph.Description,
			                            CONCAT(s.SupplierCode, ' ', s.SupplierName) AS Reference,
			                            0 AS Debit,
			                            (pd.Quantity * pd.Price) - pd.Discount AS Credit
		                            FROM
			                            purchase_header ph
				                            JOIN supplier s ON s.Id = ph.SupplierId
				                            LEFT JOIN purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
				                            JOIN chart_of_account coa ON coa.AccountNo = 201
		                            GROUP BY
			                            ph.Id,
			                            ph.PurchaseNo,
			                            ph.PurchaseDate,
			                            ph.Description,
			                            s.SupplierCode,
			                            s.SupplierName,
			                            coa.AccountNo,
			                            coa.AccountName,
			                            pd.Quantity,
			                            pd.Price,
			                            pd.Discount

		                            UNION ALL

		                            -- Debit of Account Payable
		                            SELECT
			                            coa.AccountNo,
			                            coa.AccountName,
			                            ph.Id,
			                            ph.PurchaseNo,
			                            pp.PaymentDate AS [PurchaseDate],
			                            ph.Description,
			                            CONCAT('Pembayaran: ', s.SupplierCode, ' ', s.SupplierName) AS Reference,
			                            pp.Amount AS Debit,
			                            0 AS Credit
		                            FROM
			                            purchase_header ph
				                            JOIN supplier s ON s.Id = ph.SupplierId
				                            JOIN purchase_payment pp ON ph.Id = pp.PurchaseHeaderId
				                            JOIN chart_of_account coa ON coa.AccountNo = 201
		                            GROUP BY
			                            ph.Id,
			                            ph.PurchaseNo,
			                            pp.PaymentDate,
			                            ph.Description,
			                            s.SupplierCode,
			                            s.SupplierName,
			                            coa.AccountNo,
			                            coa.AccountName,
			                            pp.Type,
			                            pp.Amount

		                            UNION ALL

		                            -- Credit of Cash / Bank
		                            SELECT
			                            coa.AccountNo,
			                            coa.AccountName,
			                            ph.Id,
			                            ph.PurchaseNo,
			                            pp.PaymentDate AS [PurchaseDate],
			                            ph.Description,
			                            CONCAT('Pembayaran: ', s.SupplierCode, ' ', s.SupplierName) AS Reference,
			                            0 AS Debit,
			                            pp.Amount AS Credit
		                            FROM
			                            purchase_header ph
				                            JOIN supplier s ON s.Id = ph.SupplierId
				                            JOIN purchase_payment pp ON ph.Id = pp.PurchaseHeaderId
				                            JOIN chart_of_account coa ON coa.AccountNo = CASE pp.Type
					                            WHEN 'Cash' THEN 111
					                            WHEN 'Bank' THEN 112
					                            ELSE 111
				                            END
		                            GROUP BY
			                            ph.Id,
			                            ph.PurchaseNo,
			                            pp.PaymentDate,
			                            ph.Description,
			                            s.SupplierCode,
			                            s.SupplierName,
			                            coa.AccountNo,
			                            coa.AccountName,
			                            pp.Type,
			                            pp.Amount

	                            ) q
                            WHERE q.Id = @PurchaseHeaderId
                            GROUP BY
	                            q.Id,
	                            q.PurchaseNo,
	                            q.PurchaseDate,
	                            q.AccountNo,
	                            q.AccountName,
	                            q.Description,
	                            q.Reference
                            HAVING
	                            SUM(q.Debit) <> 0 OR SUM(q.Credit) <> 0
                            ORDER BY
	                            q.Reference DESC
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@PurchaseHeaderId", purchaseHeaderId));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Access the values in each row using the reader
                                var accountNo = reader["AccountNo"];
                                var accountName = reader["AccountName"];
                                var transactionNo = reader["TransactionNo"];
                                var transactionDate = reader["TransactionDate"];
                                var description = reader["Description"];
                                var reference = reader["Reference"];
                                var debit = reader["Debit"];
                                var credit = reader["Credit"];
                                var balance = reader["Balance"];

                                // Process the values as needed, e.g., create objects, perform calculations, etc.
                                // Example: Create a PurchaseEntry object
                                var purchaseEntry = new PurchaseEntryContainer
                                {
                                    AccountNo = Convert.ToInt32(reader["AccountNo"]),
                                    AccountName = Convert.ToString(reader["AccountName"])!,
                                    Id = Guid.Parse(reader["Id"].ToString()!),
                                    TransactionNo = Convert.ToString(reader["TransactionNo"])!,
                                    TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    Reference = Convert.ToString(reader["Reference"]),
                                    Debit = Convert.ToDecimal(reader["Debit"]),
                                    Credit = Convert.ToDecimal(reader["Credit"]),
                                    Balance = Convert.ToDecimal(reader["Balance"])
                                };

                                purchaseData.Add(purchaseEntry);
                            }
                        }
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan laporan rincian pembelian", ex);
                }
            }

            return purchaseData;
        }


        public async Task<IEnumerable<SalesEntryContainer>> GetSalesQueryById(Guid salesHeaderId)
        {
            List<SalesEntryContainer> salesData = new();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            SELECT
                                q.AccountNo,
                                q.AccountName,
                                q.Id,
                                q.SalesNo AS TransactionNo,
                                q.SalesDate AS TransactionDate,
                                q.Description,
                                q.Reference,
                                SUM(COALESCE(q.Debit, 0)) AS Debit,
                                SUM(COALESCE(q.Credit, 0)) AS Credit,
                                SUM(COALESCE(q.Debit, 0) - COALESCE(q.Credit, 0)) AS Balance
                            FROM (

                                    -- Debit of Account Receivable
                                    SELECT
                                        coa.AccountNo,
                                        coa.AccountName,
                                        sh.Id,
                                        sh.SalesNo,
                                        sh.SalesDate,
                                        sh.Description,
                                        CONCAT(c.CustomerCode, ' ', c.CustomerName) AS Reference,
                                        (sd.Quantity * sd.Price) - sd.Discount AS Debit,
                                        0 AS Credit
                                    FROM
                                        sales_header sh
                                            JOIN customer c ON c.Id = sh.CustomerId
                                            LEFT JOIN sales_detail sd ON sh.Id = sd.SalesHeaderId
                                            JOIN chart_of_account coa ON coa.AccountNo = 116
                                    GROUP BY
                                        sh.Id,
                                        sh.SalesNo,
                                        sh.SalesDate,
                                        sh.Description,
                                        c.CustomerCode,
                                        c.CustomerName,
                                        coa.AccountNo,
                                        coa.AccountName,
                                        sd.Quantity,
                                        sd.Price,
			                            sd.Discount

                                    UNION ALL

                                    -- Debit of Discount
                                    SELECT
                                        coa.AccountNo,
                                        coa.AccountName,
                                        sh.Id,
                                        sh.SalesNo,
                                        sh.SalesDate,
                                        sh.Description,
                                        CONCAT(c.CustomerCode, ' ', c.CustomerName) AS Reference,
                                        sd.Discount AS Debit,
			                            0 AS Credit
                                    FROM
                                        sales_header sh
                                            JOIN customer c ON c.Id = sh.CustomerId
                                            LEFT JOIN sales_detail sd ON sh.Id = sd.SalesHeaderId
                                            JOIN chart_of_account coa ON coa.AccountNo = 420
                                    GROUP BY
                                        sh.Id,
                                        sh.SalesNo,
                                        sh.SalesDate,
                                        sh.Description,
                                        c.customerCode,
                                        c.customerName,
                                        coa.AccountNo,
                                        coa.AccountName,
			                            sd.Discount

		                            UNION ALL

                                    -- Credit of Revenue
                                    SELECT
                                        coa.AccountNo,
                                        coa.AccountName,
                                        sh.Id,
                                        sh.SalesNo,
                                        sh.SalesDate,
                                        sh.Description,
                                        CONCAT(c.CustomerCode, ' ', c.CustomerName) AS Reference,
                                        0 AS Debit,
                                        sd.Quantity * sd.Price AS Credit
                                    FROM
                                        sales_header sh
                                            JOIN customer c ON c.Id = sh.CustomerId
                                            LEFT JOIN sales_detail sd ON sh.Id = sd.SalesHeaderId
                                            JOIN chart_of_account coa ON coa.AccountNo = 401
                                    GROUP BY
                                        sh.Id,
                                        sh.SalesNo,
                                        sh.SalesDate,
                                        sh.Description,
                                        c.customerCode,
                                        c.customerName,
                                        coa.AccountNo,
                                        coa.AccountName,
                                        sd.Quantity,
                                        sd.Price

                                    UNION ALL

                                    -- Credit of Account Payable
                                    SELECT
                                        coa.AccountNo,
                                        coa.AccountName,
                                        sh.Id,
                                        sh.SalesNo,
                                        sp.PaymentDate AS [SalesDate],
                                        sh.Description,
                                        CONCAT('Penjualan: ', c.CustomerCode, ' ', c.CustomerName) AS Reference,
                                        0 AS Debit,
                                        sp.Amount AS Credit
                                    FROM
                                        sales_header sh
                                            JOIN customer c ON c.Id = sh.CustomerId
                                            JOIN sales_payment sp ON sh.Id = sp.SalesHeaderId
                                            JOIN chart_of_account coa ON coa.AccountNo = 116
                                    GROUP BY
                                        sh.Id,
                                        sh.SalesNo,
                                        sp.PaymentDate,
                                        sh.Description,
                                        c.customerCode,
                                        c.customerName,
                                        coa.AccountNo,
                                        coa.AccountName,
                                        sp.Type,
                                        sp.Amount

                                    UNION ALL

                                    -- Debit of Cash / Bank
                                    SELECT
                                        coa.AccountNo,
                                        coa.AccountName,
                                        sh.Id,
                                        sh.SalesNo,
                                        sp.PaymentDate AS [SalesDate],
                                        sh.Description,
                                        CONCAT('Penjualan: ', c.CustomerCode, ' ', c.CustomerName) AS Reference,
                                        sp.Amount AS Debit,
                                        0 AS Credit
                                    FROM
                                        sales_header sh
                                            JOIN customer c ON c.Id = sh.CustomerId
                                            JOIN sales_payment sp ON sh.Id = sp.SalesHeaderId
                                            JOIN chart_of_account coa ON coa.AccountNo = CASE sp.Type
                                                WHEN 'Cash' THEN 111
                                                WHEN 'Bank' THEN 112
                                                ELSE 111
                                            END
                                    GROUP BY
                                        sh.Id,
                                        sh.SalesNo,
                                        sp.PaymentDate,
                                        sh.Description,
                                        c.customerCode,
                                        c.customerName,
                                        coa.AccountNo,
                                        coa.AccountName,
                                        sp.Type,
                                        sp.Amount

                                ) q
                            WHERE q.Id = @SalesHeaderId
                            GROUP BY
                                q.Id,
                                q.SalesNo,
                                q.SalesDate,
                                q.AccountNo,
                                q.AccountName,
                                q.Description,
                                q.Reference
                            HAVING
	                            SUM(q.Debit) <> 0 OR SUM(q.Credit) <> 0
                            ORDER BY
                                q.Reference DESC
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@SalesHeaderId", salesHeaderId));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Access the values in each row using the reader
                                var accountNo = reader["AccountNo"];
                                var accountName = reader["AccountName"];
                                var transactionNo = reader["TransactionNo"];
                                var transactionDate = reader["TransactionDate"];
                                var description = reader["Description"];
                                var reference = reader["Reference"];
                                var debit = reader["Debit"];
                                var credit = reader["Credit"];
                                var balance = reader["Balance"];

                                // Process the values as needed, e.g., create objects, perform calculations, etc.
                                // Example: Create a PurchaseEntry object
                                var salesEntry = new SalesEntryContainer
                                {
                                    AccountNo = Convert.ToInt32(reader["AccountNo"]),
                                    AccountName = Convert.ToString(reader["AccountName"])!,
                                    Id = Guid.Parse(reader["Id"].ToString()!),
                                    TransactionNo = Convert.ToString(reader["TransactionNo"])!,
                                    TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    Reference = Convert.ToString(reader["Reference"]),
                                    Debit = Convert.ToDecimal(reader["Debit"]),
                                    Credit = Convert.ToDecimal(reader["Credit"]),
                                    Balance = Convert.ToDecimal(reader["Balance"])
                                };

                                salesData.Add(salesEntry);
                            }
                        }
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan laporan rincian penjualan", ex);
                }
            }

            return salesData;
        }
        //private IQueryable<JournalEntryContainer> GetJournalEntryQuery()
        //{
        //    return _context.JournalEntries.GroupJoin(_context.JournalItems.Join(_context.ChartOfAccounts,
        //                                                                        item => item.ChartOfAccountId,
        //                                                                        coa => coa.Id,
        //                                                                        (item, coa) => new
        //                                                                        {
        //                                                                            JournalEntryId = item.JournalEntryId,
        //                                                                            AccountNo = coa.AccountNo,
        //                                                                            Debit = item.Debit,
        //                                                                            Credit = item.Credit,
        //                                                                        }),
        //                                                                      header => header.Id,
        //                                                                      item => item.JournalEntryId,
        //                                                                      (header, details) => new
        //                                                                      {
        //                                                                          TransactionNo = header.TransactionNo,
        //                                                                          TransactionDate = header.TransactionDate,
        //                                                                          TransactionDetails = details,
        //                                                                          Description = header.Description
        //                                                                      })
        //                                                            .SelectMany(header => header.TransactionDetails.DefaultIfEmpty(),
        //                                                                        (header, detail) => new
        //                                                                        {
        //                                                                            TransactionNo = header.TransactionNo,
        //                                                                            TransactionDate = header.TransactionDate,
        //                                                                            Description = header.Description,
        //                                                                            AccountNo = detail != null ? (int?)detail.AccountNo : null,
        //                                                                            Debit = detail != null ? (decimal?)detail.Debit : null,
        //                                                                            Credit = detail != null ? (decimal?)detail.Credit : null,
        //                                                                        })
        //                                                            .GroupBy(result => new
        //                                                            {
        //                                                                result.TransactionNo,
        //                                                                result.TransactionDate,
        //                                                                result.AccountNo,
        //                                                                result.Description
        //                                                            })
        //                                                            .Select(group => new JournalEntryContainer
        //                                                            {
        //                                                                TransactionNo = group.Key.TransactionNo,
        //                                                                TransactionDate = group.Key.TransactionDate,
        //                                                                Description = group.Key.Description,
        //                                                                AccountNo = group.Key.AccountNo,
        //                                                                Debit = group.Sum(item => item.Debit) ?? 0m,
        //                                                                Credit = group.Sum(item => item.Credit) ?? 0m
        //                                                            });
        //}

        //private IQueryable<JournalEntryContainer> GetPurchaseDebitQuery()
        //{
        //    return _context.PurchaseHeaders.GroupJoin(_context.PurchaseDetails,
        //                                                                      header => header.Id,
        //                                                                      detail => detail.PurchaseHeaderId,
        //                                                                      (header, details) => new
        //                                                                      {
        //                                                                          TransactionNo = header.TransactionNo,
        //                                                                          TransactionDate = header.PurchaseDate,
        //                                                                          TransactionDetails = details,
        //                                                                          Description = header.Description
        //                                                                      })
        //                                                            .SelectMany(header => header.TransactionDetails.DefaultIfEmpty(),
        //                                                                        (header, detail) => new
        //                                                                        {
        //                                                                            TransactionNo = header.TransactionNo,
        //                                                                            TransactionDate = header.TransactionDate,
        //                                                                            Description = header.Description,
        //                                                                            AccountNo = detail != null ? (int?)113 : null,
        //                                                                            Debit = detail != null ? (decimal?)(detail.Price * detail.Quantity) : null,
        //                                                                            Credit = detail != null ? (decimal?)0m : null,
        //                                                                        })
        //                                                            .GroupBy(result => new
        //                                                            {
        //                                                                result.TransactionNo,
        //                                                                result.TransactionDate,
        //                                                                result.AccountNo,
        //                                                                result.Description,
        //                                                            })
        //                                                            .Select(group => new JournalEntryContainer
        //                                                            {
        //                                                                TransactionNo = group.Key.TransactionNo,
        //                                                                TransactionDate = group.Key.TransactionDate,
        //                                                                Description = group.Key.Description,
        //                                                                AccountNo = group.Key.AccountNo,
        //                                                                Debit = group.Sum(item => item.Debit) ?? 0m,
        //                                                                Credit = 0m
        //                                                            });
        //}

        //private IQueryable<JournalEntryContainer> GetPurchaseCreditQuery()
        //{
        //    return _context.PurchaseHeaders.GroupJoin(_context.PurchaseDetails,
        //                                                                      header => header.Id,
        //                                                                      detail => detail.PurchaseHeaderId,
        //                                                                      (header, details) => new
        //                                                                      {
        //                                                                          TransactionNo = header.TransactionNo,
        //                                                                          TransactionDate = header.PurchaseDate,
        //                                                                          TransactionDetails = details,
        //                                                                          Description = header.Description
        //                                                                      })
        //                                                            .SelectMany(header => header.TransactionDetails.DefaultIfEmpty(),
        //                                                                        (header, detail) => new
        //                                                                        {
        //                                                                            TransactionNo = header.TransactionNo,
        //                                                                            TransactionDate = header.TransactionDate,
        //                                                                            Description = header.Description,
        //                                                                            AccountNo = detail != null ? (int?)111 : null,
        //                                                                            Debit = detail != null ? (decimal?)0m : null,
        //                                                                            Credit = detail != null ? (decimal?)(detail.Price * detail.Quantity) : null,
        //                                                                        })
        //                                                            .GroupBy(result => new
        //                                                            {
        //                                                                result.TransactionNo,
        //                                                                result.TransactionDate,
        //                                                                result.AccountNo,
        //                                                                result.Description,
        //                                                            })
        //                                                            .Select(group => new JournalEntryContainer
        //                                                            {
        //                                                                TransactionNo = group.Key.TransactionNo,
        //                                                                TransactionDate = group.Key.TransactionDate,
        //                                                                Description = group.Key.Description,
        //                                                                AccountNo = group.Key.AccountNo,
        //                                                                Debit = 0m,
        //                                                                Credit = group.Sum(item => item.Credit) ?? 0m,
        //                                                            });
        //}

        //private IQueryable<PurchaseEntryContainer> GetPurchaseDebitQueryById(Guid purchaseHeaderId)
        //{
        //    return _context.PurchaseHeaders.Where(header => header.Id == purchaseHeaderId)
        //                                                            .Join(_context.ChartOfAccounts,
        //                                                                  header => 113,
        //                                                                  coa => coa.AccountNo,
        //                                                                  (header, coa) => new {
        //                                                                      header.Id,
        //                                                                      header.TransactionNo,
        //                                                                      header.PurchaseDate,
        //                                                                      header.Description,
        //                                                                      AccountNo = coa.AccountNo,
        //                                                                      AccountName = coa.AccountName
        //                                                                  })
        //                                                            .Join(_context.PurchaseDetails,
        //                                                                      header => header.Id,
        //                                                                      detail => detail.PurchaseHeaderId,
        //                                                                      (header, detail) => new
        //                                                                      {
        //                                                                          TransactionNo = header.TransactionNo,
        //                                                                          TransactionDate = header.PurchaseDate,
        //                                                                          AccountNo = header.AccountNo,
        //                                                                          AccountName = header.AccountName,
        //                                                                          Description = header.Description,
        //                                                                          Debit = detail.Price * detail.Quantity,
        //                                                                          Credit = 0m,
        //                                                                      })
        //                                                            .GroupBy(result => new
        //                                                            {
        //                                                                result.TransactionNo,
        //                                                                result.TransactionDate,
        //                                                                result.AccountNo,
        //                                                                result.AccountName,
        //                                                                result.Description,
        //                                                            })
        //                                                            .Select(group => new PurchaseEntryContainer
        //                                                            {
        //                                                                TransactionNo = group.Key.TransactionNo,
        //                                                                PurchaseDate = group.Key.TransactionDate,
        //                                                                Description = group.Key.Description,
        //                                                                AccountNo = group.Key.AccountNo,
        //                                                                AccountName = group.Key.AccountName,
        //                                                                Debit = group.Sum(item => item.Debit),
        //                                                                Credit = 0m
        //                                                            });
        //}

        //private IQueryable<PurchaseEntryContainer> GetPurchaseCreditQueryById(Guid purchaseHeaderId, bool hasPayment = false)
        //{
        //    if (hasPayment)
        //    {
        //        return _context.PurchaseHeaders.Where(header => header.Id == purchaseHeaderId)
        //                                       .Join(_context.PurchasePayments.Join(_context.ChartOfAccounts,
        //                                                                                 payment => payment.Type == PaymentType.Cash.ToString() ? 111 : 112,
        //                                                                                 coa => coa.AccountNo,
        //                                                                                 (payment, coa) => new
        //                                                                                 {
        //                                                                                     payment.PurchaseHeaderId,
        //                                                                                     payment.PaymentDate,
        //                                                                                     payment.Amount,
        //                                                                                     coa.AccountNo,
        //                                                                                     coa.AccountName,
        //                                                                                 }),
        //                                                  header => header.Id,
        //                                                  payment => payment.PurchaseHeaderId,
        //                                                  (header, payment) => new
        //                                                  {
        //                                                      header.TransactionNo,
        //                                                      header.Description,
        //                                                      payment.PaymentDate,
        //                                                      payment.AccountNo,
        //                                                      payment.AccountName,
        //                                                      payment.Amount,
        //                                                  })
        //                                       .GroupBy(header => new
        //                                       {
        //                                           header.TransactionNo,
        //                                           header.PaymentDate,
        //                                           header.AccountNo,
        //                                           header.AccountName,
        //                                           header.Description,
        //                                       })
        //                                       .OrderBy(group => group.Key.PaymentDate)
        //                                       .Select(group => new PurchaseEntryContainer
        //                                       {
        //                                            TransactionNo = group.Key.TransactionNo,
        //                                            PurchaseDate = group.Key.PaymentDate,
        //                                            AccountNo = group.Key.AccountNo,
        //                                            AccountName = group.Key.AccountName,
        //                                            Description = group.Key.Description,
        //                                            Debit = 0m,
        //                                            Credit = group.Sum(item => item.Amount)
        //                                       });
        //    }
        //    return _context.PurchaseHeaders.Where(header => header.Id == purchaseHeaderId)
        //                                       .Join(_context.PurchaseDetails.Join(_context.ChartOfAccounts,
        //                                                                                 detail => 201,
        //                                                                                 coa => coa.AccountNo,
        //                                                                                 (detail, coa) => new
        //                                                                                 {
        //                                                                                     detail.PurchaseHeaderId,
        //                                                                                     detail.Quantity,
        //                                                                                     detail.Price,
        //                                                                                     coa.AccountNo,
        //                                                                                     coa.AccountName,
        //                                                                                 }),
        //                                                  header => header.Id,
        //                                                  detail => detail.PurchaseHeaderId,
        //                                                  (header, detail) => new
        //                                                  {
        //                                                      header.TransactionNo,
        //                                                      header.PurchaseDate,
        //                                                      header.Description,
        //                                                      detail.AccountNo,
        //                                                      detail.AccountName,
        //                                                      detail.Quantity,
        //                                                      detail.Price,
        //                                                  })
        //                                       .GroupBy(header => new
        //                                       {
        //                                           header.TransactionNo,
        //                                           header.PurchaseDate,
        //                                           header.AccountNo,
        //                                           header.AccountName,
        //                                           header.Description
        //                                       })
        //                                       .OrderBy(group => group.Key.PurchaseDate)
        //                                       .Select(group => new PurchaseEntryContainer
        //                                       {
        //                                           TransactionNo = group.Key.TransactionNo,
        //                                           PurchaseDate = group.Key.PurchaseDate,
        //                                           AccountNo = group.Key.AccountNo,
        //                                           AccountName = group.Key.AccountName,
        //                                           Description = group.Key.Description,
        //                                           Debit = 0m,
        //                                           Credit = group.Sum(item => item.Quantity * item.Price)
        //                                       });
        //}

        //private IQueryable<PurchaseEntryContainer> GetPayablePurchaseCreditQueryById(Guid purchaseHeaderId)
        //{
        //    return _context.PurchaseHeaders.Where(header => header.Id == purchaseHeaderId)
        //                                                            .Join(_context.ChartOfAccounts,
        //                                                                  header => 201,
        //                                                                  coa => coa.AccountNo,
        //                                                                  (header, coa) => new {
        //                                                                      header.Id,
        //                                                                      header.TransactionNo,
        //                                                                      header.PurchaseDate,
        //                                                                      header.Description,
        //                                                                      AccountNo = coa.AccountNo,
        //                                                                      AccountName = coa.AccountName
        //                                                                  })
        //                                                            .Join(_context.PurchaseDetails,
        //                                                                      header => header.Id,
        //                                                                      detail => detail.PurchaseHeaderId,
        //                                                                      (header, detail) => new
        //                                                                      {
        //                                                                          TransactionNo = header.TransactionNo,
        //                                                                          PurchaseDate = header.PurchaseDate,
        //                                                                          Description = header.Description,
        //                                                                          AccountNo = header.AccountNo,
        //                                                                          AccountName = header.AccountName,
        //                                                                          Debit = 0m,
        //                                                                          Credit = detail.Price * detail.Quantity
        //                                                                      })
        //                                                            .GroupBy(result => new
        //                                                            {
        //                                                                result.TransactionNo,
        //                                                                result.PurchaseDate,
        //                                                                result.AccountNo,
        //                                                                result.AccountName,
        //                                                                result.Description,
        //                                                            })
        //                                                            .Select(group => new PurchaseEntryContainer
        //                                                            {
        //                                                                TransactionNo = group.Key.TransactionNo,
        //                                                                PurchaseDate = group.Key.PurchaseDate,
        //                                                                Description = group.Key.Description,
        //                                                                AccountNo = group.Key.AccountNo,
        //                                                                AccountName = group.Key.AccountName,
        //                                                                Debit = 0m,
        //                                                                Credit = group.Sum(item => item.Credit),
        //                                                            });
        //}

        //private IQueryable<PurchaseEntryContainer> GetPaidPurchaseDebitQueryById(Guid purchaseHeaderId)
        //{
        //    return _context.PurchaseHeaders.Where(header => header.Id == purchaseHeaderId)
        //                                                            .Join(_context.ChartOfAccounts,
        //                                                                  header => 201,
        //                                                                  coa => coa.AccountNo,
        //                                                                  (header, coa) => new {
        //                                                                      header.Id,
        //                                                                      header.TransactionNo,
        //                                                                      header.Description,
        //                                                                      AccountNo = coa.AccountNo,
        //                                                                      AccountName = coa.AccountName
        //                                                                  })
        //                                                            .Join(_context.PurchasePayments,
        //                                                                  header => header.Id,
        //                                                                  payment => payment.PurchaseHeaderId,
        //                                                                  (header, payment) => new
        //                                                                  {
        //                                                                      TransactionNo = header.TransactionNo,
        //                                                                      TransactionDate = payment.PaymentDate,
        //                                                                      Description = header.Description,
        //                                                                      AccountNo = header.AccountNo,
        //                                                                      AccountName = header.AccountName,
        //                                                                      Debit = payment.Amount,
        //                                                                      Credit = 0m,
        //                                                                  })
        //                                                            .GroupBy(result => new
        //                                                            {
        //                                                                result.TransactionNo,
        //                                                                result.TransactionDate,
        //                                                                result.AccountNo,
        //                                                                result.AccountName,
        //                                                                result.Description,
        //                                                            })
        //                                                            .OrderBy(group => group.Key.TransactionDate)
        //                                                            .Select(group => new PurchaseEntryContainer
        //                                                            {
        //                                                                TransactionNo = group.Key.TransactionNo,
        //                                                                PurchaseDate = group.Key.TransactionDate,
        //                                                                Description = group.Key.Description,
        //                                                                AccountNo = group.Key.AccountNo,
        //                                                                AccountName = group.Key.AccountName,
        //                                                                Debit = group.Sum(item => item.Debit),
        //                                                                Credit = 0m,
        //                                                            });
        //}

        //private IQueryable<PurchaseEntryContainer> GetPaidPurchaseCreditQueryById(Guid purchaseHeaderId)
        //{
        //    return _context.PurchaseHeaders.Where(header => header.Id == purchaseHeaderId)
        //                                                            .Join(_context.PurchasePayments.Join(_context.ChartOfAccounts,
        //                                                                                                 payment => payment.Type == PaymentType.Cash.ToString() ? 111 : 112,
        //                                                                                                 coa => coa.AccountNo,
        //                                                                                                 (payment, coa) => new
        //                                                                                                 {
        //                                                                                                     PurchaseHeaderId = payment.PurchaseHeaderId,
        //                                                                                                     PurchaseDate = payment.PaymentDate,
        //                                                                                                     Debit = 0m,
        //                                                                                                     Credit = payment.Amount,
        //                                                                                                     AccountNo = coa.AccountNo,
        //                                                                                                     AccountName = coa.AccountName
        //                                                                                                 }),
        //                                                                  header => header.Id,
        //                                                                  payment => payment.PurchaseHeaderId,
        //                                                                  (header, payment) => new
        //                                                                  {
        //                                                                      TransactionNo = header.TransactionNo,
        //                                                                      TransactionDate = payment.PurchaseDate,
        //                                                                      Description = header.Description,
        //                                                                      AccountNo = payment.AccountNo,
        //                                                                      AccountName = payment.AccountName,
        //                                                                      Debit = payment.Debit,
        //                                                                      Credit = payment.Credit,
        //                                                                  })
        //                                                            .GroupBy(result => new
        //                                                            {
        //                                                                result.TransactionNo,
        //                                                                result.TransactionDate,
        //                                                                result.AccountNo,
        //                                                                result.AccountName,
        //                                                                result.Description,
        //                                                            })
        //                                                            .OrderBy(group => group.Key.TransactionDate)
        //                                                            .Select(group => new PurchaseEntryContainer
        //                                                            {
        //                                                                TransactionNo = group.Key.TransactionNo,
        //                                                                PurchaseDate = group.Key.TransactionDate,
        //                                                                Description = group.Key.Description,
        //                                                                AccountNo = group.Key.AccountNo,
        //                                                                AccountName = group.Key.AccountName,
        //                                                                Debit = 0m,
        //                                                                Credit = group.Sum(item => item.Credit),
        //                                                            });
        //}


        private IQueryable<JournalEntryContainer> GetSalesDebitQuery()
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

        private IQueryable<JournalEntryContainer> GetSalesCreditQuery()
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
        public string? AccountName { get; set; }

        [Precision(19, 3)]
        public Decimal? Debit { get; set; }

        [Precision(19, 3)]
        public Decimal? Credit { get; set; }
    }

    public class PurchaseEntryContainer
    {
        public int AccountNo { get; set; }
        public string AccountName { get; set; } = default!;
        public Guid Id { get; set; }
        public string TransactionNo { get; set; } = default!;
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string? Reference { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
    public class SalesEntryContainer
    {
        public int AccountNo { get; set; }
        public string AccountName { get; set; } = default!;
        public Guid Id { get; set; }
        public string TransactionNo { get; set; } = default!;
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string? Reference { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}
