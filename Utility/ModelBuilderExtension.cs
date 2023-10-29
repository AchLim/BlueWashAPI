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


        public static void SeedLaundryServiceAndPriceMenu(this ModelBuilder modelBuilder)
        {
            var paket_bulanan_lengkap_guid = Guid.NewGuid();
            var paket_bulanan_setrika_guid = Guid.NewGuid();
            var satuan_guid = Guid.NewGuid();
            var sepatu_dan_tas_guid = Guid.NewGuid();
            var karpet_atau_gorden_guid = Guid.NewGuid();
            var kiloan_lengkap_guid = Guid.NewGuid();
            var kiloan_setrika_atau_cuci_lipat_guid = Guid.NewGuid();
            var bed_cover_dan_selimut_guid = Guid.NewGuid();
            var sprei_dan_alas_kasur_guid = Guid.NewGuid();
            var bantal_atau_boneka_guid = Guid.NewGuid();

            modelBuilder.Entity<LaundryService>().HasData(new List<LaundryService>(10)
            {
                new LaundryService { Id = paket_bulanan_lengkap_guid, Name = "PAKET BULANAN LENGKAP", LaundryProcess = LaundryProcess.Wash | LaundryProcess.Dry | LaundryProcess.Iron },
                new LaundryService { Id = paket_bulanan_setrika_guid, Name = "PAKET BULANAN SETRIKA", LaundryProcess = LaundryProcess.Iron },
                new LaundryService { Id = satuan_guid, Name = "SATUAN", LaundryProcess = LaundryProcess.Wash | LaundryProcess.Dry },
                new LaundryService { Id = sepatu_dan_tas_guid, Name = "SEPATU DAN TAS", LaundryProcess = LaundryProcess.Wash | LaundryProcess.Dry },
                new LaundryService { Id = karpet_atau_gorden_guid, Name = "KARPET/GORDEN", LaundryProcess = LaundryProcess.Wash | LaundryProcess.Dry },
                new LaundryService { Id = kiloan_lengkap_guid, Name = "KILOAN LENGKAP", LaundryProcess = LaundryProcess.Wash | LaundryProcess.Dry | LaundryProcess.Iron },
                new LaundryService { Id = kiloan_setrika_atau_cuci_lipat_guid, Name = "KILOAN SETRIKA/CUCI LIPAT", LaundryProcess = LaundryProcess.Wash | LaundryProcess.Dry | LaundryProcess.Iron },
                new LaundryService { Id = bed_cover_dan_selimut_guid, Name = "BED COVER & SELIMUT", LaundryProcess = LaundryProcess.Wash | LaundryProcess.Dry },
                new LaundryService { Id = sprei_dan_alas_kasur_guid, Name = "SPREI & ALAS KASUR", LaundryProcess = LaundryProcess.Wash | LaundryProcess.Dry },
                new LaundryService { Id = bantal_atau_boneka_guid, Name = "BANTAL/BONEKA", LaundryProcess = LaundryProcess.Wash | LaundryProcess.Dry },
            });


            modelBuilder.Entity<PriceMenu>().HasData(new List<PriceMenu>(43)
            {
                new PriceMenu { LaundryServiceId = paket_bulanan_lengkap_guid, PriceMenuId = Guid.NewGuid(), Name = "25 Kgs", Price = 140_000, PricingOption = PricingOption.Package, TimeUnit = TimeUnit.None, DeliveryOption = DeliveryOption.None },
                new PriceMenu { LaundryServiceId = paket_bulanan_lengkap_guid, PriceMenuId = Guid.NewGuid(), Name = "50 Kgs", Price = 275_000, PricingOption = PricingOption.Package, TimeUnit = TimeUnit.None, DeliveryOption = DeliveryOption.None },
                new PriceMenu { LaundryServiceId = paket_bulanan_lengkap_guid, PriceMenuId = Guid.NewGuid(), Name = "100 Kgs", Price = 550_000, PricingOption = PricingOption.Package, TimeUnit = TimeUnit.None, DeliveryOption = DeliveryOption.None },

                new PriceMenu { LaundryServiceId = paket_bulanan_setrika_guid, PriceMenuId = Guid.NewGuid(), Name = "25 Kgs", Price = 120_000, PricingOption = PricingOption.Package, TimeUnit = TimeUnit.None, DeliveryOption = DeliveryOption.None },
                new PriceMenu { LaundryServiceId = paket_bulanan_setrika_guid, PriceMenuId = Guid.NewGuid(), Name = "50 Kgs", Price = 240_000, PricingOption = PricingOption.Package, TimeUnit = TimeUnit.None, DeliveryOption = DeliveryOption.None },
                new PriceMenu { LaundryServiceId = paket_bulanan_setrika_guid, PriceMenuId = Guid.NewGuid(), Name = "100 Kgs", Price = 475_000, PricingOption = PricingOption.Package, TimeUnit = TimeUnit.None, DeliveryOption = DeliveryOption.None },
                

                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Bawahan", Price = 10_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Atasan", Price = 10_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Jas", Price = 20_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Jas Set", Price = 30_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Blazer", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Blazer Set", Price = 25_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Dress Panjang", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Dress Pendek", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Jaket/Sweater", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Handuk Besar", Price = 7_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = Guid.NewGuid(), Name = "Handuk Sedang", Price = 6_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },


                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = Guid.NewGuid(), Name = "Sepatu", Price = 25_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = Guid.NewGuid(), Name = "Tas Besar", Price = 30_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = Guid.NewGuid(), Name = "Tas Sedang", Price = 25_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = Guid.NewGuid(), Name = "Tas Kecil", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = Guid.NewGuid(), Name = "Tas Mini", Price = 10_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },


                new PriceMenu { LaundryServiceId = kiloan_lengkap_guid, PriceMenuId = Guid.NewGuid(), Name = "Reguler", Price = 6_000, PricingOption = PricingOption.Kilogram, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = kiloan_lengkap_guid, PriceMenuId = Guid.NewGuid(), Name = "One Day", Price = 8_000, PricingOption = PricingOption.Kilogram, TimeUnit = TimeUnit.Day, ProcessingTime = 1, DeliveryOption = DeliveryOption.OneDay },
                new PriceMenu { LaundryServiceId = kiloan_lengkap_guid, PriceMenuId = Guid.NewGuid(), Name = "Express", Price = 10_000, PricingOption = PricingOption.Kilogram, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },


                new PriceMenu { LaundryServiceId = kiloan_setrika_atau_cuci_lipat_guid, PriceMenuId = Guid.NewGuid(), Name = "Reguler", Price = 5_000, PricingOption = PricingOption.Kilogram, TimeUnit = TimeUnit.Day, ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = kiloan_setrika_atau_cuci_lipat_guid, PriceMenuId = Guid.NewGuid(), Name = "One Day", Price = 7_000, PricingOption = PricingOption.Kilogram, TimeUnit = TimeUnit.Day, ProcessingTime = 1, DeliveryOption = DeliveryOption.OneDay },
                new PriceMenu { LaundryServiceId = kiloan_setrika_atau_cuci_lipat_guid, PriceMenuId = Guid.NewGuid(), Name = "Express", Price = 9_000, PricingOption = PricingOption.Kilogram, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },


                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover King Set", Price = 38_000, PricingOption = PricingOption.Set, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover Queen Set", Price = 35_000, PricingOption = PricingOption.Set, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover Single Set", Price = 27_000, PricingOption = PricingOption.Set, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover King", Price = 25_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover Queen", Price = 20_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover Single", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Selimut", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Selimut Tipis", Price = 10_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },


                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover King Set", Price = 76_000, PricingOption = PricingOption.Set, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover Queen Set", Price = 70_000, PricingOption = PricingOption.Set, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover Single Set", Price = 54_000, PricingOption = PricingOption.Set, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover King", Price = 50_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover Queen", Price = 40_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Bed Cover Single", Price = 30_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Selimut", Price = 30_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = Guid.NewGuid(), Name = "Selimut Tipis", Price = 20_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Hour, ProcessingTime = 6, DeliveryOption = DeliveryOption.Express },


                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = Guid.NewGuid(), Name = "Sprei King Set", Price = 20_000, PricingOption = PricingOption.Set, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = Guid.NewGuid(), Name = "Sprei Quen Set", Price = 20_000, PricingOption = PricingOption.Set, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = Guid.NewGuid(), Name = "Sprei Single Set", Price = 15_000, PricingOption = PricingOption.Set, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = Guid.NewGuid(), Name = "Sprei King", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = Guid.NewGuid(), Name = "Sprei Queen", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = Guid.NewGuid(), Name = "Sprei Single", Price = 10_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = Guid.NewGuid(), Name = "Alas Kasur King", Price = 25_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = Guid.NewGuid(), Name = "Alas Kasur Queen", Price = 20_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = Guid.NewGuid(), Name = "Alas Kasur Single", Price = 15_000, PricingOption = PricingOption.Unit, TimeUnit = TimeUnit.Day, ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler },
            });
        }

    }
}
