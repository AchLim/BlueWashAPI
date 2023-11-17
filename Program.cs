using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using Book_Lending_System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebAPI.OptionsSetup;
using WebAPI.Authentication;
using WebAPI.Exception;
using WebAPI.DAL;
using Serilog;

Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();

try
{
    Log.Information("Starting web application...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    var AllowedOrigins = "_AllowedOrigins";
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: AllowedOrigins, policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
                  .WithHeaders("Content-Type", "Authorization")
                  .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                  .AllowCredentials();
        });
    });

    builder.Services.AddCookiePolicy(options =>
    {
        options.MinimumSameSitePolicy = SameSiteMode.None;
        options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
        options.Secure = CookieSecurePolicy.Always;

    });

    builder.Services.AddControllers(options =>
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

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddDbContext<ApplicationContext>((sp, options) =>
    {
        //var auditableInterceptor = sp.GetService<UpdateEntityInterceptor>()!;

        options.UseSqlServer(builder.Configuration.GetConnectionString("AppDatabase")
                            ?? throw new InvalidOperationException("Database 'AppDatabase' tidak ditemukan!")
                            );
                //.AddInterceptors(auditableInterceptor)
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new()
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
    builder.Services.AddAuthorization();

    builder.Services.AddTransient<IJwtProvider, JwtProvider>();
    builder.Services.ConfigureOptions<JwtOptionsSetup>();
    builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

    builder.Services.AddScoped<IAccessRightRepository, AccessRightRepository>();
    builder.Services.AddScoped<ILaundryServiceRepository, LaundryServiceRepository>();
    builder.Services.AddScoped<IPriceMenuRepository, PriceMenuRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
    builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
    builder.Services.AddScoped<IChartOfAccountRepository, ChartOfAccountRepository>();
    builder.Services.AddScoped<ISalesRepository, SalesRepository>();
    builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
    builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
    builder.Services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();


    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseCors(AllowedOrigins);
    app.UseCookiePolicy();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
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

    app.MapControllers();


    using (var scope = app.Services.CreateAsyncScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = services.GetRequiredService<ApplicationContext>();
            await ContextSeed.SeedRolesAsync(context);
            await ContextSeed.SeedUsersAsync(context);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured when seeding the database.");
        }
    }

    app.Run();
}

catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}