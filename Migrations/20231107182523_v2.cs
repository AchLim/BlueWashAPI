using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("01315a51-bbc7-4e22-bfb0-abce26d2607c"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("2d4d1231-267f-4f8d-9967-0c06718eac09"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("3c257b8b-10b0-46c6-b63f-71296619cb65"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("4641765c-ee78-4a2f-82e7-4574eb1dab0d"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("531c171b-4b4a-4ca3-8a3b-db705903b051"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("57060c37-e3f1-4871-b565-df83f354edaa"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("5b6e2813-bbbc-497e-a19a-d914377514b2"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("5c168ca1-33bb-43ef-aeb3-6c8989d9fd2b"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("6384d92c-cd73-4219-86b0-a653f5b72adc"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("b3433c9f-5092-42de-a819-d08c13e2c786"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("d213010c-ce6c-4712-9699-fb504986d9e9"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("d63d4f83-52fd-40bb-bd0b-b0f60248b282"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("d9fb8c52-1982-4aa3-96aa-3d9f98d220d4"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("e7decc39-8496-4f8e-98ae-12ccf533f61b"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("ec6d2e6b-009b-4824-aa5a-1f014455b6d6"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("fa87aab7-a87f-4ee0-bc51-9b7df8247f33"));

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "Id",
                keyValue: new Guid("1b97d48f-1ce4-4d08-93e0-96dc1fdd1c9f"));

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "Id",
                keyValue: new Guid("2b1b149c-93b5-485d-bc6e-26167e0ac899"));

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "Id",
                keyValue: new Guid("4438c3b5-093f-4a58-a112-b91004431915"));

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "Id",
                keyValue: new Guid("b97e9501-e2c9-4273-8fea-f437d21ef75c"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("43896f0e-231d-4765-8700-24c8422033e0"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("c6b1f038-dcda-41bf-9ecb-ce213ea84ab6"));

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("17316939-27da-4072-9e9f-151f1dcbbf1e"), new Guid("6b1a7b00-24fc-4fce-843e-dcf3360d5a61") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("17316939-27da-4072-9e9f-151f1dcbbf1e"), new Guid("b41e6970-585c-490d-beb5-0c778d5a82fd") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("17316939-27da-4072-9e9f-151f1dcbbf1e"), new Guid("bcb2c904-25e0-4430-b015-3863bcbfc745") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("24f4688c-0696-4cbc-b9bf-beebf045cd01"), new Guid("334e12eb-27f1-4884-b477-c9251d9d80f8") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("24f4688c-0696-4cbc-b9bf-beebf045cd01"), new Guid("78cee24c-15c3-479e-9d69-5898f39bc780") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("24f4688c-0696-4cbc-b9bf-beebf045cd01"), new Guid("c086f307-e7ba-42b4-b4fc-9ee88a66833d") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("05baa46f-a786-412b-afba-83fcf32b7524") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("185ab6b1-5920-49c8-9f19-0d2e2d52e5ab") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("519a7bea-2a73-4074-a6d3-1060545265e4") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("6e171c25-7730-4764-83a5-7f84e95e99a9") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("77789a6e-cc71-44bf-bee1-6e3d1bdf5b4a") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("9e330b40-4a53-45ae-8fff-501c6d5f5196") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("9f930088-e479-4040-aaab-4580a854e5b9") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("aace7aed-4ed7-4c24-be15-b19376c92c28") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("e2bef248-ee3c-4c90-b60a-8851744722a2") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("012356fc-52f4-40cf-9555-1555249db7d0") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("034f695b-fb6b-4d05-bd83-1e3e2e18df24") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("0aea0c48-4670-46f0-8fef-6c458fad04e4") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("2941b8ed-e5f4-4987-b3ec-956ae30a8095") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("2c153408-28e9-4cbf-a233-33c99d7a8481") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("2fb750c9-dfc5-47a2-8759-124f01061e29") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("66ec3a9e-7c28-4b89-906c-453f7f333343") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("7429d8ac-c51f-4783-b3ac-4822ddf3e623") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("74b14ddf-b7cc-47d7-b1c0-805c0cda254f") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("7776add1-ea90-4104-9c61-08a4bbe398ab") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("7aeb0f91-6136-484c-ae71-38e99b45ea62") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("865c44f6-8606-4dc0-bd11-b51c9c1b8f35") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("a1df3c83-179c-49d3-99aa-63bca1167db2") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("be8dacea-4048-4e23-9cf8-c0315eb25311") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("c37628e1-a20f-461d-aafe-1aa4d3438b10") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("f315c1c9-cca5-4054-bc80-e09ec4ce9713") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("4af17f1e-e195-4588-9a83-1e345f772d19") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("5fab4a4f-a337-46dc-88d4-70bbd1bb39ee") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("845c49c4-99db-4f81-9c90-78b446abb1d8") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("c01d3528-de64-4373-af8b-35c9789d1e90") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("f90f1bef-b84b-4380-ac67-e715f065463e") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("81fca663-9d6f-4d57-88bf-60572d75bd27"), new Guid("067b1d37-b571-4b5f-9d08-26ff4e554650") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("81fca663-9d6f-4d57-88bf-60572d75bd27"), new Guid("3a43e75b-f4a9-46b5-992e-42c0a5416220") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("81fca663-9d6f-4d57-88bf-60572d75bd27"), new Guid("f29c039b-758d-4c61-9df2-1bbacb84df45") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("8cd87758-7af4-4b0d-a3d1-798fc26d65b8"), new Guid("889c7b44-d349-4dbe-9be5-e25b1193d9a3") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("8cd87758-7af4-4b0d-a3d1-798fc26d65b8"), new Guid("b6a7d82c-dd29-4319-8da2-790479e657cc") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("8cd87758-7af4-4b0d-a3d1-798fc26d65b8"), new Guid("d5f5f01e-3b91-4a7c-9004-9e335a59930d") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("08fbed71-be1b-4d7c-bbc9-df5376dcffca") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("3a5ec84a-f4da-46cc-8e58-9e7f0f053172") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("3f3d13a8-1845-493f-865d-50934dd13385") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("43ac258a-ecc4-47fe-a5a0-4b4d59b42b3c") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("4fc0e852-47c1-4d52-b36b-6166003a001a") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("5f96413b-8a34-4736-a53c-7ed0ba04ba45") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("98e98c39-7d42-436a-bfa9-ffbfa93f8676") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("a9c6093c-ad08-4d64-9e11-6dc5578c8e9c") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("aed413e7-7a42-4a77-beab-9cc58a4d0b33") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("c92ca872-6ee2-471b-a807-0064028afcae") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("dec69d42-6def-459f-8a50-ed5d08fe5771") });

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("17316939-27da-4072-9e9f-151f1dcbbf1e"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("24f4688c-0696-4cbc-b9bf-beebf045cd01"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("26f330d9-b716-4947-b318-430719eb06b8"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("35142756-3afd-477b-9248-a75eab53a95e"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("81fca663-9d6f-4d57-88bf-60572d75bd27"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("8cd87758-7af4-4b0d-a3d1-798fc26d65b8"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "user",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreation",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiration",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "chart_of_account",
                columns: new[] { "Id", "AccountHeaderName", "AccountHeaderNo", "AccountName", "AccountNo", "Created", "CreatedBy", "CurrencyId", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("05715063-7fb4-49be-92f9-a25e9ef1340c"), "Ekuitas", 300, "Ekuitas Pemilik Usaha", 301, null, null, null, null, null },
                    { new Guid("23c7a597-fae3-40f4-ba84-6702cadb39a6"), "Pengeluaran", 500, "Beban Sewa", 502, null, null, null, null, null },
                    { new Guid("2d11cd7a-bbbf-4f2a-af12-686dd83a2200"), "Pengeluaran", 500, "Beban Listrik", 504, null, null, null, null, null },
                    { new Guid("36eaf193-9eed-43e2-a25b-9e7c3c6d87ed"), "Asset", 100, "Sewa dibayar di muka", 115, null, null, null, null, null },
                    { new Guid("4367ea09-0b5e-415e-99cf-7f3bba3abd55"), "Pendapatan", 400, "Pendapatan Penjualan", 401, null, null, null, null, null },
                    { new Guid("4905d891-57c7-476b-97b8-24b2f5d26eba"), "Asset", 100, "Bank", 112, null, null, null, null, null },
                    { new Guid("4d6d2376-35c0-4cb8-ad10-7bb2671fe096"), "Asset", 100, "Akumulasi Depresiasi - Mesin Cuci", 122, null, null, null, null, null },
                    { new Guid("5614c60b-29c1-4137-a304-210d3b8bc5bb"), "Asset", 100, "Persediaan", 113, null, null, null, null, null },
                    { new Guid("852c439a-23a0-4645-a56a-f2c183b9a2b5"), "Asset", 100, "Peralatan", 121, null, null, null, null, null },
                    { new Guid("884ebd0a-7427-4732-bf51-711facd6d041"), "Pengeluaran", 500, "Beban Gaji", 501, null, null, null, null, null },
                    { new Guid("8a8ffadd-b83c-488d-87b4-abef923fa59e"), "Pengeluaran", 500, "Beban Depresiasi", 506, null, null, null, null, null },
                    { new Guid("af8d8bfa-fdf1-4508-9433-c3a4930f046e"), "Pengeluaran", 500, "Beban Perlengkapan", 505, null, null, null, null, null },
                    { new Guid("b6b376f9-490a-4078-989a-90fbb6d99cc9"), "Liabilitas", 200, "Utang Usaha", 201, null, null, null, null, null },
                    { new Guid("b76db597-cdb7-493c-aef2-6f474b08cbd3"), "Pengeluaran", 500, "Beban Utilitas", 503, null, null, null, null, null },
                    { new Guid("c98fcd17-ef7c-4781-a44b-c0d9e3840d96"), "Asset", 100, "Kas", 111, null, null, null, null, null },
                    { new Guid("faa10a6e-4433-4dc8-891f-7e47e2ab8602"), "Asset", 100, "Perlengkapan", 114, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "currency",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "CultureName", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("10930b60-2c60-4daa-be3d-ebd35c22f359"), "IDR", null, null, "id-ID", null, null, "Indonesia Rupiah" },
                    { new Guid("14071cc8-4369-42c8-aa92-7a2bb17ad07c"), "USD", null, null, "en-US", null, null, "Dollar USD" },
                    { new Guid("41483a1e-ad2c-43fd-87ec-95188e8c9bfd"), "SGD", null, null, "en-SG", null, null, "Dollar Singapore" },
                    { new Guid("e536582c-8347-489c-b6c1-4435de12d1a7"), "MYR", null, null, "ms-MY", null, null, "Ringgit Malaysia" }
                });

            migrationBuilder.InsertData(
                table: "laundry_service",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "LaundryProcess", "Name" },
                values: new object[,]
                {
                    { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), null, null, null, null, 3, "SEPATU DAN TAS" },
                    { new Guid("29beb618-8301-493e-ae83-f146d8d0bbdd"), null, null, null, null, 7, "PAKET BULANAN LENGKAP" },
                    { new Guid("302045f7-e303-4bda-b00e-1856fe0e7801"), null, null, null, null, 7, "KILOAN LENGKAP" },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), null, null, null, null, 3, "BED COVER & SELIMUT" },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), null, null, null, null, 3, "SATUAN" },
                    { new Guid("8223d17d-c036-4601-b723-4cb3e6d3ede0"), null, null, null, null, 3, "BANTAL/BONEKA" },
                    { new Guid("a7395bb6-30fb-4767-9d3b-68d03fbf795b"), null, null, null, null, 3, "KARPET/GORDEN" },
                    { new Guid("ac8f3f6e-c2e3-4bc6-84c5-520327ef4f1d"), null, null, null, null, 7, "KILOAN SETRIKA/CUCI LIPAT" },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), null, null, null, null, 3, "SPREI & ALAS KASUR" },
                    { new Guid("ecd36d91-f970-4e8f-8f65-3d9be96e0a37"), null, null, null, null, 4, "PAKET BULANAN SETRIKA" }
                });

            migrationBuilder.InsertData(
                table: "price_menu",
                columns: new[] { "LaundryServiceId", "PriceMenuId", "Created", "CreatedBy", "DeliveryOption", "LastModified", "LastModifiedBy", "Name", "Price", "PricingOption", "ProcessingTime", "TimeUnit" },
                values: new object[,]
                {
                    { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("0c64ea8d-9606-44b4-ac27-91cccca3eeed"), null, null, 1, null, null, "Tas Sedang", 25000m, 1, 2, 2 },
                    { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("164c20a0-806a-4e72-a2c1-0f1688e7b920"), null, null, 1, null, null, "Sepatu", 25000m, 1, 2, 2 },
                    { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("236aa4c3-7c48-4cce-9053-ec18be6df18c"), null, null, 1, null, null, "Tas Mini", 10000m, 1, 2, 2 },
                    { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("4a5b2032-cbd2-4959-bfe5-cb3f00bbb7d3"), null, null, 1, null, null, "Tas Besar", 30000m, 1, 2, 2 },
                    { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("52d0965f-260c-4ae3-b714-aee4250099a3"), null, null, 1, null, null, "Tas Kecil", 15000m, 1, 2, 2 },
                    { new Guid("29beb618-8301-493e-ae83-f146d8d0bbdd"), new Guid("63c5432d-0c8c-45bc-a41c-a1ecf13750ea"), null, null, 0, null, null, "50 Kgs", 275000m, 8, 0, 0 },
                    { new Guid("29beb618-8301-493e-ae83-f146d8d0bbdd"), new Guid("b0371d55-6b22-4df2-9eb7-815297c6c5ca"), null, null, 0, null, null, "100 Kgs", 550000m, 8, 0, 0 },
                    { new Guid("29beb618-8301-493e-ae83-f146d8d0bbdd"), new Guid("c96d3594-b647-4f2b-b0a1-38edb879c12d"), null, null, 0, null, null, "25 Kgs", 140000m, 8, 0, 0 },
                    { new Guid("302045f7-e303-4bda-b00e-1856fe0e7801"), new Guid("16741d42-0132-4591-a960-a604e31fa225"), null, null, 2, null, null, "One Day", 8000m, 2, 1, 2 },
                    { new Guid("302045f7-e303-4bda-b00e-1856fe0e7801"), new Guid("273546b1-425a-4c8a-8f4f-2d190998cf42"), null, null, 4, null, null, "Express", 10000m, 2, 6, 1 },
                    { new Guid("302045f7-e303-4bda-b00e-1856fe0e7801"), new Guid("ca131bbf-e81d-4a1c-b6a6-75a2e9314e7e"), null, null, 1, null, null, "Reguler", 6000m, 2, 2, 2 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("26aff994-b7f4-4ef4-bf29-11252eb25aab"), null, null, 1, null, null, "Bed Cover Single", 15000m, 1, 3, 2 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("3b5edc4b-c643-4c67-9e56-3f034d0f73a7"), null, null, 4, null, null, "Bed Cover King", 50000m, 1, 6, 1 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("4166c909-1917-4a4e-93eb-acc50a375797"), null, null, 4, null, null, "Bed Cover Queen", 40000m, 1, 6, 1 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("4fb27742-8853-420c-8add-dfee27d29c32"), null, null, 4, null, null, "Bed Cover King Set", 76000m, 4, 6, 1 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("55cf4f98-dd64-4d44-bcb1-ad852b55d27f"), null, null, 4, null, null, "Bed Cover Queen Set", 70000m, 4, 6, 1 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("618e6bf2-8e14-488d-bfe8-5303af7ca4b9"), null, null, 4, null, null, "Bed Cover Single", 30000m, 1, 6, 1 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("62f85138-7916-4fc5-aee1-a2076ac69256"), null, null, 4, null, null, "Selimut", 30000m, 1, 6, 1 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("68ef264a-a5f6-4b8e-b54d-2b38fc17aac8"), null, null, 1, null, null, "Bed Cover King Set", 38000m, 4, 3, 2 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("7fa6dcc4-f367-4c17-8781-b32e3d874249"), null, null, 1, null, null, "Bed Cover Queen Set", 35000m, 4, 3, 2 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("82db1721-1eb6-4c67-bd01-699bb0a37b59"), null, null, 4, null, null, "Bed Cover Single Set", 54000m, 4, 6, 1 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("83aab2c3-a298-42b2-8145-0ab065cd61dc"), null, null, 4, null, null, "Selimut Tipis", 20000m, 1, 6, 1 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("98565c64-329b-4dde-a527-d72649ec8335"), null, null, 1, null, null, "Bed Cover Single Set", 27000m, 4, 3, 2 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("9f756e7d-7166-4c47-85bb-b7b98fc1e565"), null, null, 1, null, null, "Bed Cover Queen", 20000m, 1, 3, 2 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("ba8371dc-a654-474d-977f-27fd18834f7c"), null, null, 1, null, null, "Selimut", 15000m, 1, 3, 2 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("f7e1e027-d323-48b1-a183-6296c776f11f"), null, null, 1, null, null, "Selimut Tipis", 10000m, 1, 3, 2 },
                    { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("ff647078-2430-4d92-ae23-e8636da71e1e"), null, null, 1, null, null, "Bed Cover King", 25000m, 1, 3, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("0bb4a89a-3192-4007-9a8c-4a7cc4285230"), null, null, 1, null, null, "Dress Panjang", 15000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("1191ca1b-c2ea-406d-8614-e3bd1d10727b"), null, null, 1, null, null, "Handuk Sedang", 6000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("1f26e158-ca65-418e-a404-6c7926992273"), null, null, 1, null, null, "Dress Pendek", 15000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("44da787e-5d33-4860-8242-f7ca3b17a733"), null, null, 1, null, null, "Atasan", 10000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("6a635d7b-e3e5-4020-ba3b-30bba861e746"), null, null, 1, null, null, "Bawahan", 10000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("9d88e120-c6e6-4c0a-af66-f42a31b1340c"), null, null, 1, null, null, "Blazer Set", 25000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("bb0c462a-f3e3-42bb-bc3f-aa891084e35d"), null, null, 1, null, null, "Jas Set", 30000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("bf81a4b9-2f80-40ae-b1c1-d8b595b41f96"), null, null, 1, null, null, "Handuk Besar", 7000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("d8a44d07-4895-4b70-9318-91f17358bcc2"), null, null, 1, null, null, "Blazer", 15000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("f374defc-5885-4065-b615-2de763e4b163"), null, null, 1, null, null, "Jas", 20000m, 1, 2, 2 },
                    { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("f81d23fa-1ecd-42b7-87cc-bd84665b46ca"), null, null, 1, null, null, "Jaket/Sweater", 15000m, 1, 2, 2 },
                    { new Guid("ac8f3f6e-c2e3-4bc6-84c5-520327ef4f1d"), new Guid("331902b7-5b9b-4b8d-9dda-77de63489cce"), null, null, 2, null, null, "One Day", 7000m, 2, 1, 2 },
                    { new Guid("ac8f3f6e-c2e3-4bc6-84c5-520327ef4f1d"), new Guid("339713a0-05fd-4a7d-b5e6-ec2a2a3831c8"), null, null, 1, null, null, "Reguler", 5000m, 2, 2, 2 },
                    { new Guid("ac8f3f6e-c2e3-4bc6-84c5-520327ef4f1d"), new Guid("b34d53f0-a596-42fe-9fb9-e01bd3cd7a32"), null, null, 4, null, null, "Express", 9000m, 2, 6, 1 },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("0077451c-a72c-4a1f-8265-de8ad3ff13bf"), null, null, 1, null, null, "Alas Kasur Queen", 20000m, 1, 3, 2 },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("36683734-99a7-435a-a5dd-3252ff7c1439"), null, null, 1, null, null, "Alas Kasur Single", 15000m, 1, 3, 2 },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("3cadfd31-8049-4b7d-9cba-6ef03da9d36d"), null, null, 1, null, null, "Sprei Queen", 15000m, 1, 3, 2 },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("68df888c-4dcf-4657-9bbb-7d01936255ce"), null, null, 1, null, null, "Sprei King", 15000m, 1, 3, 2 },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("941d94c3-5b9f-40ef-949c-b860e9262a6b"), null, null, 1, null, null, "Sprei Single", 10000m, 1, 3, 2 },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("b300944b-7e44-4925-b2f0-fe8b656c1057"), null, null, 1, null, null, "Alas Kasur King", 25000m, 1, 3, 2 },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("bf9c37a9-0645-481b-bcde-1b56607e8f7e"), null, null, 1, null, null, "Sprei Quen Set", 20000m, 4, 3, 2 },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("ddd564a7-b395-4ead-9d5f-86ced965f468"), null, null, 1, null, null, "Sprei Single Set", 15000m, 4, 3, 2 },
                    { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("f2f43c4c-f275-45f2-926b-02aef7c07e3c"), null, null, 1, null, null, "Sprei King Set", 20000m, 4, 3, 2 },
                    { new Guid("ecd36d91-f970-4e8f-8f65-3d9be96e0a37"), new Guid("02ea5984-b8ed-4e6e-bd18-f281a75ca503"), null, null, 0, null, null, "50 Kgs", 240000m, 8, 0, 0 },
                    { new Guid("ecd36d91-f970-4e8f-8f65-3d9be96e0a37"), new Guid("0a976b33-0d6d-4979-9a52-963f880d03dd"), null, null, 0, null, null, "100 Kgs", 475000m, 8, 0, 0 },
                    { new Guid("ecd36d91-f970-4e8f-8f65-3d9be96e0a37"), new Guid("c52ba3df-8088-4ddb-8fa4-60984b39a21f"), null, null, 0, null, null, "25 Kgs", 120000m, 8, 0, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("05715063-7fb4-49be-92f9-a25e9ef1340c"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("23c7a597-fae3-40f4-ba84-6702cadb39a6"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("2d11cd7a-bbbf-4f2a-af12-686dd83a2200"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("36eaf193-9eed-43e2-a25b-9e7c3c6d87ed"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("4367ea09-0b5e-415e-99cf-7f3bba3abd55"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("4905d891-57c7-476b-97b8-24b2f5d26eba"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("4d6d2376-35c0-4cb8-ad10-7bb2671fe096"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("5614c60b-29c1-4137-a304-210d3b8bc5bb"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("852c439a-23a0-4645-a56a-f2c183b9a2b5"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("884ebd0a-7427-4732-bf51-711facd6d041"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("8a8ffadd-b83c-488d-87b4-abef923fa59e"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("af8d8bfa-fdf1-4508-9433-c3a4930f046e"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("b6b376f9-490a-4078-989a-90fbb6d99cc9"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("b76db597-cdb7-493c-aef2-6f474b08cbd3"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("c98fcd17-ef7c-4781-a44b-c0d9e3840d96"));

            migrationBuilder.DeleteData(
                table: "chart_of_account",
                keyColumn: "Id",
                keyValue: new Guid("faa10a6e-4433-4dc8-891f-7e47e2ab8602"));

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "Id",
                keyValue: new Guid("10930b60-2c60-4daa-be3d-ebd35c22f359"));

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "Id",
                keyValue: new Guid("14071cc8-4369-42c8-aa92-7a2bb17ad07c"));

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "Id",
                keyValue: new Guid("41483a1e-ad2c-43fd-87ec-95188e8c9bfd"));

            migrationBuilder.DeleteData(
                table: "currency",
                keyColumn: "Id",
                keyValue: new Guid("e536582c-8347-489c-b6c1-4435de12d1a7"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("8223d17d-c036-4601-b723-4cb3e6d3ede0"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("a7395bb6-30fb-4767-9d3b-68d03fbf795b"));

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("0c64ea8d-9606-44b4-ac27-91cccca3eeed") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("164c20a0-806a-4e72-a2c1-0f1688e7b920") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("236aa4c3-7c48-4cce-9053-ec18be6df18c") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("4a5b2032-cbd2-4959-bfe5-cb3f00bbb7d3") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"), new Guid("52d0965f-260c-4ae3-b714-aee4250099a3") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("29beb618-8301-493e-ae83-f146d8d0bbdd"), new Guid("63c5432d-0c8c-45bc-a41c-a1ecf13750ea") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("29beb618-8301-493e-ae83-f146d8d0bbdd"), new Guid("b0371d55-6b22-4df2-9eb7-815297c6c5ca") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("29beb618-8301-493e-ae83-f146d8d0bbdd"), new Guid("c96d3594-b647-4f2b-b0a1-38edb879c12d") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("302045f7-e303-4bda-b00e-1856fe0e7801"), new Guid("16741d42-0132-4591-a960-a604e31fa225") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("302045f7-e303-4bda-b00e-1856fe0e7801"), new Guid("273546b1-425a-4c8a-8f4f-2d190998cf42") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("302045f7-e303-4bda-b00e-1856fe0e7801"), new Guid("ca131bbf-e81d-4a1c-b6a6-75a2e9314e7e") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("26aff994-b7f4-4ef4-bf29-11252eb25aab") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("3b5edc4b-c643-4c67-9e56-3f034d0f73a7") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("4166c909-1917-4a4e-93eb-acc50a375797") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("4fb27742-8853-420c-8add-dfee27d29c32") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("55cf4f98-dd64-4d44-bcb1-ad852b55d27f") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("618e6bf2-8e14-488d-bfe8-5303af7ca4b9") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("62f85138-7916-4fc5-aee1-a2076ac69256") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("68ef264a-a5f6-4b8e-b54d-2b38fc17aac8") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("7fa6dcc4-f367-4c17-8781-b32e3d874249") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("82db1721-1eb6-4c67-bd01-699bb0a37b59") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("83aab2c3-a298-42b2-8145-0ab065cd61dc") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("98565c64-329b-4dde-a527-d72649ec8335") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("9f756e7d-7166-4c47-85bb-b7b98fc1e565") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("ba8371dc-a654-474d-977f-27fd18834f7c") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("f7e1e027-d323-48b1-a183-6296c776f11f") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"), new Guid("ff647078-2430-4d92-ae23-e8636da71e1e") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("0bb4a89a-3192-4007-9a8c-4a7cc4285230") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("1191ca1b-c2ea-406d-8614-e3bd1d10727b") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("1f26e158-ca65-418e-a404-6c7926992273") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("44da787e-5d33-4860-8242-f7ca3b17a733") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("6a635d7b-e3e5-4020-ba3b-30bba861e746") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("9d88e120-c6e6-4c0a-af66-f42a31b1340c") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("bb0c462a-f3e3-42bb-bc3f-aa891084e35d") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("bf81a4b9-2f80-40ae-b1c1-d8b595b41f96") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("d8a44d07-4895-4b70-9318-91f17358bcc2") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("f374defc-5885-4065-b615-2de763e4b163") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("75e0983a-677f-4604-a0b4-478d214e9598"), new Guid("f81d23fa-1ecd-42b7-87cc-bd84665b46ca") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("ac8f3f6e-c2e3-4bc6-84c5-520327ef4f1d"), new Guid("331902b7-5b9b-4b8d-9dda-77de63489cce") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("ac8f3f6e-c2e3-4bc6-84c5-520327ef4f1d"), new Guid("339713a0-05fd-4a7d-b5e6-ec2a2a3831c8") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("ac8f3f6e-c2e3-4bc6-84c5-520327ef4f1d"), new Guid("b34d53f0-a596-42fe-9fb9-e01bd3cd7a32") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("0077451c-a72c-4a1f-8265-de8ad3ff13bf") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("36683734-99a7-435a-a5dd-3252ff7c1439") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("3cadfd31-8049-4b7d-9cba-6ef03da9d36d") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("68df888c-4dcf-4657-9bbb-7d01936255ce") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("941d94c3-5b9f-40ef-949c-b860e9262a6b") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("b300944b-7e44-4925-b2f0-fe8b656c1057") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("bf9c37a9-0645-481b-bcde-1b56607e8f7e") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("ddd564a7-b395-4ead-9d5f-86ced965f468") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"), new Guid("f2f43c4c-f275-45f2-926b-02aef7c07e3c") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("ecd36d91-f970-4e8f-8f65-3d9be96e0a37"), new Guid("02ea5984-b8ed-4e6e-bd18-f281a75ca503") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("ecd36d91-f970-4e8f-8f65-3d9be96e0a37"), new Guid("0a976b33-0d6d-4979-9a52-963f880d03dd") });

            migrationBuilder.DeleteData(
                table: "price_menu",
                keyColumns: new[] { "LaundryServiceId", "PriceMenuId" },
                keyValues: new object[] { new Guid("ecd36d91-f970-4e8f-8f65-3d9be96e0a37"), new Guid("c52ba3df-8088-4ddb-8fa4-60984b39a21f") });

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("1321a22b-b7a4-44a6-ab6a-f5ad50cd314a"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("29beb618-8301-493e-ae83-f146d8d0bbdd"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("302045f7-e303-4bda-b00e-1856fe0e7801"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("5c65db87-598e-40bf-821c-0acf5d19999a"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("75e0983a-677f-4604-a0b4-478d214e9598"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("ac8f3f6e-c2e3-4bc6-84c5-520327ef4f1d"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("b1c9d482-d63f-4dd6-898d-18bfb3730f11"));

            migrationBuilder.DeleteData(
                table: "laundry_service",
                keyColumn: "Id",
                keyValue: new Guid("ecd36d91-f970-4e8f-8f65-3d9be96e0a37"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "user");

            migrationBuilder.DropColumn(
                name: "TokenCreation",
                table: "user");

            migrationBuilder.DropColumn(
                name: "TokenExpiration",
                table: "user");

            migrationBuilder.InsertData(
                table: "chart_of_account",
                columns: new[] { "Id", "AccountHeaderName", "AccountHeaderNo", "AccountName", "AccountNo", "Created", "CreatedBy", "CurrencyId", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("01315a51-bbc7-4e22-bfb0-abce26d2607c"), "Pengeluaran", 500, "Beban Depresiasi", 506, null, null, null, null, null },
                    { new Guid("2d4d1231-267f-4f8d-9967-0c06718eac09"), "Asset", 100, "Bank", 112, null, null, null, null, null },
                    { new Guid("3c257b8b-10b0-46c6-b63f-71296619cb65"), "Asset", 100, "Persediaan", 113, null, null, null, null, null },
                    { new Guid("4641765c-ee78-4a2f-82e7-4574eb1dab0d"), "Asset", 100, "Kas", 111, null, null, null, null, null },
                    { new Guid("531c171b-4b4a-4ca3-8a3b-db705903b051"), "Pengeluaran", 500, "Beban Listrik", 504, null, null, null, null, null },
                    { new Guid("57060c37-e3f1-4871-b565-df83f354edaa"), "Pengeluaran", 500, "Beban Utilitas", 503, null, null, null, null, null },
                    { new Guid("5b6e2813-bbbc-497e-a19a-d914377514b2"), "Asset", 100, "Sewa dibayar di muka", 115, null, null, null, null, null },
                    { new Guid("5c168ca1-33bb-43ef-aeb3-6c8989d9fd2b"), "Pengeluaran", 500, "Beban Perlengkapan", 505, null, null, null, null, null },
                    { new Guid("6384d92c-cd73-4219-86b0-a653f5b72adc"), "Asset", 100, "Perlengkapan", 114, null, null, null, null, null },
                    { new Guid("b3433c9f-5092-42de-a819-d08c13e2c786"), "Ekuitas", 300, "Ekuitas Pemilik Usaha", 301, null, null, null, null, null },
                    { new Guid("d213010c-ce6c-4712-9699-fb504986d9e9"), "Pengeluaran", 500, "Beban Gaji", 501, null, null, null, null, null },
                    { new Guid("d63d4f83-52fd-40bb-bd0b-b0f60248b282"), "Asset", 100, "Peralatan", 121, null, null, null, null, null },
                    { new Guid("d9fb8c52-1982-4aa3-96aa-3d9f98d220d4"), "Asset", 100, "Akumulasi Depresiasi - Mesin Cuci", 122, null, null, null, null, null },
                    { new Guid("e7decc39-8496-4f8e-98ae-12ccf533f61b"), "Pendapatan", 400, "Pendapatan Penjualan", 401, null, null, null, null, null },
                    { new Guid("ec6d2e6b-009b-4824-aa5a-1f014455b6d6"), "Pengeluaran", 500, "Beban Sewa", 502, null, null, null, null, null },
                    { new Guid("fa87aab7-a87f-4ee0-bc51-9b7df8247f33"), "Liabilitas", 200, "Utang Usaha", 201, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "currency",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "CultureName", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("1b97d48f-1ce4-4d08-93e0-96dc1fdd1c9f"), "MYR", null, null, "ms-MY", null, null, "Ringgit Malaysia" },
                    { new Guid("2b1b149c-93b5-485d-bc6e-26167e0ac899"), "SGD", null, null, "en-SG", null, null, "Dollar Singapore" },
                    { new Guid("4438c3b5-093f-4a58-a112-b91004431915"), "USD", null, null, "en-US", null, null, "Dollar USD" },
                    { new Guid("b97e9501-e2c9-4273-8fea-f437d21ef75c"), "IDR", null, null, "id-ID", null, null, "Indonesia Rupiah" }
                });

            migrationBuilder.InsertData(
                table: "laundry_service",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "LaundryProcess", "Name" },
                values: new object[,]
                {
                    { new Guid("17316939-27da-4072-9e9f-151f1dcbbf1e"), null, null, null, null, 4, "PAKET BULANAN SETRIKA" },
                    { new Guid("24f4688c-0696-4cbc-b9bf-beebf045cd01"), null, null, null, null, 7, "PAKET BULANAN LENGKAP" },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), null, null, null, null, 3, "SPREI & ALAS KASUR" },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), null, null, null, null, 3, "BED COVER & SELIMUT" },
                    { new Guid("43896f0e-231d-4765-8700-24c8422033e0"), null, null, null, null, 3, "BANTAL/BONEKA" },
                    { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), null, null, null, null, 3, "SEPATU DAN TAS" },
                    { new Guid("81fca663-9d6f-4d57-88bf-60572d75bd27"), null, null, null, null, 7, "KILOAN SETRIKA/CUCI LIPAT" },
                    { new Guid("8cd87758-7af4-4b0d-a3d1-798fc26d65b8"), null, null, null, null, 7, "KILOAN LENGKAP" },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), null, null, null, null, 3, "SATUAN" },
                    { new Guid("c6b1f038-dcda-41bf-9ecb-ce213ea84ab6"), null, null, null, null, 3, "KARPET/GORDEN" }
                });

            migrationBuilder.InsertData(
                table: "price_menu",
                columns: new[] { "LaundryServiceId", "PriceMenuId", "Created", "CreatedBy", "DeliveryOption", "LastModified", "LastModifiedBy", "Name", "Price", "PricingOption", "ProcessingTime", "TimeUnit" },
                values: new object[,]
                {
                    { new Guid("17316939-27da-4072-9e9f-151f1dcbbf1e"), new Guid("6b1a7b00-24fc-4fce-843e-dcf3360d5a61"), null, null, 0, null, null, "50 Kgs", 240000m, 8, 0, 0 },
                    { new Guid("17316939-27da-4072-9e9f-151f1dcbbf1e"), new Guid("b41e6970-585c-490d-beb5-0c778d5a82fd"), null, null, 0, null, null, "100 Kgs", 475000m, 8, 0, 0 },
                    { new Guid("17316939-27da-4072-9e9f-151f1dcbbf1e"), new Guid("bcb2c904-25e0-4430-b015-3863bcbfc745"), null, null, 0, null, null, "25 Kgs", 120000m, 8, 0, 0 },
                    { new Guid("24f4688c-0696-4cbc-b9bf-beebf045cd01"), new Guid("334e12eb-27f1-4884-b477-c9251d9d80f8"), null, null, 0, null, null, "50 Kgs", 275000m, 8, 0, 0 },
                    { new Guid("24f4688c-0696-4cbc-b9bf-beebf045cd01"), new Guid("78cee24c-15c3-479e-9d69-5898f39bc780"), null, null, 0, null, null, "100 Kgs", 550000m, 8, 0, 0 },
                    { new Guid("24f4688c-0696-4cbc-b9bf-beebf045cd01"), new Guid("c086f307-e7ba-42b4-b4fc-9ee88a66833d"), null, null, 0, null, null, "25 Kgs", 140000m, 8, 0, 0 },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("05baa46f-a786-412b-afba-83fcf32b7524"), null, null, 1, null, null, "Sprei King", 15000m, 1, 3, 2 },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("185ab6b1-5920-49c8-9f19-0d2e2d52e5ab"), null, null, 1, null, null, "Sprei Quen Set", 20000m, 4, 3, 2 },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("519a7bea-2a73-4074-a6d3-1060545265e4"), null, null, 1, null, null, "Sprei Single Set", 15000m, 4, 3, 2 },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("6e171c25-7730-4764-83a5-7f84e95e99a9"), null, null, 1, null, null, "Sprei Queen", 15000m, 1, 3, 2 },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("77789a6e-cc71-44bf-bee1-6e3d1bdf5b4a"), null, null, 1, null, null, "Sprei Single", 10000m, 1, 3, 2 },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("9e330b40-4a53-45ae-8fff-501c6d5f5196"), null, null, 1, null, null, "Sprei King Set", 20000m, 4, 3, 2 },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("9f930088-e479-4040-aaab-4580a854e5b9"), null, null, 1, null, null, "Alas Kasur Queen", 20000m, 1, 3, 2 },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("aace7aed-4ed7-4c24-be15-b19376c92c28"), null, null, 1, null, null, "Alas Kasur King", 25000m, 1, 3, 2 },
                    { new Guid("26f330d9-b716-4947-b318-430719eb06b8"), new Guid("e2bef248-ee3c-4c90-b60a-8851744722a2"), null, null, 1, null, null, "Alas Kasur Single", 15000m, 1, 3, 2 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("012356fc-52f4-40cf-9555-1555249db7d0"), null, null, 1, null, null, "Selimut", 15000m, 1, 3, 2 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("034f695b-fb6b-4d05-bd83-1e3e2e18df24"), null, null, 1, null, null, "Bed Cover Single Set", 27000m, 4, 3, 2 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("0aea0c48-4670-46f0-8fef-6c458fad04e4"), null, null, 1, null, null, "Bed Cover King Set", 38000m, 4, 3, 2 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("2941b8ed-e5f4-4987-b3ec-956ae30a8095"), null, null, 1, null, null, "Bed Cover Queen", 20000m, 1, 3, 2 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("2c153408-28e9-4cbf-a233-33c99d7a8481"), null, null, 1, null, null, "Bed Cover Single", 15000m, 1, 3, 2 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("2fb750c9-dfc5-47a2-8759-124f01061e29"), null, null, 4, null, null, "Selimut Tipis", 20000m, 1, 6, 1 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("66ec3a9e-7c28-4b89-906c-453f7f333343"), null, null, 1, null, null, "Bed Cover King", 25000m, 1, 3, 2 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("7429d8ac-c51f-4783-b3ac-4822ddf3e623"), null, null, 4, null, null, "Bed Cover Single Set", 54000m, 4, 6, 1 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("74b14ddf-b7cc-47d7-b1c0-805c0cda254f"), null, null, 4, null, null, "Bed Cover Queen", 40000m, 1, 6, 1 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("7776add1-ea90-4104-9c61-08a4bbe398ab"), null, null, 1, null, null, "Selimut Tipis", 10000m, 1, 3, 2 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("7aeb0f91-6136-484c-ae71-38e99b45ea62"), null, null, 1, null, null, "Bed Cover Queen Set", 35000m, 4, 3, 2 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("865c44f6-8606-4dc0-bd11-b51c9c1b8f35"), null, null, 4, null, null, "Bed Cover King Set", 76000m, 4, 6, 1 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("a1df3c83-179c-49d3-99aa-63bca1167db2"), null, null, 4, null, null, "Bed Cover Single", 30000m, 1, 6, 1 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("be8dacea-4048-4e23-9cf8-c0315eb25311"), null, null, 4, null, null, "Selimut", 30000m, 1, 6, 1 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("c37628e1-a20f-461d-aafe-1aa4d3438b10"), null, null, 4, null, null, "Bed Cover Queen Set", 70000m, 4, 6, 1 },
                    { new Guid("35142756-3afd-477b-9248-a75eab53a95e"), new Guid("f315c1c9-cca5-4054-bc80-e09ec4ce9713"), null, null, 4, null, null, "Bed Cover King", 50000m, 1, 6, 1 },
                    { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("4af17f1e-e195-4588-9a83-1e345f772d19"), null, null, 1, null, null, "Tas Sedang", 25000m, 1, 2, 2 },
                    { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("5fab4a4f-a337-46dc-88d4-70bbd1bb39ee"), null, null, 1, null, null, "Tas Kecil", 15000m, 1, 2, 2 },
                    { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("845c49c4-99db-4f81-9c90-78b446abb1d8"), null, null, 1, null, null, "Sepatu", 25000m, 1, 2, 2 },
                    { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("c01d3528-de64-4373-af8b-35c9789d1e90"), null, null, 1, null, null, "Tas Mini", 10000m, 1, 2, 2 },
                    { new Guid("79557007-dc13-4a39-831d-d9bf0b17cd38"), new Guid("f90f1bef-b84b-4380-ac67-e715f065463e"), null, null, 1, null, null, "Tas Besar", 30000m, 1, 2, 2 },
                    { new Guid("81fca663-9d6f-4d57-88bf-60572d75bd27"), new Guid("067b1d37-b571-4b5f-9d08-26ff4e554650"), null, null, 4, null, null, "Express", 9000m, 2, 6, 1 },
                    { new Guid("81fca663-9d6f-4d57-88bf-60572d75bd27"), new Guid("3a43e75b-f4a9-46b5-992e-42c0a5416220"), null, null, 1, null, null, "Reguler", 5000m, 2, 2, 2 },
                    { new Guid("81fca663-9d6f-4d57-88bf-60572d75bd27"), new Guid("f29c039b-758d-4c61-9df2-1bbacb84df45"), null, null, 2, null, null, "One Day", 7000m, 2, 1, 2 },
                    { new Guid("8cd87758-7af4-4b0d-a3d1-798fc26d65b8"), new Guid("889c7b44-d349-4dbe-9be5-e25b1193d9a3"), null, null, 4, null, null, "Express", 10000m, 2, 6, 1 },
                    { new Guid("8cd87758-7af4-4b0d-a3d1-798fc26d65b8"), new Guid("b6a7d82c-dd29-4319-8da2-790479e657cc"), null, null, 2, null, null, "One Day", 8000m, 2, 1, 2 },
                    { new Guid("8cd87758-7af4-4b0d-a3d1-798fc26d65b8"), new Guid("d5f5f01e-3b91-4a7c-9004-9e335a59930d"), null, null, 1, null, null, "Reguler", 6000m, 2, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("08fbed71-be1b-4d7c-bbc9-df5376dcffca"), null, null, 1, null, null, "Atasan", 10000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("3a5ec84a-f4da-46cc-8e58-9e7f0f053172"), null, null, 1, null, null, "Dress Pendek", 15000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("3f3d13a8-1845-493f-865d-50934dd13385"), null, null, 1, null, null, "Jas Set", 30000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("43ac258a-ecc4-47fe-a5a0-4b4d59b42b3c"), null, null, 1, null, null, "Jaket/Sweater", 15000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("4fc0e852-47c1-4d52-b36b-6166003a001a"), null, null, 1, null, null, "Bawahan", 10000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("5f96413b-8a34-4736-a53c-7ed0ba04ba45"), null, null, 1, null, null, "Blazer Set", 25000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("98e98c39-7d42-436a-bfa9-ffbfa93f8676"), null, null, 1, null, null, "Dress Panjang", 15000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("a9c6093c-ad08-4d64-9e11-6dc5578c8e9c"), null, null, 1, null, null, "Handuk Sedang", 6000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("aed413e7-7a42-4a77-beab-9cc58a4d0b33"), null, null, 1, null, null, "Blazer", 15000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("c92ca872-6ee2-471b-a807-0064028afcae"), null, null, 1, null, null, "Jas", 20000m, 1, 2, 2 },
                    { new Guid("c1e34235-5f6c-4549-9c27-f0feee77c4cc"), new Guid("dec69d42-6def-459f-8a50-ed5d08fe5771"), null, null, 1, null, null, "Handuk Besar", 7000m, 1, 2, 2 }
                });
        }
    }
}
