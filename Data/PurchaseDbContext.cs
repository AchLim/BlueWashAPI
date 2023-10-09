using Microsoft.EntityFrameworkCore;
using PurchaseAPI.Models;

namespace PurchaseAPI.Data
{
    public class PurchaseDbContext : DbContext
    {

        public PurchaseDbContext(DbContextOptions<PurchaseDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bank>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<BankAccount>().HasIndex(ba => ba.AccountNumber).IsUnique();
            modelBuilder.Entity<Currency>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Currency>().HasIndex(c => c.Abbreviation).IsUnique();
            modelBuilder.Entity<UnitOfMeasure>().HasIndex(uom => uom.Name).IsUnique();

            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(p => p.PurchaseOrderLines)
                .WithOne(pl => pl.PurchaseOrder)
                .HasForeignKey(pl => pl.PurchaseOrderId);

        }

        public DbSet<Bank> Banks { get; set; } = default!;
        public DbSet<BankAccount> BankAccounts { get; set; } = default!;
        public DbSet<Currency> Currencies { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; } = default!;
        public DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; } = default!;
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; } = default!;
        public DbSet<Vendor> Vendors { get; set; } = default!;
    }
}
