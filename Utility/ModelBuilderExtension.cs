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
            var idGuid = new Guid("effcd6d9-4e3a-441f-8f85-fede6cc03967");
            var sgGuid = new Guid("a03ef135-1a46-42d8-a0bf-427cf2e0b463");
            var myGuid = new Guid("16888cd6-6cac-4d2d-b8a4-f26d953cffe9");
            var usGuid = new Guid("ebbbe2c7-2089-46dd-904d-2ed01a0fc550");

            List<Currency> Currencies = new(4)
            {
                new Currency { Id = idGuid, Name = "Indonesia Rupiah", Code = "IDR", CultureName = "id-ID" },
                new Currency { Id = sgGuid, Name = "Dollar Singapore", Code = "SGD", CultureName = "en-SG" },
                new Currency { Id = myGuid, Name = "Ringgit Malaysia", Code = "MYR", CultureName = "ms-MY" },
                new Currency { Id = usGuid, Name = "Dollar USD", Code = "USD", CultureName = "en-US" },
            };
            modelBuilder.Entity<Currency>().HasData(Currencies);
        }
        public static void SeedChartOfAccount(this ModelBuilder modelBuilder)
        {
            var kasGuid = new Guid("913e2edf-4e3b-49fc-884c-41876f4bcdb3");
            var bankGuid = new Guid("87105a71-142b-4c4f-8568-b5ff60aee6eb");
            var persediaanGuid = new Guid("7993c1f9-0fcc-4ccf-b19c-2b4add5782bf");
            var perlengkapanGuid = new Guid("1fc549e7-c60f-4440-beeb-eb4d6d580992");
            var advanceSewaGuid = new Guid("573b0f53-ae55-4540-b22f-895f76605db7");
            var piutangGuid = new Guid("7fc23dc9-8ddc-4536-978b-6b4a833a8650");
            var peralatanGuid = new Guid("7a09d43a-8464-4652-bafb-daf8ed0a001c");
            var akumulasiDepresiasiGuid = new Guid("2585e6db-9f52-41f9-b022-6bac4ee39c4e");
            var utangGuid = new Guid("5014fce0-361a-4b9f-ad3e-2f680ede0030");
            var ekuitasGuid = new Guid("0faf503b-3842-4b38-b2ab-82a09a686810");
            var labaDitahanGuid = new Guid("8af02038-2ada-4c2e-899c-353e211049f5");
            var incomeSummaryGuid = new Guid("11ebb164-b8e0-4757-bd08-7cc409e8faf5");
            var salesIncomeGuid = new Guid("547b9264-083c-406e-b0e7-618f0241568b");
            var salesDiscountGuid = new Guid("ddc0e86d-bbb3-49dd-a982-c666f1d16575");
            var purchaseGuid = new Guid("77068633-1c07-46d0-b2e8-e919780693ea");
            var purchaseDiscountGuid = new Guid("2cc907e5-9f6b-4dee-a3ab-3b7699fe9dfe");
            var beginningInventoryGuid = new Guid("be3fa0e8-7f38-4a9d-82af-1da0a64d4c70");
            var endingInventoryGuid = new Guid("b1310f15-6de2-4119-9553-3080d4a660fb");
            var bebanGajiGuid = new Guid("15c09e8c-3439-4ca6-b63e-d0d3c2c2ddeb");
            var bebanSewaGuid = new Guid("40ffeb73-be96-4599-8f65-d2ebc9a26c0a");
            var bebanUtilitasGuid = new Guid("d8558de6-29a8-49cc-aea2-da282660cb43");
            var bebanListrikGuid = new Guid("f0a715d4-86ac-4db4-bd44-3561f8e5daaa");
            var bebanPerlengkapanGuid = new Guid("ebaa3442-0334-4581-9049-1cc80a600c59");
            var bebanDepresiasiGuid = new Guid("ed97eb75-10a0-4df6-8e6c-5c11d37ae2cb");

            modelBuilder.Entity<ChartOfAccount>().HasData(new List<ChartOfAccount>(20)
            {
                new ChartOfAccount { Id = kasGuid, AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 111, AccountName = "Kas" },
                new ChartOfAccount { Id = bankGuid, AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 112, AccountName = "Bank" },
                new ChartOfAccount { Id = persediaanGuid, AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 113, AccountName = "Persediaan" },
                new ChartOfAccount { Id = perlengkapanGuid, AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 114, AccountName = "Perlengkapan" },
                new ChartOfAccount { Id = advanceSewaGuid, AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 115, AccountName = "Sewa dibayar di muka" },
                new ChartOfAccount { Id = piutangGuid, AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 116, AccountName = "Piutang Dagang" },
                new ChartOfAccount { Id = peralatanGuid, AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 121, AccountName = "Peralatan" },
                new ChartOfAccount { Id = akumulasiDepresiasiGuid, AccountHeaderNo = 100, AccountHeaderName = "Asset", AccountNo = 122, AccountName = "Akumulasi Depresiasi - Mesin Cuci" },
                new ChartOfAccount { Id = utangGuid, AccountHeaderNo = 200, AccountHeaderName = "Liabilitas", AccountNo = 201, AccountName = "Utang Dagang" },
                new ChartOfAccount { Id = ekuitasGuid, AccountHeaderNo = 300, AccountHeaderName = "Ekuitas", AccountNo = 301, AccountName = "Ekuitas Pemilik Usaha" },
                new ChartOfAccount { Id = labaDitahanGuid, AccountHeaderNo = 300, AccountHeaderName = "Ekuitas", AccountNo = 310, AccountName = "Laba Ditahan" },
                new ChartOfAccount { Id = incomeSummaryGuid, AccountHeaderNo = 300, AccountHeaderName = "Ekuitas", AccountNo = 320, AccountName = "Ikhtisar Laba-Rugi" },
                new ChartOfAccount { Id = salesIncomeGuid, AccountHeaderNo = 400, AccountHeaderName = "Pendapatan", AccountNo = 401, AccountName = "Pendapatan Penjualan" },
                new ChartOfAccount { Id = salesDiscountGuid, AccountHeaderNo = 400, AccountHeaderName = "Pendapatan", AccountNo = 420, AccountName = "Potongan Penjualan" },
                new ChartOfAccount { Id = purchaseGuid, AccountHeaderNo = 500, AccountHeaderName = "Harga Pokok Penjualan", AccountNo = 501, AccountName = "Pembelian" },
                new ChartOfAccount { Id = purchaseDiscountGuid, AccountHeaderNo = 500, AccountHeaderName = "Harga Pokok Penjualan", AccountNo = 502 , AccountName = "Potongan Pembelian" },
                new ChartOfAccount { Id = beginningInventoryGuid, AccountHeaderNo = 500, AccountHeaderName = "Harga Pokok Penjualan", AccountNo = 511, AccountName = "Persediaan Awal" },
                new ChartOfAccount { Id = endingInventoryGuid, AccountHeaderNo = 500, AccountHeaderName = "Harga Pokok Penjualan", AccountNo = 521, AccountName = "Persediaan Akhir" },
                new ChartOfAccount { Id = bebanGajiGuid, AccountHeaderNo = 600, AccountHeaderName = "Pengeluaran", AccountNo = 601, AccountName = "Beban Gaji" },
                new ChartOfAccount { Id = bebanSewaGuid, AccountHeaderNo = 600, AccountHeaderName = "Pengeluaran", AccountNo = 602, AccountName = "Beban Sewa" },
                new ChartOfAccount { Id = bebanUtilitasGuid, AccountHeaderNo = 600, AccountHeaderName = "Pengeluaran", AccountNo = 603, AccountName = "Beban Utilitas" },
                new ChartOfAccount { Id = bebanListrikGuid, AccountHeaderNo = 600, AccountHeaderName = "Pengeluaran", AccountNo = 604, AccountName = "Beban Listrik" },
                new ChartOfAccount { Id = bebanPerlengkapanGuid, AccountHeaderNo = 600, AccountHeaderName = "Pengeluaran", AccountNo = 605, AccountName = "Beban Perlengkapan" },
                new ChartOfAccount { Id = bebanDepresiasiGuid, AccountHeaderNo = 600, AccountHeaderName = "Pengeluaran", AccountNo = 606, AccountName = "Beban Depresiasi" },
            });
        }
        public static void SeedLaundryServiceAndPriceMenu(this ModelBuilder modelBuilder)
        {
            var paket_bulanan_lengkap_guid = new Guid("6e3d879e-9804-47db-8550-fee599a81b5b");
            var paket_bulanan_setrika_guid = new Guid("6f02fd67-f4c3-4d73-8856-a3b3c68d9276");
            var satuan_guid = new Guid("69224789-77cf-44ac-b906-1d7c09868107");
            var sepatu_dan_tas_guid = new Guid("85c8f51f-9c9b-4cb7-85ea-742c6f3d22e7");
            var karpet_atau_gorden_guid = new Guid("521e5061-495f-4952-96c5-49fc24d27067");
            var kiloan_lengkap_guid = new Guid("d925808e-80db-4d69-9059-d2a4720cc0c8");
            var kiloan_setrika_atau_cuci_lipat_guid = new Guid("de1ce154-4c00-4f12-a593-a73634b10d67");
            var bed_cover_dan_selimut_guid = new Guid("23249048-50e4-446d-ae97-bb9993a3c09a");
            var sprei_dan_alas_kasur_guid = new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72");
            var bantal_atau_boneka_guid = new Guid("ce192b81-4fcb-4d98-b570-3c556585589a");

            modelBuilder.Entity<LaundryService>().HasData(new List<LaundryService>(10)
            {
                new LaundryService { Id = paket_bulanan_lengkap_guid, Name = "PAKET BULANAN LENGKAP", LaundryProcess = (ushort)(LaundryProcess.Wash | LaundryProcess.Dry | LaundryProcess.Iron) },
                new LaundryService { Id = paket_bulanan_setrika_guid, Name = "PAKET BULANAN SETRIKA", LaundryProcess = (ushort)(LaundryProcess.Iron) },
                new LaundryService { Id = satuan_guid, Name = "SATUAN", LaundryProcess = (ushort)(LaundryProcess.Wash | LaundryProcess.Dry) },
                new LaundryService { Id = sepatu_dan_tas_guid, Name = "SEPATU DAN TAS", LaundryProcess = (ushort)(LaundryProcess.Wash | LaundryProcess.Dry) },
                new LaundryService { Id = karpet_atau_gorden_guid, Name = "KARPET/GORDEN", LaundryProcess = (ushort)(LaundryProcess.Wash | LaundryProcess.Dry) },
                new LaundryService { Id = kiloan_lengkap_guid, Name = "KILOAN LENGKAP", LaundryProcess = (ushort)(LaundryProcess.Wash | LaundryProcess.Dry | LaundryProcess.Iron) },
                new LaundryService { Id = kiloan_setrika_atau_cuci_lipat_guid, Name = "KILOAN SETRIKA/CUCI LIPAT", LaundryProcess = (ushort)(LaundryProcess.Wash | LaundryProcess.Dry | LaundryProcess.Iron) },
                new LaundryService { Id = bed_cover_dan_selimut_guid, Name = "BED COVER & SELIMUT", LaundryProcess = (ushort)(LaundryProcess.Wash | LaundryProcess.Dry) },
                new LaundryService { Id = sprei_dan_alas_kasur_guid, Name = "SPREI & ALAS KASUR", LaundryProcess = (ushort)(LaundryProcess.Wash | LaundryProcess.Dry) },
                new LaundryService { Id = bantal_atau_boneka_guid, Name = "BANTAL/BONEKA", LaundryProcess = (ushort)(LaundryProcess.Wash | LaundryProcess.Dry) },
            });


            modelBuilder.Entity<PriceMenu>().HasData(new List<PriceMenu>(43)
            {
                new PriceMenu { LaundryServiceId = paket_bulanan_lengkap_guid, PriceMenuId = new Guid("222744c7-845a-4c8b-b023-1cd488965784"), Name = "25 Kgs", Price = 140_000, PricingOption = PricingOption.Package.ToString(), TimeUnit = TimeUnit.None.ToString(), DeliveryOption = DeliveryOption.None.ToString() },
                new PriceMenu { LaundryServiceId = paket_bulanan_lengkap_guid, PriceMenuId = new Guid("afbc2e8a-e324-4baf-994e-2703221d0f49"), Name = "50 Kgs", Price = 275_000, PricingOption = PricingOption.Package.ToString(), TimeUnit = TimeUnit.None.ToString(), DeliveryOption = DeliveryOption.None.ToString() },
                new PriceMenu { LaundryServiceId = paket_bulanan_lengkap_guid, PriceMenuId = new Guid("02f2aad2-e154-4454-9ea2-f71dc88b7896"), Name = "100 Kgs", Price = 550_000, PricingOption = PricingOption.Package.ToString(), TimeUnit = TimeUnit.None.ToString(), DeliveryOption = DeliveryOption.None.ToString() },

                new PriceMenu { LaundryServiceId = paket_bulanan_setrika_guid, PriceMenuId = new Guid("39d2e31c-fca5-4f44-8bf7-d0bec5027d79"), Name = "25 Kgs", Price = 120_000, PricingOption = PricingOption.Package.ToString(), TimeUnit = TimeUnit.None.ToString(), DeliveryOption = DeliveryOption.None.ToString() },
                new PriceMenu { LaundryServiceId = paket_bulanan_setrika_guid, PriceMenuId = new Guid("bd4cb094-193e-4712-bed6-02ef2d91b30f"), Name = "50 Kgs", Price = 240_000, PricingOption = PricingOption.Package.ToString(), TimeUnit = TimeUnit.None.ToString(), DeliveryOption = DeliveryOption.None.ToString() },
                new PriceMenu { LaundryServiceId = paket_bulanan_setrika_guid, PriceMenuId = new Guid("3330723b-eb73-439d-8bcd-4e7a11dfe760"), Name = "100 Kgs", Price = 475_000, PricingOption = PricingOption.Package.ToString(), TimeUnit = TimeUnit.None.ToString(), DeliveryOption = DeliveryOption.None.ToString() },
                

                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("10fc601c-c2e1-4b3d-9c59-6038981391b2"), Name = "Bawahan", Price = 10_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("00da1441-6fd1-46af-9d68-ae49cb00bc7f"), Name = "Atasan", Price = 10_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("e8663646-4523-42bc-9916-14c7788eb9dd"), Name = "Jas", Price = 20_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("ca3da966-ed51-42bc-8c37-698a8ca24b26"), Name = "Jas Set", Price = 30_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("a4c5c479-7364-406e-8acd-a761161293eb"), Name = "Blazer", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("81305acb-43e8-4245-b7e7-ac79b25a5e20"), Name = "Blazer Set", Price = 25_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("65f2160e-5977-4488-bc8c-98de3e3655f4"), Name = "Dress Panjang", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("1f70dd2e-b01d-4fb5-b573-4c86a18ec326"), Name = "Dress Pendek", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("994ef033-b02d-4452-83b6-e24c22294d91"), Name = "Jaket/Sweater", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("036a5843-d09a-4c6c-bd92-f7036e706857"), Name = "Handuk Besar", Price = 7_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = satuan_guid, PriceMenuId = new Guid("b1a8839a-9bde-450f-8eea-fbc7e1c0fc4d"), Name = "Handuk Sedang", Price = 6_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },


                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = new Guid("abd24f3c-8784-4329-af85-af5431cf830f"), Name = "Sepatu", Price = 25_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = new Guid("238f014a-e593-4d13-946b-8aebda6d6f86"), Name = "Tas Besar", Price = 30_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = new Guid("1a5a3143-a7a4-4335-be68-d4195601977f"), Name = "Tas Sedang", Price = 25_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = new Guid("1cc74945-64ee-4933-8147-1a01760e9c8a"), Name = "Tas Kecil", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sepatu_dan_tas_guid, PriceMenuId = new Guid("3e7ecae5-adf9-40f8-9d00-a11f25c30eee"), Name = "Tas Mini", Price = 10_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },


                new PriceMenu { LaundryServiceId = kiloan_lengkap_guid, PriceMenuId = new Guid("a7dfabc2-89e7-4d4e-b7db-9afadeb7ab39"), Name = "Reguler", Price = 6_000, PricingOption = PricingOption.Kilogram.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = kiloan_lengkap_guid, PriceMenuId = new Guid("2afa7cd8-c18b-4aab-b9be-3767a9ff2a2c"), Name = "One Day", Price = 8_000, PricingOption = PricingOption.Kilogram.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 1, DeliveryOption = DeliveryOption.OneDay.ToString() },
                new PriceMenu { LaundryServiceId = kiloan_lengkap_guid, PriceMenuId = new Guid("90cb34d3-a294-47a1-b39a-c21b3a740547"), Name = "Express", Price = 10_000, PricingOption = PricingOption.Kilogram.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },


                new PriceMenu { LaundryServiceId = kiloan_setrika_atau_cuci_lipat_guid, PriceMenuId = new Guid("a73c9ccf-1cd6-4b3b-adc5-b273112a7811"), Name = "Reguler", Price = 5_000, PricingOption = PricingOption.Kilogram.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 2, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = kiloan_setrika_atau_cuci_lipat_guid, PriceMenuId = new Guid("5c86719c-f26f-423a-bd22-de328af45222"), Name = "One Day", Price = 7_000, PricingOption = PricingOption.Kilogram.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 1, DeliveryOption = DeliveryOption.OneDay.ToString() },
                new PriceMenu { LaundryServiceId = kiloan_setrika_atau_cuci_lipat_guid, PriceMenuId = new Guid("90539d35-6fe7-460d-85bf-911c93588903"), Name = "Express", Price = 9_000, PricingOption = PricingOption.Kilogram.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },


                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("433ffecc-398f-43b7-9bb4-b48e77d15b76"), Name = "Bed Cover King Set", Price = 38_000, PricingOption = PricingOption.Set.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("e1ebbf40-6911-4178-b68a-1d24f7cb76ee"), Name = "Bed Cover Queen Set", Price = 35_000, PricingOption = PricingOption.Set.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("493d5a0f-6fa2-4815-a41c-3122274ccb97"), Name = "Bed Cover Single Set", Price = 27_000, PricingOption = PricingOption.Set.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("e3766c6c-6237-454b-a59d-f804daf81bc0"), Name = "Bed Cover King", Price = 25_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("3c8812fb-7759-4b2a-aa88-82a0b4116519"), Name = "Bed Cover Queen", Price = 20_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("f1d1ce2e-5555-481b-8a63-c1fec3b5da4d"), Name = "Bed Cover Single", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("20c0b667-f491-4aae-8374-e7ed5928f9c1"), Name = "Selimut", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("93e89b35-55fb-4069-b60f-1f55ad37ae60"), Name = "Selimut Tipis", Price = 10_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },


                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("79346961-e03e-49ce-a356-b5f836fa4555"), Name = "Bed Cover King Set", Price = 76_000, PricingOption = PricingOption.Set.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("1e00fa06-7de4-4b9d-94bc-00310f15a160"), Name = "Bed Cover Queen Set", Price = 70_000, PricingOption = PricingOption.Set.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("0761f843-b8ec-4f55-bf1f-64af6de3b417"), Name = "Bed Cover Single Set", Price = 54_000, PricingOption = PricingOption.Set.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("983a9b58-09f0-4d9f-b453-138ee6aa6cb5"), Name = "Bed Cover King", Price = 50_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("b678f1a9-7aed-402f-b348-88dee8901b8e"), Name = "Bed Cover Queen", Price = 40_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("aee4b233-74e4-41a2-988a-70c52ed50292"), Name = "Bed Cover Single", Price = 30_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("aa50318f-d0a2-4c30-8c0d-dc447807311e"), Name = "Selimut", Price = 30_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },
                new PriceMenu { LaundryServiceId = bed_cover_dan_selimut_guid, PriceMenuId = new Guid("ad7d2d80-2bee-4112-8029-88cdae7b730e"), Name = "Selimut Tipis", Price = 20_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Hour.ToString(), ProcessingTime = 6, DeliveryOption = DeliveryOption.Express.ToString() },


                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = new Guid("04f6d6d9-874f-4b2b-90cc-72450c767a42"), Name = "Sprei King Set", Price = 20_000, PricingOption = PricingOption.Set.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = new Guid("954df4a5-e9c8-4a05-b893-2e8a68da9171"), Name = "Sprei Queen Set", Price = 20_000, PricingOption = PricingOption.Set.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = new Guid("8b99aa71-ec4f-4d90-8dd5-8b1a19b92945"), Name = "Sprei Single Set", Price = 15_000, PricingOption = PricingOption.Set.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = new Guid("f38cbf47-f2ce-4f39-9bdb-2b769c1614c7"), Name = "Sprei King", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = new Guid("f8214070-d518-4965-a864-b0611169b4b7"), Name = "Sprei Queen", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = new Guid("915970c9-6e07-4101-8e82-bde2b935b454"), Name = "Sprei Single", Price = 10_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = new Guid("90e19c42-a89b-4a29-82bc-04066b19f35c"), Name = "Alas Kasur King", Price = 25_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = new Guid("cdf2af80-d63f-4585-8a41-200bcea47660"), Name = "Alas Kasur Queen", Price = 20_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
                new PriceMenu { LaundryServiceId = sprei_dan_alas_kasur_guid, PriceMenuId = new Guid("2a527b55-4947-448c-a56c-1afb523b09c0"), Name = "Alas Kasur Single", Price = 15_000, PricingOption = PricingOption.Unit.ToString(), TimeUnit = TimeUnit.Day.ToString(), ProcessingTime = 3, DeliveryOption = DeliveryOption.Reguler.ToString() },
            });
        }
        public static void SeedMenu(this ModelBuilder modelBuilder)
        {
            var dashboardCategoryGuid = new Guid("248f896d-1eed-49e4-b7bd-a0a0a37ffb1c");
            var masterDataCategoryGuid = new Guid("7aa59789-75a6-4414-9daf-b64c2fb3ebcd");
            var accountingCategoryGuid = new Guid("62cee0bc-e597-4f11-bb64-b9b1aa85f0a8");
            var purchaseCategoryGuid = new Guid("d694f409-4f9c-4692-a42f-2094e3b09008");
            var salesCategoryGuid = new Guid("3b37be0b-107b-474e-84db-7d66b4982799");
            var reportCategoryGuid = new Guid("c81512f7-e526-474d-8b52-04e2c7b8f75b");
            var configurationCategoryGuid = new Guid("b71ed8ec-6644-4f0e-862e-de4a383d36d7");


            modelBuilder.Entity<MenuCategory>().HasData(new List<MenuCategory>(7)
            {
                new MenuCategory { Id = dashboardCategoryGuid, CategorySequence = 10, CategoryName = "dashboard", CategoryDisplayName = "Dashboard" },
                new MenuCategory { Id = masterDataCategoryGuid, CategorySequence = 20, CategoryName = "master-data", CategoryDisplayName = "Master Data" },
                new MenuCategory { Id = accountingCategoryGuid, CategorySequence = 30, CategoryName ="accounting", CategoryDisplayName = "Accounting" },
                new MenuCategory { Id = purchaseCategoryGuid, CategorySequence = 40, CategoryName = "purchase", CategoryDisplayName = "Purchase" },
                new MenuCategory { Id = salesCategoryGuid, CategorySequence = 50, CategoryName = "sales", CategoryDisplayName = "Sales" },
                new MenuCategory { Id = reportCategoryGuid, CategorySequence = 60, CategoryName = "report", CategoryDisplayName = "Report" },
                new MenuCategory { Id = configurationCategoryGuid, CategorySequence = 1000, CategoryName = "configuration", CategoryDisplayName = "Configuration" },
            });

            modelBuilder.Entity<Menu>().HasData(new List<Menu>(21)
            {
                new Menu { Id = new Guid("7975f8b8-9f7a-4866-bf06-608daf33a4af"), MenuCategoryId = dashboardCategoryGuid, MenuSequence = 10, MenuName = "dashboard", MenuDisplayName = "Dashboard" },

                new Menu { Id = new Guid("3b616efc-ea0c-4fb8-848a-f1deec14e8c3"), MenuCategoryId = masterDataCategoryGuid, MenuSequence = 10, MenuName = "chart-of-account", MenuDisplayName = "Chart of Account" },
                new Menu { Id = new Guid("6c8f0943-2aa5-48fe-beb3-34b6f9928c99"), MenuCategoryId = masterDataCategoryGuid, MenuSequence = 20, MenuName = "customer", MenuDisplayName = "Pelanggan" },
                new Menu { Id = new Guid("e429e924-4275-4eff-8926-ce88c3b1dcd3"), MenuCategoryId = masterDataCategoryGuid, MenuSequence = 30, MenuName = "supplier", MenuDisplayName = "Pemasok" },
                new Menu { Id = new Guid("3c96056a-8e67-49d7-9496-e5c97ee1e09d"), MenuCategoryId = masterDataCategoryGuid, MenuSequence = 40, MenuName = "inventory", MenuDisplayName = "Persediaan" },
                new Menu { Id = new Guid("61a61cb6-6589-4efc-b2be-9a41f067cc32"), MenuCategoryId = masterDataCategoryGuid, MenuSequence = 50, MenuName = "laundry-service", MenuDisplayName = "Tipe Laundry" },
                //new Menu { Id = new Guid("c200839f-7af3-45c8-91bb-8b0ab7ed66a9"), MenuCategoryId = masterDataCategoryGuid, MenuSequence = 60, MenuName = "price-menu", MenuDisplayName = "Menu Harga" },

                new Menu { Id = new Guid("32581d67-ddf0-4a97-aabe-ea486bca3e97"), MenuCategoryId = accountingCategoryGuid, MenuSequence = 10, MenuName = "journal-entry", MenuDisplayName = "Journal Entries" },
                new Menu { Id = new Guid("f3bc99da-2dfb-46d9-812c-57c301058d2c"), MenuCategoryId = accountingCategoryGuid, MenuSequence = 100, MenuName = "closing-entry", MenuDisplayName = "Closing Entries" },

                new Menu { Id = new Guid("2fb96518-bfb9-4d54-89a4-45a50698c905"), MenuCategoryId = purchaseCategoryGuid, MenuSequence = 10, MenuName = "purchase", MenuDisplayName = "Pembelian" },
                new Menu { Id = new Guid("19c7a7d5-811e-4b2a-97e1-e39e791deb78"), MenuCategoryId = purchaseCategoryGuid, MenuSequence = 20, MenuName = "purchase-payment", MenuDisplayName = "Pembayaran Pembelian" },

                new Menu { Id = new Guid("37c9a181-582c-4dde-9217-98d4dffc7700"), MenuCategoryId = salesCategoryGuid, MenuSequence = 10, MenuName = "sales", MenuDisplayName = "Penjualan" },
                new Menu { Id = new Guid("38804e0c-3e19-4044-8229-bb2026970a4d"), MenuCategoryId = salesCategoryGuid, MenuSequence = 20, MenuName = "sales-payment", MenuDisplayName = "Pembayaran Penjualan" },

                new Menu { Id = new Guid("73d0eb70-ccf3-4ae8-9c1c-c157839c8bbe"), MenuCategoryId = reportCategoryGuid, MenuSequence = 10, MenuName = "general-ledger", MenuDisplayName = "Buku Besar" },
                new Menu { Id = new Guid("2eb6b50f-f53d-4924-a49d-1effae422e64"), MenuCategoryId = reportCategoryGuid, MenuSequence = 20, MenuName = "trial-balance", MenuDisplayName = "Neraca Saldo" },
                new Menu { Id = new Guid("a282abe9-3b20-4bb1-b6d9-444d4c1294b4"), MenuCategoryId = reportCategoryGuid, MenuSequence = 30, MenuName = "income-statement", MenuDisplayName = "Laba Rugi" },
                new Menu { Id = new Guid("42c2d2cd-3f56-4bc0-840e-8769856edc0f"), MenuCategoryId = reportCategoryGuid, MenuSequence = 40, MenuName = "balance-sheet", MenuDisplayName = "Posisi Keuangan" },
                new Menu { Id = new Guid("51ef7e8c-4913-4ebe-a6d0-5dd72ec24c19"), MenuCategoryId = reportCategoryGuid, MenuSequence = 50, MenuName = "report-purchase", MenuDisplayName = "Pembelian" },
                new Menu { Id = new Guid("1878d7e6-fce4-4074-8796-3d814f4cd444"), MenuCategoryId = reportCategoryGuid, MenuSequence = 60, MenuName = "report-sales", MenuDisplayName = "Penjualan" },

                new Menu { Id = new Guid("0906c674-b075-4f2d-b54f-43236c9d03ea"), MenuCategoryId = configurationCategoryGuid, MenuSequence = 10, MenuName = "company", MenuDisplayName = "Perusahaan" },
                new Menu { Id = new Guid("598b71b8-338e-4fa2-95ed-b3f97cfcd252"), MenuCategoryId = configurationCategoryGuid, MenuSequence = 100, MenuName = "message-template", MenuDisplayName = "Template WhatsApp" },

            });
        }

        public static void SeedCompany(this ModelBuilder modelBuilder)
        {
            var companyGuid = new Guid("040731b6-c2ce-4842-b134-9a51746f2ca9");

            modelBuilder.Entity<Company>().HasData(new List<Company>(1)
            {
                new Company
                {   Id = companyGuid,
                    Name = "BLUE WASH",
                    Address = "Ruko Puri Loka Blok E No. 5, Sei Panas, Kec. Batam Kota, Kota Batam, Kepulauan Riau 29411",
                    MobileNumber = "+6282283025500"
                }
            });

        }
    }
}
