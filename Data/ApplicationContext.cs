using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Models.Common;
using WebAPI.Utility;

namespace WebAPI.Data
{
    public class ApplicationContext : DbContext, IUnitOfWork
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditableEntity>().UseTpcMappingStrategy();

            modelBuilder.Entity<AuditableEntity>()
                        .Property(a => a.Created).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<AuditableEntity>()
                        .Property(a => a.LastModified).ValueGeneratedOnAddOrUpdate()
                        .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Save);

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateOnly>()
                                .HaveConversion<DateOnlyConverter>()
                                .HaveColumnType("date");

            configurationBuilder.Properties<TimeOnly>()
                                .HaveConversion<TimeOnlyConverter>()
                                .HaveColumnType("time");

            base.ConfigureConventions(configurationBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private IDbContextTransaction? _currentTransaction = null;
        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null!;

            _currentTransaction = await Database.BeginTransactionAsync();

            return _currentTransaction;
        }

        public async Task CommitAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null!;
                }
            }
        }

        private void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null!;
                }
            }
        }

        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; } = default!;
        public DbSet<Currency> Currencies { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<GeneralAccountDetail> GeneralAccountDetails { get; set; } = default!;
        public DbSet<GeneralAccountHeader> GeneralAccountHeaders { get; set; } = default!;
        public DbSet<Inventory> Inventories { get; set; } = default!;
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; } = default!;
        public DbSet<PurchaseHeader> PurchaseHeaders { get; set; } = default!;
        public DbSet<SalesDetail> SalesDetails { get; set; } = default!;
        public DbSet<SalesHeader> SalesHeaders { get; set; } = default!;
        public DbSet<SalesPayment> SalesPayments { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; } = default!;
    }
}
