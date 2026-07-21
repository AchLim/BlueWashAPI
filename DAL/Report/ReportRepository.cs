using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Data;
using WebAPI.Exception;

namespace WebAPI.DAL
{
    public sealed class ReportRepository : IReportRepository
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ReportRepository> _logger;

        public ReportRepository(ILogger<ReportRepository> logger, ApplicationContext context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<GeneralLedgerContainer>> GetGeneralLedgerData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<GeneralLedgerContainer> generalLedgerData = new List<GeneralLedgerContainer>();

            await using (var transaction = await _context.Database.BeginTransactionAsync()) 
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            -- Journal Entry
                            SELECT
                                coa.AccountNo,
                                coa.AccountName,
                                je.TransactionNo,
                                je.TransactionDate,
                                je.Description,
                                je.Reference,
                                je.Debit,
                                je.Credit
                            FROM
                                chart_of_account coa
                                LEFT JOIN (
                                    SELECT
                                        entry.TransactionNo,
                                        entry.TransactionDate,
                                        entry.Description,
                                        '' AS Reference,
                                        innerCoa.AccountNo,
                                        item.Debit,
                                        item.Credit
                                    FROM
                                        journal_entry entry
                                        INNER JOIN journal_item item ON entry.Id = item.JournalEntryId
			                            INNER JOIN chart_of_account innerCoa ON innerCoa.Id = item.ChartOfAccountId
                                    WHERE
                                        entry.TransactionDate BETWEEN @DateFrom AND @DateTo AND entry.Status = 'Posted'

		                            UNION ALL

		                            SELECT
			                            q.TransactionNo,
			                            q.TransactionDate,
			                            q.Description,
			                            q.Reference,
			                            q.AccountNo,
			                            q.Debit AS Debit,
			                            q.Credit AS Credit
		                            FROM (

				                            -- Debit - Supplies
				                            SELECT
					                            coa.AccountNo,
					                            ph.Id,
					                            ph.PurchaseNo AS TransactionNo,
					                            ph.PurchaseDate AS TransactionDate,
					                            ph.Description,
					                            CONCAT(s.SupplierCode, ' ', s.SupplierName) AS Reference,
					                            COALESCE(pd.Quantity * pd.Price, 0) AS Debit,
					                            0 AS Credit
				                            FROM
					                            purchase_header ph
						                            JOIN supplier s ON s.Id = ph.SupplierId
						                            LEFT JOIN purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
						                            JOIN chart_of_account coa ON coa.AccountNo = 113
				                            WHERE
					                            ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
				                            GROUP BY
					                            ph.Id,
					                            ph.PurchaseNo,
					                            ph.PurchaseDate,
					                            ph.Description,
					                            s.SupplierCode,
					                            s.SupplierName,
					                            coa.AccountNo,
					                            pd.Quantity,
					                            pd.Price

				                            UNION ALL

				                            -- Credit - Purchase Discount
				                            SELECT
					                            coa.AccountNo,
					                            ph.Id,
					                            ph.PurchaseNo AS TransactionNo,
					                            ph.PurchaseDate AS TransactionDate,
					                            ph.Description,
					                            CONCAT(s.SupplierCode, ' ', s.SupplierName) AS Reference,
					                            0 AS Debit,
					                            COALESCE(pd.Discount, 0) AS Credit
				                            FROM
					                            purchase_header ph
						                            JOIN supplier s ON s.Id = ph.SupplierId
						                            LEFT JOIN purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
						                            JOIN chart_of_account coa ON coa.AccountNo = 502
				                            WHERE
					                            ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
				                            GROUP BY
					                            ph.Id,
					                            ph.PurchaseNo,
					                            ph.PurchaseDate,
					                            ph.Description,
					                            s.SupplierCode,
					                            s.SupplierName,
					                            coa.AccountNo,
					                            pd.Discount
				                            HAVING
					                            pd.Discount <> 0

				                            UNION ALL

				                            -- Credit - Account Payable
				                            SELECT
					                            coa.AccountNo,
					                            ph.Id,
					                            ph.PurchaseNo AS TransactionNo,
					                            ph.PurchaseDate AS TransactionDate,
					                            ph.Description,
					                            CONCAT(s.SupplierCode, ' ', s.SupplierName) AS Reference,
					                            0 AS Debit,
					                            COALESCE((pd.Quantity * pd.Price) - pd.Discount, 0) AS Credit
				                            FROM
					                            purchase_header ph
						                            JOIN supplier s ON s.Id = ph.SupplierId
						                            LEFT JOIN purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
						                            JOIN chart_of_account coa ON coa.AccountNo = 201
				                            WHERE
					                            ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
				                            GROUP BY
					                            ph.Id,
					                            ph.PurchaseNo,
					                            ph.PurchaseDate,
					                            ph.Description,
					                            s.SupplierCode,
					                            s.SupplierName,
					                            coa.AccountNo,
					                            pd.Quantity,
					                            pd.Price,
					                            pd.Discount

				                            UNION ALL

				                            -- Debit of Account Payable
				                            SELECT
					                            coa.AccountNo,
					                            ph.Id,
					                            ph.PurchaseNo AS TransactionNo,
					                            pp.PaymentDate AS TransactionDate,
					                            ph.Description,
					                            CONCAT('Pembayaran: ', s.SupplierCode, ' ', s.SupplierName) AS Reference,
					                            pp.Amount AS Debit,
					                            0 AS Credit
				                            FROM
					                            purchase_header ph
						                            JOIN supplier s ON s.Id = ph.SupplierId
						                            JOIN purchase_payment pp ON ph.Id = pp.PurchaseHeaderId
						                            JOIN chart_of_account coa ON coa.AccountNo = 201
				                            WHERE
					                            pp.PaymentDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
				                            GROUP BY
					                            ph.Id,
					                            ph.PurchaseNo,
					                            pp.PaymentDate,
					                            ph.Description,
					                            s.SupplierCode,
					                            s.SupplierName,
					                            coa.AccountNo,
					                            pp.Type,
					                            pp.Amount

				                            UNION ALL

				                            -- Credit of Cash / Bank
				                            SELECT
					                            coa.AccountNo,
					                            ph.Id,
					                            ph.PurchaseNo AS TransactionNo,
					                            pp.PaymentDate AS TransactionDate,
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
				                            WHERE
					                            pp.PaymentDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
				                            GROUP BY
					                            ph.Id,
					                            ph.PurchaseNo,
					                            pp.PaymentDate,
					                            ph.Description,
					                            s.SupplierCode,
					                            s.SupplierName,
					                            coa.AccountNo,
					                            pp.Type,
					                            pp.Amount

				                            UNION ALL

				                            -- Debit of Account Receivable
				                            SELECT
					                            coa.AccountNo,
					                            sh.Id,
					                            sh.SalesNo AS TransactionNo,
					                            sh.SalesDate AS TransactionDate,
					                            sh.Description,
					                            CONCAT(c.CustomerCode, ' ', c.CustomerName) AS Reference,
					                            COALESCE((sd.Quantity * sd.Price) - sd.Discount, 0) AS Debit,
					                            0 AS Credit
				                            FROM
					                            sales_header sh
						                            JOIN customer c ON c.Id = sh.CustomerId
						                            LEFT JOIN sales_detail sd ON sh.Id = sd.SalesHeaderId
						                            JOIN chart_of_account coa ON coa.AccountNo = 116
				                            WHERE
					                            sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
				                            GROUP BY
					                            sh.Id,
					                            sh.SalesNo,
					                            sh.SalesDate,
					                            sh.Description,
					                            c.CustomerCode,
					                            c.CustomerName,
					                            coa.AccountNo,
					                            sd.Quantity,
					                            sd.Price,
					                            sd.Discount

				                            UNION ALL

				                            -- Credit of Sales Discount
				                            SELECT
					                            coa.AccountNo,
					                            sh.Id,
					                            sh.SalesNo AS TransactionNo,
					                            sh.SalesDate AS TransactionDate,
					                            sh.Description,
					                            CONCAT(c.CustomerCode, ' ', c.CustomerName) AS Reference,
					                            sd.Discount AS Debit,
					                            0 AS Credit
				                            FROM
					                            sales_header sh
						                            JOIN customer c ON c.Id = sh.CustomerId
						                            LEFT JOIN sales_detail sd ON sh.Id = sd.SalesHeaderId
						                            JOIN chart_of_account coa ON coa.AccountNo = 420
				                            WHERE
					                            sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
				                            GROUP BY
					                            sh.Id,
					                            sh.SalesNo,
					                            sh.SalesDate,
					                            sh.Description,
					                            c.customerCode,
					                            c.customerName,
					                            coa.AccountNo,
					                            sd.Discount
				                            HAVING
					                            sd.Discount <> 0

				                            UNION ALL

				                            -- Credit of Revenue
				                            SELECT
					                            coa.AccountNo,
					                            sh.Id,
					                            sh.SalesNo AS TransactionNo,
					                            sh.SalesDate AS TransactionDate,
					                            sh.Description,
					                            CONCAT(c.CustomerCode, ' ', c.CustomerName) AS Reference,
					                            0 AS Debit,
					                            COALESCE(sd.Quantity * sd.Price, 0) AS Credit
				                            FROM
					                            sales_header sh
						                            JOIN customer c ON c.Id = sh.CustomerId
						                            LEFT JOIN sales_detail sd ON sh.Id = sd.SalesHeaderId
						                            JOIN chart_of_account coa ON coa.AccountNo = 401
				                            WHERE
					                            sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
				                            GROUP BY
					                            sh.Id,
					                            sh.SalesNo,
					                            sh.SalesDate,
					                            sh.Description,
					                            c.customerCode,
					                            c.customerName,
					                            coa.AccountNo,
					                            sd.Quantity,
					                            sd.Price

				                            UNION ALL

				                            -- Credit of Account Payable
				                            SELECT
					                            coa.AccountNo,
					                            sh.Id,
					                            sh.SalesNo AS TransactionNo,
					                            sp.PaymentDate AS TransactionDate,
					                            sh.Description,
					                            CONCAT('Penjualan: ', c.CustomerCode, ' ', c.CustomerName) AS Reference,
					                            0 AS Debit,
					                            sp.Amount AS Credit
				                            FROM
					                            sales_header sh
						                            JOIN customer c ON c.Id = sh.CustomerId
						                            JOIN sales_payment sp ON sh.Id = sp.SalesHeaderId
						                            JOIN chart_of_account coa ON coa.AccountNo = 116
				                            WHERE
					                            sp.PaymentDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
				                            GROUP BY
					                            sh.Id,
					                            sh.SalesNo,
					                            sp.PaymentDate,
					                            sh.Description,
					                            c.customerCode,
					                            c.customerName,
					                            coa.AccountNo,
					                            sp.Type,
					                            sp.Amount

				                            UNION ALL

				                            -- Debit of Cash / Bank
				                            SELECT
					                            coa.AccountNo,
					                            sh.Id,
					                            sh.SalesNo AS TransactionNo,
					                            sp.PaymentDate AS TransactionDate,
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
				                            WHERE
					                            sp.PaymentDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
				                            GROUP BY
					                            sh.Id,
					                            sh.SalesNo,
					                            sp.PaymentDate,
					                            sh.Description,
					                            c.customerCode,
					                            c.customerName,
					                            coa.AccountNo,
					                            sp.Type,
					                            sp.Amount
			                            ) q
                                ) je ON coa.AccountNo = je.AccountNo
                            ORDER BY
	                            coa.AccountNo,
	                            je.TransactionDate,
	                            je.Description,
	                            je.Reference
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                // Manually map the result to your custom models
                                var accountNo = result.GetInt32(result.GetOrdinal("AccountNo"));
                                var accountName = result.GetString(result.GetOrdinal("AccountName"));
                                var transactionNo = result.IsDBNull(result.GetOrdinal("TransactionNo")) ? null : result.GetString(result.GetOrdinal("TransactionNo"));
                                DateTime? transactionDate = result.IsDBNull(result.GetOrdinal("TransactionDate")) ? null : result.GetDateTime(result.GetOrdinal("TransactionDate"));
                                var description = result.IsDBNull(result.GetOrdinal("Description")) ? null : result.GetString(result.GetOrdinal("Description"));
                                var reference = result.IsDBNull(result.GetOrdinal("Reference")) ? null : result.GetString(result.GetOrdinal("Reference"));
                                decimal? debit = result.IsDBNull(result.GetOrdinal("Debit")) ? null : result.GetDecimal(result.GetOrdinal("Debit"));
                                decimal? credit = result.IsDBNull(result.GetOrdinal("Credit")) ? null : result.GetDecimal(result.GetOrdinal("Credit"));

                                // Create or find the GeneralLedgerContainer
                                var ledgerContainer = generalLedgerData.FirstOrDefault(l => l.AccountNo == accountNo);
                                if (ledgerContainer == null)
                                {
                                    ledgerContainer = new GeneralLedgerContainer
                                    {
                                        AccountNo = accountNo,
                                        AccountName = accountName,
                                        Entries = new List<GeneralLedgerDetailContainer>()
                                    };
                                    generalLedgerData.Add(ledgerContainer);
                                }

                                // Add the GeneralLedgerDetailContainer to the GeneralLedgerContainer
                                if (transactionNo is not null)
                                {
                                    var ledgerDetail = new GeneralLedgerDetailContainer
                                    {
                                        TransactionNo = transactionNo,
                                        TransactionDate = DateOnly.FromDateTime(transactionDate!.Value),
                                        Description = description,
                                        Reference = reference,
                                        Debit = debit!.Value,
                                        Credit = credit!.Value,
                                    };
                                    ledgerContainer.Entries.Add(ledgerDetail);
                                }
                            }
                        }
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("An error occurred while retrieving general ledger data", ex);
                }
            }

            return generalLedgerData;
        }
        public async Task<IEnumerable<TrialBalanceContainer>> GetTrialBalanceData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<TrialBalanceContainer> trialBalanceData = new List<TrialBalanceContainer>();

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            -- Trial Balance
                            SELECT
                                coa.AccountNo,
                                coa.AccountName,
                                IIF(SUM(COALESCE(je.Debit - je.Credit, 0)) > 0, SUM(je.Debit - je.Credit), 0) AS Debit,
                                IIF(SUM(COALESCE(je.Debit - je.Credit, 0)) < 0, SUM(je.Debit - je.Credit), 0) AS Credit
                            FROM
                                chart_of_account coa
                                LEFT JOIN (
                                    SELECT
                                        innerCoa.AccountNo,
                                        COALESCE(item.Debit, 0) AS Debit,
                                        COALESCE(item.Credit, 0) AS Credit
                                    FROM
                                        journal_entry entry
                                        INNER JOIN journal_item item ON entry.Id = item.JournalEntryId
                                        INNER JOIN chart_of_account innerCoa ON innerCoa.Id = item.ChartOfAccountId
                                    WHERE entry.TransactionDate BETWEEN @DateFrom AND @DateTo AND entry.Status = 'Posted'

                                    UNION ALL

                                    SELECT
                                        q.AccountNo,
                                        q.Debit,
                                        q.Credit
                                    FROM (

                                            -- Debit - Supplies
                                            SELECT
                                                coa.AccountNo,
                                                SUM(COALESCE(pd.Quantity * pd.Price, 0)) AS Debit,
                                                0 AS Credit
                                            FROM
                                                purchase_header ph
                                                    JOIN supplier s ON s.Id = ph.SupplierId
                                                    LEFT JOIN purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
                                                    JOIN chart_of_account coa ON coa.AccountNo = 113
                                            WHERE
                                                ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo

                                            UNION ALL

                                            -- Credit - Purchase Discount
                                            SELECT
                                                coa.AccountNo,
                                                0 AS Debit,
                                                SUM(COALESCE(pd.Discount, 0)) AS Credit
                                            FROM
                                                purchase_header ph
                                                    JOIN supplier s ON s.Id = ph.SupplierId
                                                    LEFT JOIN purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
                                                    JOIN chart_of_account coa ON coa.AccountNo = 502
                                            WHERE
                                                ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo

                                            UNION ALL

                                            -- Credit - Account Payable
                                            SELECT
                                                coa.AccountNo,
                                                0 AS Debit,
                                                SUM(COALESCE((pd.Quantity * pd.Price) - pd.Discount, 0)) AS Credit
                                            FROM
                                                purchase_header ph
                                                    JOIN supplier s ON s.Id = ph.SupplierId
                                                    LEFT JOIN purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
                                                    JOIN chart_of_account coa ON coa.AccountNo = 201
                                            WHERE
                                                ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo

                                            UNION ALL

                                            -- Debit of Account Payable
                                            SELECT
                                                coa.AccountNo,
                                                SUM(COALESCE(pp.Amount, 0)) AS Debit,
                                                0 AS Credit
                                            FROM
                                                purchase_header ph
                                                    JOIN supplier s ON s.Id = ph.SupplierId
                                                    JOIN purchase_payment pp ON ph.Id = pp.PurchaseHeaderId
                                                    JOIN chart_of_account coa ON coa.AccountNo = 201
                                            WHERE
                                                pp.PaymentDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo

                                            UNION ALL

                                            -- Credit of Cash / Bank
                                            SELECT
                                                coa.AccountNo,
                                                0 AS Debit,
                                                SUM(COALESCE(pp.Amount, 0)) AS Credit
                                            FROM
                                                purchase_header ph
                                                    JOIN supplier s ON s.Id = ph.SupplierId
                                                    JOIN purchase_payment pp ON ph.Id = pp.PurchaseHeaderId
                                                    JOIN chart_of_account coa ON coa.AccountNo = CASE pp.Type
                                                        WHEN 'Cash' THEN 111
                                                        WHEN 'Bank' THEN 112
                                                        ELSE 111
                                                    END
                                            WHERE
                                                pp.PaymentDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo

                                            UNION ALL

                                            -- Debit of Account Receivable
                                            SELECT
                                                coa.AccountNo,
                                                SUM(COALESCE((sd.Quantity * sd.Price) - sd.Discount, 0)) AS Debit,
                                                0 AS Credit
                                            FROM
                                                sales_header sh
                                                    JOIN customer c ON c.Id = sh.CustomerId
                                                    LEFT JOIN sales_detail sd ON sh.Id = sd.SalesHeaderId
                                                    JOIN chart_of_account coa ON coa.AccountNo = 116
                                            WHERE
                                                sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo

                                            UNION ALL

                                            -- Credit of Sales Discount
                                            SELECT
                                                coa.AccountNo,
                                                SUM(COALESCE(sd.Discount, 0)) AS Debit,
                                                0 AS Credit
                                            FROM
                                                sales_header sh
                                                    JOIN customer c ON c.Id = sh.CustomerId
                                                    LEFT JOIN sales_detail sd ON sh.Id = sd.SalesHeaderId
                                                    JOIN chart_of_account coa ON coa.AccountNo = 420
                                            WHERE
                                                sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo

                                            UNION ALL

                                            -- Credit of Revenue
                                            SELECT
                                                coa.AccountNo,
                                                0 AS Debit,
                                                SUM(COALESCE(sd.Quantity * sd.Price, 0)) AS Credit
                                            FROM
                                                sales_header sh
                                                    JOIN customer c ON c.Id = sh.CustomerId
                                                    LEFT JOIN sales_detail sd ON sh.Id = sd.SalesHeaderId
                                                    JOIN chart_of_account coa ON coa.AccountNo = 401
                                            WHERE
                                                sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo

                                            UNION ALL

                                            -- Credit of Account Payable
                                            SELECT
                                                coa.AccountNo,
                                                0 AS Debit,
                                                SUM(COALESCE(sp.Amount, 0)) AS Credit
                                            FROM
                                                sales_header sh
                                                    JOIN customer c ON c.Id = sh.CustomerId
                                                    JOIN sales_payment sp ON sh.Id = sp.SalesHeaderId
                                                    JOIN chart_of_account coa ON coa.AccountNo = 116
                                            WHERE
                                                sp.PaymentDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo

                                            UNION ALL

                                            -- Debit of Cash / Bank
                                            SELECT
                                                coa.AccountNo,
                                                SUM(COALESCE(sp.Amount, 0)) AS Debit,
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
                                            WHERE
                                                sp.PaymentDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
                                            GROUP BY
                                                coa.AccountNo
                                        ) q
                                ) je ON coa.AccountNo = je.AccountNo
                            GROUP BY
                                coa.AccountNo,
                                coa.AccountName
                            ORDER BY
                                coa.AccountNo
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                // Manually map the result to your custom models
                                var accountNo = result.GetInt32(result.GetOrdinal("AccountNo"));
                                var accountName = result.GetString(result.GetOrdinal("AccountName"));
                                decimal debit = result.GetDecimal(result.GetOrdinal("Debit"));
                                decimal credit = result.GetDecimal(result.GetOrdinal("Credit"));

                                // Create or find the GeneralLedgerContainer
                                var trialBalanceContainer = new TrialBalanceContainer
                                {
                                    AccountNo = accountNo,
                                    AccountName = accountName,
									Debit = debit,
									Credit = credit,
                                };
                                trialBalanceData.Add(trialBalanceContainer);
                            }
                        }
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("An error occurred while retrieving trial balance data", ex);
                }
            }

            return trialBalanceData;
        }
        public async Task<IEnumerable<IncomeStatementContainer>> GetIncomeStatementData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<IncomeStatementContainer> incomeStatementData = new List<IncomeStatementContainer>();

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
	                            income_statement.AccountNo,
	                            income_statement.AccountName,
	                            SUM(income_statement.Balance) AS Balance
                            FROM
	                            (
		                            -- Journal Entry -- Revenue & Expense -- Manual Beban Persediaan
		                            SELECT
			                            innerCoa.AccountNo,
			                            innerCoa.AccountName,
			                            IIF(COALESCE(ji.Debit, 0) != 0, ji.Debit, 0) - IIF(COALESCE(ji.Credit, 0) != 0, ji.Credit, 0) AS Balance
		                            FROM
			                            chart_of_account innerCoa
		                            CROSS JOIN
			                            journal_entry je
		                            LEFT JOIN
			                            journal_item ji ON ji.JournalEntryId = je.Id AND ji.ChartOfAccountId = innerCoa.Id
		                            WHERE innerCoa.AccountHeaderNo IN (400, 500, 600) AND je.TransactionDate BETWEEN @DateFrom AND @DateTo AND je.Status = 'Posted'
		                            GROUP BY
			                            innerCoa.AccountHeaderNo,
			                            innerCoa.AccountNo,
			                            innerCoa.AccountName,
			                            ji.Debit,
			                            ji.Credit

		                            UNION ALL

		                            -- Purchase Discount
		                            SELECT
			                            innerCoa.AccountNo,
			                            innerCoa.AccountName,
			                            -SUM(pd.Discount) AS Balance
		                            FROM
			                            purchase_header ph
		                            LEFT JOIN
			                            purchase_detail pd ON pd.PurchaseHeaderId = ph.Id
		                            JOIN
			                            chart_of_account innerCoa ON innerCoa.AccountNo = 502
		                            WHERE ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
		                            GROUP BY
			                            innerCoa.AccountHeaderNo,
			                            innerCoa.AccountNo,
			                            innerCoa.AccountName

		                            UNION ALL

		                            -- Sales Revenue
		                            SELECT
			                            innerCoa.AccountNo,
			                            innerCoa.AccountName,
			                            -SUM(sd.Quantity * sd.Price) AS Balance
		                            FROM
			                            sales_header sh
		                            LEFT JOIN
			                            sales_detail sd ON sd.SalesHeaderId = sh.Id
		                            JOIN
			                            chart_of_account innerCoa ON innerCoa.AccountNo = 401
		                            WHERE sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
		                            GROUP BY
			                            innerCoa.AccountHeaderNo,
			                            innerCoa.AccountNo,
			                            innerCoa.AccountName

		                            UNION ALL

		                            -- Sales Discount
		                            SELECT
			                            innerCoa.AccountNo,
			                            innerCoa.AccountName,
			                            SUM(sd.Discount) AS Balance
		                            FROM
			                            sales_header sh
		                            LEFT JOIN
			                            sales_detail sd ON sd.SalesHeaderId = sh.Id
		                            JOIN
			                            chart_of_account innerCoa ON innerCoa.AccountNo = 420
		                            WHERE sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
		                            GROUP BY
			                            innerCoa.AccountHeaderNo,
			                            innerCoa.AccountNo,
			                            innerCoa.AccountName

	                            ) income_statement JOIN chart_of_account coa ON coa.AccountNo = income_statement.AccountNo
                            GROUP BY
	                            income_statement.AccountNo,
	                            income_statement.AccountName
                            ORDER BY
	                            income_statement.AccountNo
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                // Manually map the result to your custom models
                                var accountNo = result.GetInt32(result.GetOrdinal("AccountNo"));
                                var accountName = result.GetString(result.GetOrdinal("AccountName"));
                                decimal balance = result.GetDecimal(result.GetOrdinal("Balance"));

                                // Create or find the GeneralLedgerContainer
                                var incomeStatementContainer = new IncomeStatementContainer
                                {
                                    AccountNo = accountNo,
                                    AccountName = accountName,
                                    Balance = balance,
                                };
                                incomeStatementData.Add(incomeStatementContainer);
                            }
                        }
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("An error occurred while retrieving income statement data", ex);
                }
            }

            return incomeStatementData;
        }
        public async Task<IEnumerable<BalanceSheetContainer>> GetBalanceSheetData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<BalanceSheetContainer> balanceSheetData = new List<BalanceSheetContainer>();

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
                                income_statement.AccountNo,
                                income_statement.AccountName,
                                CASE
                                    WHEN income_statement.AccountNo >= 100 AND income_statement.AccountNo < 200 THEN SUM(income_statement.Debit) - SUM(income_statement.Credit)
                                    ELSE 0
                                END AS Debit,
                                CASE
                                    WHEN income_statement.AccountNo >= 200 AND income_statement.AccountNo < 400 THEN SUM(income_statement.Credit) - SUM(income_statement.Debit)
                                    ELSE 0
                                END AS Credit
                            FROM
                                (
                                    -- Journal Entry -- Assets
                                    SELECT
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName,
                                        SUM(COALESCE(ji.Debit, 0)) AS Debit,
                                        SUM(COALESCE(ji.Credit, 0)) AS Credit
                                    FROM
                                        chart_of_account innerCoa
                                    CROSS JOIN
                                        journal_entry je
                                    LEFT JOIN
                                        journal_item ji ON ji.JournalEntryId = je.Id AND ji.ChartOfAccountId = innerCoa.Id
                                    WHERE innerCoa.AccountHeaderNo IN (100, 200, 300) AND je.TransactionDate BETWEEN @DateFrom AND @DateTo AND je.Status = 'Posted'
                                    GROUP BY
                                        innerCoa.AccountHeaderNo,
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName

                                    UNION ALL

                                    -- Sales Revenue (Cash pada Revenue) -- Account Receivable
                                        SELECT
                                            coa.AccountNo,
                                            coa.AccountName,
                                            SUM(sales.Penjualan - sales.Pembayaran) AS [Debit],
                                            0 AS Credit
                                        FROM
                                            (
                                                -- Sales
                                                SELECT
                                                    c.CustomerName,
                                                    SUM((sd.Quantity * sd.Price) - sd.Discount) AS [Penjualan],
                                                    0 AS Pembayaran
                                                FROM
                                                    sales_header sh
                                                JOIN
                                                    customer c ON c.Id = sh.CustomerId
                                                JOIN
                                                    sales_detail sd ON sh.Id = sd.SalesHeaderId
                                                WHERE sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
                                                GROUP BY
                                                    c.CustomerName

                                                UNION

                                                -- Sales Payment
                                                SELECT
                                                    c.CustomerName,
                                                    0 AS Penjualan,
                                                    SUM(sp.Amount) AS [Pembayaran]
                                                FROM
                                                    sales_header sh
                                                JOIN
                                                    customer c ON c.Id = sh.CustomerId
                                                JOIN
                                                    sales_payment sp ON sp.SalesHeaderId = sh.Id
                                                WHERE sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sp.PaymentDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
                                                GROUP BY
                                                    c.CustomerName
                                            ) sales JOIN chart_of_account coa ON coa.AccountNo = 116
                                        GROUP BY
                                            coa.AccountNo,
                                            coa.AccountName

                                    UNION ALL

                                    -- Sales Revenue (Cash pada Revenue) -- Cash & Bank
                                    SELECT
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName,
                                        SUM(COALESCE(sp.Amount, 0)) AS Debit,
                                        0 AS Credit
                                    FROM
                                        sales_header sh
                                    JOIN
                                        sales_payment sp ON sp.SalesHeaderId = sh.Id
                                    JOIN
                                        chart_of_account innerCoa ON innerCoa.AccountNo = CASE WHEN sp.Type = 'Cash' THEN 111 ELSE 112 END
                                    WHERE sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sp.PaymentDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
                                    GROUP BY
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName

                                    UNION ALL

                                    -- Purchase Expense (Persediaan pada Kas) -- Account Payable
                                    --- Purchase Payment
                                    SELECT
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName,
                                        0 AS Debit,
                                        SUM(COALESCE(pp.Amount, 0)) AS Credit
                                    FROM
                                        purchase_header ph
                                    JOIN
                                        purchase_payment pp ON pp.PurchaseHeaderId = ph.Id
                                    JOIN
                                        chart_of_account innerCoa ON innerCoa.AccountNo = CASE WHEN pp.Type = 'Bank' THEN 112 ELSE 111 END
                                    WHERE ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND pp.PaymentDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
                                    GROUP BY
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName

                                    UNION ALL

                                    SELECT
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName,
                                        0 AS Debit,
                                        SUM(COALESCE(purchase.Pembelian - purchase.Pembayaran, 0)) AS Credit
                                    FROM
                                        (
                                            ---- Purchase Payment
                                            SELECT
                                                0 AS Pembelian,
                                                SUM(pp.Amount) AS Pembayaran
                                            FROM
                                                purchase_header ph
                                            JOIN
                                                purchase_payment pp ON pp.PurchaseHeaderId = ph.Id
                                            WHERE ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND pp.PaymentDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'

                                            UNION ALL

                                            ---- Purchase Amount
                                            SELECT
                                                SUM((pd.Quantity * pd.Price) - pd.Discount) AS Pembelian,
                                                0 AS Pembayaran
                                            FROM
                                                purchase_header ph
                                            JOIN
                                                purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
                                            WHERE ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'

                                        ) purchase JOIN chart_of_account innerCoa ON innerCoa.AccountNo = 201
                                    GROUP BY
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName

                                    UNION ALL

                                    --- Purchased Supplies
                                    SELECT
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName,
                                        SUM(pd.Quantity * pd.Price) AS Debit,
                                        0 AS Credit
                                    FROM
                                        purchase_header ph
                                    JOIN
                                        purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
                                    JOIN
                                        chart_of_account innerCoa ON innerCoa.AccountNo IN (113)
                                    WHERE ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
                                    GROUP BY
                                        innerCoa.AccountNo,
                                        innerCoa.AccountName
                                ) income_statement JOIN chart_of_account coa ON coa.AccountNo = income_statement.AccountNo
                            GROUP BY
                                income_statement.AccountNo,
                                income_statement.AccountName
                            ORDER BY
                                income_statement.AccountNo
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                // Manually map the result to your custom models
                                var accountNo = result.GetInt32(result.GetOrdinal("AccountNo"));
                                var accountName = result.GetString(result.GetOrdinal("AccountName"));
                                decimal debit = result.GetDecimal(result.GetOrdinal("Debit"));
                                decimal credit = result.GetDecimal(result.GetOrdinal("Credit"));

                                // Create or find the GeneralLedgerContainer
                                var balanceSheetContainer = new BalanceSheetContainer
                                {
                                    AccountNo = accountNo,
                                    AccountName = accountName,
                                    Debit = debit,
                                    Credit = credit
                                };
                                balanceSheetData.Add(balanceSheetContainer);
                            }
                        }
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw;
                    throw new DatabaseReadException("An error occurred while retrieving balance sheet data", ex);
                }
            }

            return balanceSheetData;
        }
        public async Task<IEnumerable<PurchaseReportContainer>> GetPurchaseReportData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<PurchaseReportContainer> purchaseReportData = new List<PurchaseReportContainer>();

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
	                            purchase.PurchaseDate,
	                            purchase.SupplierCode,
	                            purchase.SupplierName,
	                            SUM(purchase.Pembelian) AS [PurchaseAmount],
	                            SUM(purchase.Pembayaran) AS [PaymentAmount],
	                            SUM(purchase.Diskon) AS [DiscountAmount],
	                            SUM(purchase.Pembelian - purchase.Diskon - purchase.Pembayaran) AS [Balance]
                            FROM
	                            (
		                            -- Purchase
		                            SELECT
			                            ph.PurchaseDate,
			                            s.SupplierCode,
			                            s.SupplierName,
			                            SUM(pd.Quantity * pd.Price) AS [Pembelian],
			                            0 AS Pembayaran,
			                            SUM(pd.Discount) AS [Diskon]
		                            FROM
			                            purchase_header ph
		                            JOIN
			                            supplier s ON s.Id = ph.SupplierId
		                            JOIN
			                            purchase_detail pd ON ph.Id = pd.PurchaseHeaderId
		                            WHERE ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
		                            GROUP BY
			                            ph.PurchaseDate,
			                            s.SupplierCode,
			                            s.SupplierName

		                            UNION ALL

		                            -- Purchase Payment
		                            SELECT
			                            ph.PurchaseDate,
			                            s.SupplierCode,
			                            s.SupplierName,
			                            0 AS Pembelian,
			                            SUM(sp.Amount) AS [Pembayaran],
			                            0 AS Diskon
		                            FROM
			                            purchase_header ph
		                            JOIN
			                            supplier s ON s.Id = ph.SupplierId
		                            JOIN
			                            purchase_payment sp ON sp.PurchaseHeaderId = ph.Id
		                            WHERE ph.PurchaseDate BETWEEN @DateFrom AND @DateTo AND sp.PaymentDate BETWEEN @DateFrom AND @DateTo AND ph.Status = 'Posted'
		                            GROUP BY
			                            ph.PurchaseDate,
			                            s.SupplierCode,
			                            s.SupplierName
	                            ) purchase
                            GROUP BY
	                            purchase.PurchaseDate,
	                            purchase.SupplierCode,
	                            purchase.SupplierName
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                // Manually map the result to your custom models
                                var purchaseDate = result.GetDateTime(result.GetOrdinal("PurchaseDate"));
                                var supplierCode = result.GetString(result.GetOrdinal("SupplierCode"));
                                var supplierName = result.GetString(result.GetOrdinal("SupplierName"));
                                decimal purchaseAmount = result.GetDecimal(result.GetOrdinal("PurchaseAmount"));
                                decimal paymentAmount = result.GetDecimal(result.GetOrdinal("PaymentAmount"));
                                decimal discountAmount = result.GetDecimal(result.GetOrdinal("DiscountAmount"));
                                decimal balance = result.GetDecimal(result.GetOrdinal("Balance"));

                                // Create or find the GeneralLedgerContainer
                                var purchaseReportContainer = new PurchaseReportContainer
                                {
                                    PurchaseDate = DateOnly.FromDateTime(purchaseDate),
                                    SupplierCode = supplierCode,
                                    SupplierName = supplierName,
                                    PurchaseAmount = purchaseAmount,
                                    PaymentAmount = paymentAmount,
                                    DiscountAmount = discountAmount,
                                    Balance = balance
                                };
                                purchaseReportData.Add(purchaseReportContainer);
                            }
                        }
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("An error occurred while retrieving purchase report data", ex);
                }
            }

            return purchaseReportData;
        }
        public async Task<IEnumerable<SalesReportContainer>> GetSalesReportData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<SalesReportContainer> salesReportData = new List<SalesReportContainer>();

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
	                            sales.CustomerCode,
	                            sales.CustomerName,
	                            SUM(sales.Penjualan) AS [SalesAmount],
	                            SUM(sales.Pembayaran) AS [PaymentAmount],
	                            SUM(sales.Diskon) AS [DiscountAmount],
	                            SUM(sales.Penjualan - sales.Diskon - sales.Pembayaran) AS [Balance]
                            FROM
	                            (
		                            -- Sales
		                            SELECT
			                            c.CustomerName,
			                            c.CustomerCode,
			                            SUM(sd.Quantity * sd.Price) AS [Penjualan],
			                            0 AS Pembayaran,
			                            SUM(sd.Discount) AS Diskon
		                            FROM
			                            sales_header sh
		                            JOIN
			                            customer c ON c.Id = sh.CustomerId
		                            JOIN
			                            sales_detail sd ON sh.Id = sd.SalesHeaderId
		                            WHERE sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
		                            GROUP BY
			                            c.CustomerName,
			                            c.CustomerCode

		                            UNION ALL

		                            -- Sales Payment
		                            SELECT
			                            c.CustomerName,
			                            c.CustomerCode,
			                            0 AS Penjualan,
			                            SUM(sp.Amount) AS [Pembayaran],
			                            0 AS Diskon
		                            FROM
			                            sales_header sh
		                            JOIN
			                            customer c ON c.Id = sh.CustomerId
		                            JOIN
			                            sales_payment sp ON sp.SalesHeaderId = sh.Id
		                            WHERE sh.SalesDate BETWEEN @DateFrom AND @DateTo AND sp.PaymentDate BETWEEN @DateFrom AND @DateTo AND sh.Status = 'Posted'
		                            GROUP BY
			                            c.CustomerName,
			                            c.CustomerCode
	                            ) sales
                            GROUP BY
	                            sales.CustomerName,
	                            sales.CustomerCode
                            ORDER BY
	                            sales.CustomerCode
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                // Manually map the result to your custom models
                                var customerCode = result.GetString(result.GetOrdinal("CustomerCode"));
                                var customerName = result.GetString(result.GetOrdinal("CustomerName"));
                                decimal salesAmount = result.GetDecimal(result.GetOrdinal("SalesAmount"));
                                decimal paymentAmount = result.GetDecimal(result.GetOrdinal("PaymentAmount"));
                                decimal discountAmount = result.GetDecimal(result.GetOrdinal("DiscountAmount"));
                                decimal balance = result.GetDecimal(result.GetOrdinal("Balance"));

                                // Create or find the GeneralLedgerContainer
                                var salesReportContainer = new SalesReportContainer
                                {
                                    CustomerCode = customerCode,
                                    CustomerName = customerName,
                                    SalesAmount = salesAmount,
                                    PaymentAmount = paymentAmount,
                                    DiscountAmount = discountAmount,
                                    Balance = balance
                                };
                                salesReportData.Add(salesReportContainer);
                            }
                        }
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("An error occurred while retrieving sales report data", ex);
                }
            }

            return salesReportData;
        }

        //private IQueryable<GeneralLedgerDetailContainer> GetJournalEntryQuery()
        //{
        //    return _context.JournalEntries.Join(_context.JournalItems,
        //                                        entry => entry.Id,
        //                                        item => item.JournalEntryId,
        //                                        (entry, item) => new
        //                                        {
        //                                            entry.TransactionNo,
        //                                            entry.TransactionDate,
        //                                            entry.Description,
        //                                            item.ChartOfAccountId,
        //                                            item.Debit,
        //                                            item.Credit
        //                                        })
        //                                  .GroupBy(result => new
        //                                  {
        //                                      result.TransactionNo,
        //                                      result.TransactionDate,
        //                                      result.ChartOfAccountId,
        //                                      result.Description,
        //                                  })
        //                                  .Select(group => new GeneralLedgerDetailContainer
        //                                  {
        //                                      TransactionNo = group.Key.TransactionNo,
        //                                      TransactionDate = group.Key.TransactionDate,
        //                                      ChartOfAccountId = group.Key.ChartOfAccountId,
        //                                      Debit = group.Sum(item => item.Debit),
        //                                      Credit = group.Sum(item => item.Credit),
        //                                      Balance = group.Sum(item => item.Debit - item.Credit),
        //                                      Description = group.Key.Description,
        //                                  });
        //}

        //private IQueryable<GeneralLedgerDetailContainer> GetPurchaseEntryDebitQuery()
        //{
        //    return _context.PurchaseHeaders.Join(_context.PurchaseDetails,
        //                                         header => header.Id,
        //                                         detail => detail.PurchaseHeaderId,
        //                                         (header, detail) => new
        //                                         {
        //                                             header.PurchaseNo,
        //                                             header.PurchaseDate,
        //                                             header.Description,
        //                                             AccountNo = 113,
        //                                             Amount = detail.Quantity * detail.Price,
        //                                         })
        //                                   .Join(_context.ChartOfAccounts,
        //                                         entry => entry.AccountNo,
        //                                         coa => coa.AccountNo,
        //                                         (entry, coa) => new
        //                                         {
        //                                             entry.PurchaseNo,
        //                                             entry.PurchaseDate,
        //                                             entry.Description,
        //                                             ChartOfAccountId = coa.Id,
        //                                             Amount = entry.Amount,
        //                                         })
        //                                   .GroupBy(entry => new
        //                                   {
        //                                       entry.PurchaseNo,
        //                                       entry.PurchaseDate,
        //                                       entry.ChartOfAccountId,
        //                                       entry.Description,
        //                                   })
        //                                   .Select(group => new GeneralLedgerDetailContainer
        //                                   {
        //                                       TransactionNo = group.Key.PurchaseNo,
        //                                       TransactionDate = group.Key.PurchaseDate,
        //                                       ChartOfAccountId = group.Key.ChartOfAccountId,
        //                                       Debit = group.Sum(item => item.Amount),
        //                                       Credit = 0m,
        //                                       Balance = group.Sum(item => item.Amount),
        //                                       Description = group.Key.Description,
        //                                   });
        //}

        //private IQueryable<GeneralLedgerDetailContainer> GetPurchaseEntryCreditQuery()
        //{
        //    return _context.PurchaseHeaders.Join(_context.PurchaseDetails,
        //                                         header => header.Id,
        //                                         detail => detail.PurchaseHeaderId,
        //                                         (header, detail) => new
        //                                         {
        //                                             header.PurchaseNo,
        //                                             header.PurchaseDate,
        //                                             header.Description,
        //                                             AccountNo = header.PaymentTerm == "Immediate" ? 111 : 201,
        //                                             Amount = detail.Quantity * detail.Price,
        //                                         })
        //                                   .Join(_context.ChartOfAccounts,
        //                                         entry => entry.AccountNo,
        //                                         coa => coa.AccountNo,
        //                                         (entry, coa) => new
        //                                         {
        //                                             entry.PurchaseNo,
        //                                             entry.PurchaseDate,
        //                                             entry.Description,
        //                                             ChartOfAccountId = coa.Id,
        //                                             Amount = entry.Amount,
        //                                         })
        //                                   .GroupBy(entry => new
        //                                   {
        //                                       entry.PurchaseNo,
        //                                       entry.PurchaseDate,
        //                                       entry.ChartOfAccountId,
        //                                       entry.Description,
        //                                   })
        //                                   .Select(group => new GeneralLedgerDetailContainer
        //                                   {
        //                                       TransactionNo = group.Key.PurchaseNo,
        //                                       TransactionDate = group.Key.PurchaseDate,
        //                                       ChartOfAccountId = group.Key.ChartOfAccountId,
        //                                       Debit = 0m,
        //                                       Credit = group.Sum(item => item.Amount),
        //                                       Balance = 0m - group.Sum(item => item.Amount),
        //                                       Description = group.Key.Description,
        //                                   });
        //}
    }

    public class GeneralLedgerContainer
    {
        public int AccountNo { get; set; }
        public string AccountName { get; set; } = default!;

        public ICollection<GeneralLedgerDetailContainer> Entries { get; set; } = default!;
    }

    public class GeneralLedgerDetailContainer
    {
        public string TransactionNo { get; set; } = default!;
        public DateOnly TransactionDate { get; set; }
        public Guid ChartOfAccountId { get; set; }
        public string? Reference { get; set; }
        public string? Description { get; set; }

        [Precision(19, 3)]
        public Decimal Debit { get; set; }

        [Precision(19, 3)]
        public Decimal Credit { get; set; }
    }

    public class TrialBalanceContainer
    {
        public int AccountNo { get; set; }
        public string AccountName { get; set; } = default!;

        [Precision(19, 3)]
        public Decimal Debit { get; set; }

        [Precision(19, 3)]
        public Decimal Credit { get; set; }
    }

    public class IncomeStatementContainer
    {
        public int AccountNo { get; set; }
        public string AccountName { get; set; } = default!;

        [Precision(19, 3)]
        public Decimal Balance { get; set; }
    }

    public class BalanceSheetContainer
    {
        public int AccountNo { get; set; }
        public string AccountName { get; set; } = default!;

        [Precision(19, 3)]
        public Decimal Debit { get; set; }

        [Precision(19, 3)]
        public Decimal Credit { get; set; }
    }

    public class SalesReportContainer
    {
        public string CustomerCode { get; set; } = default!;
        public string CustomerName { get; set; } = default!;

        [Precision(19, 3)]
        public Decimal SalesAmount { get; set; }

        [Precision(19, 3)]
        public Decimal PaymentAmount { get; set; }

        [Precision(19, 3)]
        public Decimal DiscountAmount { get; set; }

        [Precision(19, 3)]
        public Decimal Balance { get; set; }
    }

    public class PurchaseReportContainer
    {
        public DateOnly PurchaseDate;
        public string SupplierCode { get; set; } = default!;
        public string SupplierName { get; set; } = default!;

        [Precision(19, 3)]
        public Decimal PurchaseAmount { get; set; }

        [Precision(19, 3)]
        public Decimal PaymentAmount { get; set; }

        [Precision(19, 3)]
        public Decimal DiscountAmount { get; set; }

        [Precision(19, 3)]
        public Decimal Balance { get; set; }
    }
}
