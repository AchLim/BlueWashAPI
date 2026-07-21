using Book_Lending_System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WebAPI.Authentication;
using WebAPI.DAL;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.OptionsSetup;

namespace WebAPI;

public class Startup
{
    public IConfiguration Configuration { get; }
    private static readonly string allowedOrigin = "_AllowedOrigins";
    public Startup (IConfiguration configuration)
    {
        Configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: allowedOrigin, policy =>
            {
                //policy.AllowAnyOrigin()
                policy.SetIsOriginAllowed(_ => true)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        services.AddCookiePolicy(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
            options.Secure = CookieSecurePolicy.Always;
        });

        services.AddControllers(options =>
        {
            options.Filters.Add<UserNotFoundExceptionFilter>();
            options.Filters.Add<DatabaseReadExceptionFilter>();
            options.Filters.Add<DatabaseInsertExceptionFilter>();
            options.Filters.Add<DatabaseUpdateExceptionFilter>();
            options.Filters.Add<DatabaseDeleteExceptionFilter>();
            options.Filters.Add<DatabaseUniqueConstraintExceptionFilter>();
            options.Filters.Add<UnauthorizedAccessExceptionFilter>();
            options.Filters.Add<InvalidDataExceptionFilter>();

        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

        services.AddHttpContextAccessor();
        services.AddDbContext<ApplicationContext>((options) =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DB_CONNECTION_STRING"));
            options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        services.AddAuthorization();

        services.AddTransient<IJwtProvider, JwtProvider>();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddScoped<IMessageTemplateRepository, MessageTemplateRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IAccessRightRepository, AccessRightRepository>();
        services.AddScoped<ILaundryServiceRepository, LaundryServiceRepository>();
        services.AddScoped<IPriceMenuRepository, PriceMenuRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IChartOfAccountRepository, ChartOfAccountRepository>();
        services.AddScoped<ISalesRepository, SalesRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IClosingEntryRepository, ClosingEntryRepository>();
        services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSerilogRequestLogging();
        app.UseCors(allowedOrigin);
        app.UseCookiePolicy();

        app.UseHttpsRedirection();
        app.UseRouting();

        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
            await next();
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });


        using (var scope = app.ApplicationServices.CreateAsyncScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                using (var context = services.GetRequiredService<ApplicationContext>())
                {
                    await ContextSeed.SeedRolesAsync(context);
                    await ContextSeed.SeedUsersAsync(context);
                }
            }
            catch (System.Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occured when seeding the database.");
            }
        }
    }

}