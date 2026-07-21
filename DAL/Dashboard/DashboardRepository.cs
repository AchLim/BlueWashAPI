using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Data;
using WebAPI.Exception;

namespace WebAPI.DAL
{
    public sealed class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<DashboardRepository> _logger;

        public DashboardRepository(ILogger<DashboardRepository> logger, ApplicationContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Decimal>> GetCashSalesDashboardData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<Decimal> salesDashboardData = new List<Decimal>();

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            WITH DateSeries AS (
                                SELECT
                                    DATEADD(MONTH, DATEDIFF(MONTH, 0, @DateFrom), 0) AS StartOfMonth
                                UNION ALL
                                SELECT
                                    DATEADD(MONTH, 1, StartOfMonth)
                                FROM
                                    DateSeries
                                WHERE
                                    DATEADD(MONTH, 1, StartOfMonth) <= @DateTo
                            )

                            SELECT
                                YEAR(DateSeries.StartOfMonth) AS [Year],
                                MONTH(DateSeries.StartOfMonth) AS [Month],
                                COALESCE(SUM(sp.Amount), 0) AS Amount
                            FROM
                                DateSeries
                            LEFT JOIN
                                sales_payment sp ON YEAR(sp.PaymentDate) = YEAR(DateSeries.StartOfMonth)
                                             AND MONTH(sp.PaymentDate) = MONTH(DateSeries.StartOfMonth)
                                             AND sp.Type = 'Cash'
                            GROUP BY
                                YEAR(DateSeries.StartOfMonth),
                                MONTH(DateSeries.StartOfMonth)
                            ORDER BY
                                [Year], [Month];
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
                                decimal amount = result.GetDecimal(result.GetOrdinal("Amount"));

                                // Create or find the GeneralLedgerContainer
                                salesDashboardData.Add(amount);
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
                    throw new DatabaseReadException("An error occurred while retrieving sales dashboard data", ex);
                }
            }

            return salesDashboardData;
        }

        public async Task<IEnumerable<Decimal>> GetBankSalesDashboardData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<Decimal> salesDashboardData = new List<Decimal>();

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            WITH DateSeries AS (
                                SELECT
                                    DATEADD(MONTH, DATEDIFF(MONTH, 0, @DateFrom), 0) AS StartOfMonth
                                UNION ALL
                                SELECT
                                    DATEADD(MONTH, 1, StartOfMonth)
                                FROM
                                    DateSeries
                                WHERE
                                    DATEADD(MONTH, 1, StartOfMonth) <= @DateTo
                            )

                            SELECT
                                YEAR(DateSeries.StartOfMonth) AS [Year],
                                MONTH(DateSeries.StartOfMonth) AS [Month],
                                COALESCE(SUM(sp.Amount), 0) AS Amount
                            FROM
                                DateSeries
                            LEFT JOIN
                                sales_payment sp ON YEAR(sp.PaymentDate) = YEAR(DateSeries.StartOfMonth)
                                             AND MONTH(sp.PaymentDate) = MONTH(DateSeries.StartOfMonth)
                                             AND sp.Type = 'Bank'
                            GROUP BY
                                YEAR(DateSeries.StartOfMonth),
                                MONTH(DateSeries.StartOfMonth)
                            ORDER BY
                                [Year], [Month];
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
                                decimal amount = result.GetDecimal(result.GetOrdinal("Amount"));
                                salesDashboardData.Add(amount);
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
                    throw new DatabaseReadException("An error occurred while retrieving sales dashboard data", ex);
                }
            }

            return salesDashboardData;
        }

        public async Task<IEnumerable<int>> GetSalesOrderDashboardData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<int> salesOrderDashboardData = new List<int>();

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            WITH DateSeries AS (
                                SELECT
                                    DATEADD(MONTH, DATEDIFF(MONTH, 0, @DateFrom), 0) AS StartOfMonth
                                UNION ALL
                                SELECT
                                    DATEADD(MONTH, 1, StartOfMonth)
                                FROM
                                    DateSeries
                                WHERE
                                    DATEADD(MONTH, 1, StartOfMonth) <= @DateTo
                            )

                            SELECT
                                YEAR(DateSeries.StartOfMonth) AS [Year],
                                MONTH(DateSeries.StartOfMonth) AS [Month],
                                COUNT(sh.Id) AS Total
                            FROM
                                DateSeries
                            LEFT JOIN
                                sales_header sh ON YEAR(sh.SalesDate) = YEAR(DateSeries.StartOfMonth)
                                                AND MONTH(sh.SalesDate) = MONTH(DateSeries.StartOfMonth)
                            GROUP BY
                                YEAR(DateSeries.StartOfMonth),
                                MONTH(DateSeries.StartOfMonth)
                            ORDER BY
                                [Year], [Month];
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
                                int total = result.GetInt32(result.GetOrdinal("Total"));
                                salesOrderDashboardData.Add(total);
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
                    throw new DatabaseReadException("An error occurred while retrieving sales order dashboard data", ex);
                }
            }

            return salesOrderDashboardData;
        }

        public async Task<IEnumerable<PaymentByTypeContainer>> GetPaymentByTypeDashboardData(DateOnly dateFrom, DateOnly dateTo)
        {
            List<PaymentByTypeContainer> paymentByTypeDashboardData = new List<PaymentByTypeContainer>();

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
	                            sp.Type,
	                            COUNT(*) AS Total
                            FROM
	                            sales_payment sp
                            WHERE
	                            sp.PaymentDate BETWEEN @DateFrom AND @DateTo
                            GROUP BY
	                            sp.Type
                            ORDER BY
	                            sp.Type ASC;
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
                                string type = result.GetString(result.GetOrdinal("Type"));
                                int total = result.GetInt32(result.GetOrdinal("Total"));
                                paymentByTypeDashboardData.Add(new PaymentByTypeContainer
                                {
                                    Type = type,
                                    Total = total
                                });
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
                    throw new DatabaseReadException("An error occurred while retrieving payment by type dashboard data", ex);
                }
            }

            return paymentByTypeDashboardData;
        }

        public async Task<ExpenseDashboardContainer> GetExpenseDashboardData(DateOnly dateFrom, DateOnly dateTo)
        {
            ExpenseDashboardContainer expenseDashboardData = new() { Amount = 0 };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            -- Expense
                            SELECT
	                            COALESCE(SUM(pp.Amount), 0) AS Pembelian
                            FROM
	                            purchase_payment pp
                            WHERE pp.PaymentDate BETWEEN @DateFrom AND @DateTo
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        expenseDashboardData.Amount = (decimal) (await command.ExecuteScalarAsync())!;
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("An error occurred while retrieving expense dashboard data", ex);
                }
            }

            return expenseDashboardData;
        }

        public async Task<IncomeDashboardContainer> GetIncomeDashboardData(DateOnly dateFrom, DateOnly dateTo)
        {
            IncomeDashboardContainer incomeDashboardData = new() { Amount = 0 };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            -- Income
                            SELECT
	                            COALESCE(SUM(sp.Amount), 0) AS Penjualan
                            FROM
	                            sales_payment sp
                            WHERE sp.PaymentDate BETWEEN @DateFrom AND @DateTo
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        incomeDashboardData.Amount = (decimal)(await command.ExecuteScalarAsync())!;
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("An error occurred while retrieving income dashboard data", ex);
                }
            }

            return incomeDashboardData;
        }

        public async Task<TotalOrderDashboardContainer> GetTotalOrderDashboardData(DateOnly dateFrom, DateOnly dateTo)
        {
            TotalOrderDashboardContainer totalOrderDashboardData = new() { Amount = 0 };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            -- Order Count
                            SELECT
	                            COUNT(*)
                            FROM
	                            sales_header sh
                            WHERE sh.SalesDate BETWEEN @DateFrom AND @DateTo
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        totalOrderDashboardData.Amount = (int)(await command.ExecuteScalarAsync())!;
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("An error occurred while retrieving total order dashboard data", ex);
                }
            }

            return totalOrderDashboardData;
        }

        public async Task<AccountReceivableDashboardContainer> GetAccountReceivableDashboardData(DateOnly dateFrom, DateOnly dateTo)
        {
            AccountReceivableDashboardContainer accountReceivableDashboardData = new() { Amount = 0 };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create a SQL command
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.Transaction = transaction.GetDbTransaction();
                        command.CommandText = @"
                            -- Account Receivable
                            SELECT
	                            CASE
		                            WHEN COALESCE(SUM(sales.Penjualan - sales.Pembayaran), 0) < 0 THEN 0
		                            ELSE COALESCE(SUM(sales.Penjualan - sales.Pembayaran), 0)
	                            END AS [Piutang]
                            FROM
	                            (
		                            -- Sales
		                            SELECT
			                            SUM(sd.Quantity * sd.Price) AS [Penjualan],
			                            0 AS Pembayaran
		                            FROM
			                            sales_header sh
		                            JOIN
			                            sales_detail sd ON sh.Id = sd.SalesHeaderId
		                            WHERE sh.SalesDate BETWEEN @DateFrom AND @DateTo

		                            UNION

		                            -- Sales Payment
		                            SELECT
			                            0 AS Penjualan,
			                            SUM(sp.Amount) AS [Pembayaran]
		                            FROM
			                            sales_header sh
		                            JOIN
			                            sales_payment sp ON sp.SalesHeaderId = sh.Id
		                            WHERE sp.PaymentDate BETWEEN @DateFrom AND @DateTo
	                            ) sales
                        ";

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@DateFrom", dateFrom));
                        command.Parameters.Add(new SqlParameter("@DateTo", dateTo));

                        // Open the connection
                        await _context.Database.OpenConnectionAsync();

                        // Execute the SQL query and read the results
                        accountReceivableDashboardData.Amount = (decimal)(await command.ExecuteScalarAsync())!;
                    }

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    // Rollback the transaction in case of an exception
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("An error occurred while retrieving account payable dashboard data", ex);
                }
            }

            return accountReceivableDashboardData;
        }
    }

    public class PaymentByTypeContainer
    {
        public string Type { get; set; } = default!;
        public int Total { get; set; }
    }

    public class ExpenseDashboardContainer
    {
        [Precision(19, 4)]
        public decimal Amount { get; set; }
    }

    public class IncomeDashboardContainer
    {
        [Precision(19, 4)]
        public decimal Amount { get; set; }
    }

    public class TotalOrderDashboardContainer
    {
        public int Amount { get; set; }
    }

    public class AccountReceivableDashboardContainer
    {
        [Precision(19, 4)]
        public decimal Amount { get; set; }
    }
}
