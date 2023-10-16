using Microsoft.EntityFrameworkCore;
using WebAPI.DAL;
using WebAPI.Data;
using WebAPI.Interceptors;
using WebAPI.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var AllowedOrigins = "_AllowedOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowedOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.WithHeaders(new[] { "Content-Type" });
    });
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationContext>((sp, options) =>
{
    //var auditableInterceptor = sp.GetService<UpdateEntityInterceptor>()!;

    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDatabase")
                        ?? throw new InvalidOperationException("Database 'AppDatabase' tidak ditemukan!")
                        )
            //.AddInterceptors(auditableInterceptor)
            .EnableSensitiveDataLogging();
});

//builder.Services.AddSingleton<UpdateEntityInterceptor>();
builder.Services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(AllowedOrigins);

app.Run();
