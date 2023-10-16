using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using System.Globalization;
using System.Runtime.InteropServices;

namespace WebAPI.Utility
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Span<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures).AsSpan();
            List<Currency> Currencies = new(cultures.Length);

            foreach (var culture in cultures)
            {
                if (culture.LCID == 0x1000)
                    continue;

                RegionInfo region = new(culture.LCID);

                var name = region.EnglishName;
                var code = region.ISOCurrencySymbol;
                var cultureName = culture.Name;

                Currencies.Add(new Currency
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Code = code,
                    CultureName = cultureName
                });

            }

            modelBuilder.Entity<Currency>().HasData(Currencies);
        }
    }
}
