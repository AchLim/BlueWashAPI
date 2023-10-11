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

            modelBuilder.Entity<Receipt>()
                .HasMany(p => p.ReceiptLines)
                .WithOne(pl => pl.Receipt)
                .HasForeignKey(pl => pl.ReceiptId);

        }

        public DbSet<Bank> Banks { get; set; } = default!;
        public DbSet<BankAccount> BankAccounts { get; set; } = default!;
        public DbSet<Currency> Currencies { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Receipt> Receipts { get; set; } = default!;
        public DbSet<ReceiptLine> ReceiptLines { get; set; } = default!;
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; } = default!;
        public DbSet<Vendor> Vendors { get; set; } = default!;
    }
}
