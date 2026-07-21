using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Models.Common;
using WebAPI.Utility;

namespace WebAPI.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContext;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor httpContext) : base(options)
        {
            _httpContext = httpContext;
        }

        #region Override Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseExceptionProcessor();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedCurrency();
            modelBuilder.SeedChartOfAccount();
            modelBuilder.SeedLaundryServiceAndPriceMenu();
            modelBuilder.SeedMenu();
            modelBuilder.SeedCompany();

            modelBuilder.Entity<JournalEntry>()
                        .HasOne(je => je.ClosingEntry)
                        .WithMany(ce => ce.JournalEntries)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<SalesDetail>()
                        .HasOne(sd => sd.PriceMenu)
                        .WithMany(pm => pm.SalesDetails)
                        .HasForeignKey(sd => new { sd.LaundryServiceId, sd.PriceMenuId })
                        .IsRequired()
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<PurchasePayment>()
                        .ToTable(tb => tb.HasTrigger("trgUpdatePurchaseHeaderPaymentStatus"));

            modelBuilder.Entity<SalesPayment>()
                        .ToTable(tb => tb.HasTrigger("trgUpdateSalesHeaderPaymentStatus"));

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
            var currentTime = DateTime.UtcNow;
            string? userName = string.Empty;
            if (_httpContext.HttpContext is not null && _httpContext.HttpContext.User.Claims is not null)
            {
                IEnumerable<Claim> claims = _httpContext.HttpContext.User.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == JwtRegisteredClaimNames.Name)
                    {
                        userName = claim.Value;
                    }
                }
            }

            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = currentTime;
                    entry.Entity.CreatedBy = userName;
                }

                entry.Entity.LastModified = currentTime;
                entry.Entity.LastModifiedBy = userName;
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
       
        #endregion

        #region Database Sets

        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<ApplicationUser> Users { get; set; } = default!;
        public DbSet<ApplicationRole> Roles { get; set; } = default!;
        public DbSet<ApplicationUserRole> UserRoles { get; set; } = default!;
        public DbSet<MenuCategory> MenuCategories { get; set; } = default!;
        public DbSet<Menu> Menus { get; set; } = default!;
        public DbSet<UserMenu> UserMenus { get; set; } = default!;
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; } = default!;
        public DbSet<Currency> Currencies { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<JournalItem> JournalItems { get; set; } = default!;
        public DbSet<ClosingEntry> ClosingEntries { get; set; } = default!;
        public DbSet<JournalEntry> JournalEntries { get; set; } = default!;
        public DbSet<Inventory> Inventories { get; set; } = default!;
        public DbSet<PurchasePayment> PurchasePayments { get; set; } = default!;
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; } = default!;
        public DbSet<PurchaseHeader> PurchaseHeaders { get; set; } = default!;
        public DbSet<SalesDetail> SalesDetails { get; set; } = default!;
        public DbSet<SalesHeader> SalesHeaders { get; set; } = default!;
        public DbSet<SalesPayment> SalesPayments { get; set; } = default!;
        public DbSet<MessageTemplate> MessageTemplates { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; } = default!;
        public DbSet<LaundryService> LaundryServices { get; set; } = default!;
        public DbSet<PriceMenu> PriceMenus { get; set; } = default!;

        #endregion
    }
}
