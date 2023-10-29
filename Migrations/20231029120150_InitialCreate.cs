using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CultureName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "general_account_header",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general_account_header", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "laundry_service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LaundryProcess = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laundry_service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chart_of_account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountHeaderNo = table.Column<int>(type: "int", nullable: false),
                    AccountHeaderName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AccountNo = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chart_of_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chart_of_account_currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "currency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customer_currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "currency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_supplier_currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "currency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "price_menu",
                columns: table => new
                {
                    LaundryServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,3)", precision: 19, scale: 3, nullable: false),
                    PricingOption = table.Column<int>(type: "int", nullable: false),
                    ProcessingTime = table.Column<int>(type: "int", nullable: false),
                    TimeUnit = table.Column<int>(type: "int", nullable: false),
                    DeliveryOption = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_price_menu", x => new { x.LaundryServiceId, x.PriceMenuId });
                    table.ForeignKey(
                        name: "FK_price_menu_laundry_service_LaundryServiceId",
                        column: x => x.LaundryServiceId,
                        principalTable: "laundry_service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => new { x.ApplicationUserId, x.ApplicationRoleId });
                    table.ForeignKey(
                        name: "FK_user_role_role_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_user_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "general_account_detail",
                columns: table => new
                {
                    GeneralAccountHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralAccountDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChartOfAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general_account_detail", x => new { x.GeneralAccountHeaderId, x.GeneralAccountDetailId });
                    table.ForeignKey(
                        name: "FK_general_account_detail_chart_of_account_ChartOfAccountId",
                        column: x => x.ChartOfAccountId,
                        principalTable: "chart_of_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_general_account_detail_general_account_header_GeneralAccountHeaderId",
                        column: x => x.GeneralAccountHeaderId,
                        principalTable: "general_account_header",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_header",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SalesDate = table.Column<DateTime>(type: "date", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales_header", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sales_header_currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "currency",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_sales_header_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_header",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_header", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchase_header_currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "currency",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_purchase_header_supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_detail",
                columns: table => new
                {
                    SalesHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LaundryServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales_detail", x => new { x.SalesHeaderId, x.SalesDetailId });
                    table.ForeignKey(
                        name: "FK_sales_detail_laundry_service_LaundryServiceId",
                        column: x => x.LaundryServiceId,
                        principalTable: "laundry_service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sales_detail_price_menu_LaundryServiceId_PriceMenuId",
                        columns: x => new { x.LaundryServiceId, x.PriceMenuId },
                        principalTable: "price_menu",
                        principalColumns: new[] { "LaundryServiceId", "PriceMenuId" });
                    table.ForeignKey(
                        name: "FK_sales_detail_sales_header_SalesHeaderId",
                        column: x => x.SalesHeaderId,
                        principalTable: "sales_header",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_payment",
                columns: table => new
                {
                    SalesHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales_payment", x => new { x.SalesPaymentId, x.SalesHeaderId });
                    table.ForeignKey(
                        name: "FK_sales_payment_sales_header_SalesHeaderId",
                        column: x => x.SalesHeaderId,
                        principalTable: "sales_header",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_detail",
                columns: table => new
                {
                    PurchaseHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_detail", x => new { x.PurchaseHeaderId, x.PurchaseDetailId });
                    table.ForeignKey(
                        name: "FK_purchase_detail_inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_detail_purchase_header_PurchaseHeaderId",
                        column: x => x.PurchaseHeaderId,
                        principalTable: "purchase_header",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_account_AccountNo",
                table: "chart_of_account",
                column: "AccountNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_account_CurrencyId",
                table: "chart_of_account",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_currency_Name_Code_CultureName",
                table: "currency",
                columns: new[] { "Name", "Code", "CultureName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_CurrencyId",
                table: "customer",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_customer_CustomerCode",
                table: "customer",
                column: "CustomerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_general_account_detail_ChartOfAccountId",
                table: "general_account_detail",
                column: "ChartOfAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_general_account_header_TransactionNo",
                table: "general_account_header",
                column: "TransactionNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_ItemNo",
                table: "inventory",
                column: "ItemNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_laundry_service_Name",
                table: "laundry_service",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_price_menu_LaundryServiceId_Name_DeliveryOption",
                table: "price_menu",
                columns: new[] { "LaundryServiceId", "Name", "DeliveryOption" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_detail_InventoryId",
                table: "purchase_detail",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_header_CurrencyId",
                table: "purchase_header",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_header_PurchaseNo",
                table: "purchase_header",
                column: "PurchaseNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_header_SupplierId",
                table: "purchase_header",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_role_Name",
                table: "role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_detail_LaundryServiceId_PriceMenuId",
                table: "sales_detail",
                columns: new[] { "LaundryServiceId", "PriceMenuId" });

            migrationBuilder.CreateIndex(
                name: "IX_sales_header_CurrencyId",
                table: "sales_header",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_header_CustomerId",
                table: "sales_header",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_header_SalesNo",
                table: "sales_header",
                column: "SalesNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_payment_SalesHeaderId",
                table: "sales_payment",
                column: "SalesHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_supplier_CurrencyId",
                table: "supplier",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_supplier_SupplierCode",
                table: "supplier",
                column: "SupplierCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_Login_EmailAddress",
                table: "user",
                columns: new[] { "Login", "EmailAddress" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_ApplicationRoleId",
                table: "user_role",
                column: "ApplicationRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "general_account_detail");

            migrationBuilder.DropTable(
                name: "purchase_detail");

            migrationBuilder.DropTable(
                name: "sales_detail");

            migrationBuilder.DropTable(
                name: "sales_payment");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "chart_of_account");

            migrationBuilder.DropTable(
                name: "general_account_header");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "purchase_header");

            migrationBuilder.DropTable(
                name: "price_menu");

            migrationBuilder.DropTable(
                name: "sales_header");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "laundry_service");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "currency");
        }
    }
}
