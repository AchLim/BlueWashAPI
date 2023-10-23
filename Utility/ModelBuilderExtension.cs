using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Utility
{
    public static class ModelBuilderExtension
    {
        public static void SeedCurrency(this ModelBuilder modelBuilder)
        {
            //Span<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures).AsSpan();
            //List<Currency> Currencies = new(cultures.Length);

            //foreach (var culture in cultures)
            //{
            //    if (culture.LCID == 0x1000)
            //        continue;

            //    RegionInfo region = new(culture.LCID);

            //    var name = region.EnglishName;
            //    var code = region.ISOCurrencySymbol;
            //    var cultureName = culture.Name;

            //    Currencies.Add(new Currency
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = name,
            //        Code = code,
            //        CultureName = cultureName
            //    });
            //}

            List<Currency> Currencies = new(4)
            {
                new Currency { Id = Guid.NewGuid(), Name = "Indonesia Rupiah", Code = "IDR", CultureName = "id-ID" },
                new Currency { Id = Guid.NewGuid(), Name = "Dollar Singapore", Code = "SGD", CultureName = "en-SG" },
                new Currency { Id = Guid.NewGuid(), Name = "Ringgit Malaysia", Code = "MYR", CultureName = "ms-MY" },
                new Currency { Id = Guid.NewGuid(), Name = "Dollar USD", Code = "USD", CultureName = "en-US" },
            };
            modelBuilder.Entity<Currency>().HasData(Currencies);
        }
        public static void SeedChartOfAccount(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChartOfAccount>().HasData(new List<ChartOfAccount>(15)
            {
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 111, AccountName = "Kas" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 112, AccountName = "Bank" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 113, AccountName = "Persediaan" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 114, AccountName = "Perlengkapan" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 115, AccountName = "Sewa dibayar di muka" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 121, AccountName = "Peralatan" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 122, AccountName = "Akumulasi Depresiasi - Mesin Cuci" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 200, AccountHeaderName = "Liabilitas", AccountNo = 201, AccountName = "Utang Usaha" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 300, AccountHeaderName = "Ekuitas", AccountNo = 301, AccountName = "Ekuitas Pemilik Usaha" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 400, AccountHeaderName = "Pendapatan", AccountNo = 401, AccountName = "Pendapatan Penjualan" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 500, AccountHeaderName = "Pengeluaran", AccountNo = 501, AccountName = "Beban Gaji" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 500, AccountHeaderName = "Pengeluaran", AccountNo = 502, AccountName = "Beban Sewa" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 500, AccountHeaderName = "Pengeluaran", AccountNo = 503, AccountName = "Beban Utilitas" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 500, AccountHeaderName = "Pengeluaran", AccountNo = 504, AccountName = "Beban Listrik" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 500, AccountHeaderName = "Pengeluaran", AccountNo = 505, AccountName = "Beban Perlengkapan" },
                new ChartOfAccount { Id = Guid.NewGuid(), AccountHeaderNo = 500, AccountHeaderName = "Pengeluaran", AccountNo = 506, AccountName = "Beban Depresiasi" },
            });
        }
    }
}
