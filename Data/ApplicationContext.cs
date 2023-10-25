using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedCurrency();
            modelBuilder.SeedChartOfAccount();

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
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
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

        #endregion
    }
}
