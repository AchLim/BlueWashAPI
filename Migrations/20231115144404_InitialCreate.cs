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
                name: "journal_entry",
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
                    table.PrimaryKey("PK_journal_entry", x => x.Id);
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
                name: "menu_category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategorySequence = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_category", x => x.Id);
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
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuSequence = table.Column<int>(type: "int", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menu_menu_category_MenuCategoryId",
                        column: x => x.MenuCategoryId,
                        principalTable: "menu_category",
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
                name: "journal_item",
                columns: table => new
                {
                    JournalEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JournalItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_journal_item", x => new { x.JournalEntryId, x.JournalItemId });
                    table.ForeignKey(
                        name: "FK_journal_item_chart_of_account_ChartOfAccountId",
                        column: x => x.ChartOfAccountId,
                        principalTable: "chart_of_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_journal_item_journal_entry_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "journal_entry",
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
                name: "user_menu",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_menu", x => new { x.ApplicationUserId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_user_menu_menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_menu_user_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "user",
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
                    { new Guid("04dde26e-1653-4216-9146-7f7309ce0900"), "Pendapatan", 400, "Pendapatan Penjualan", 401, null, null, null, null, null },
                    { new Guid("0eb1017b-3d4a-4e3a-840e-e817947e5338"), "Pengeluaran", 600, "Beban Sewa", 602, null, null, null, null, null },
                    { new Guid("1adeb7d2-34b2-414e-920c-d00e77473961"), "Pengeluaran", 600, "Beban Depresiasi", 606, null, null, null, null, null },
                    { new Guid("23e10c87-1cad-40e3-81e2-bcbe8b27d151"), "Harga Pokok Penjualan", 500, "Persediaan Akhir", 521, null, null, null, null, null },
                    { new Guid("35a11db0-16bb-4a2d-884e-9e1517b9b57e"), "Harga Pokok Penjualan", 500, "Pembelian", 501, null, null, null, null, null },
                    { new Guid("4a7fd545-86bf-4fbd-82c7-63f22e2a0769"), "Asset", 100, "Bank", 112, null, null, null, null, null },
                    { new Guid("86adb522-4548-40e7-9d1f-dddb5737d63e"), "Asset", 100, "Sewa dibayar di muka", 115, null, null, null, null, null },
                    { new Guid("8f8dafcb-4189-4213-9dff-0dbd8853ee02"), "Pengeluaran", 600, "Beban Utilitas", 603, null, null, null, null, null },
                    { new Guid("9a1aa71f-23f4-4529-b15b-fa77b22667f5"), "Harga Pokok Penjualan", 500, "Persediaan Awal", 511, null, null, null, null, null },
                    { new Guid("9f032de8-92b1-4c5f-bbd6-7dc151742df2"), "Liabilitas", 200, "Utang Usaha", 201, null, null, null, null, null },
                    { new Guid("a076dfe6-8380-4b56-b7f7-b2ba56bf7ee8"), "Pengeluaran", 600, "Beban Listrik", 604, null, null, null, null, null },
                    { new Guid("a4b163c5-ec78-464e-a6f3-abf4b97cf715"), "Asset", 100, "Persediaan", 113, null, null, null, null, null },
                    { new Guid("ac3c5609-c66d-4813-9f86-b9e9f3e25389"), "Pengeluaran", 600, "Beban Gaji", 601, null, null, null, null, null },
                    { new Guid("c7395bdb-5497-4b5e-b325-b4987d8a628f"), "Asset", 100, "Perlengkapan", 114, null, null, null, null, null },
                    { new Guid("c9177b10-3a3e-4a86-831c-0b04120ee3c2"), "Asset", 100, "Akumulasi Depresiasi - Mesin Cuci", 122, null, null, null, null, null },
                    { new Guid("ca7cb4dc-802a-4293-b75c-8d05294bbb33"), "Asset", 100, "Peralatan", 121, null, null, null, null, null },
                    { new Guid("dd2a19fe-87a7-41ca-a6ee-86f518950c3b"), "Asset", 100, "Kas", 111, null, null, null, null, null },
                    { new Guid("de6d08fc-c38f-4c42-aa5b-519f16d93057"), "Ekuitas", 300, "Ekuitas Pemilik Usaha", 301, null, null, null, null, null },
                    { new Guid("f31e0ed4-7141-489b-8453-5b579d288cda"), "Pengeluaran", 600, "Beban Perlengkapan", 605, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "currency",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "CultureName", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("17247749-f3c8-476d-a74c-7c5ed30e00d3"), "SGD", null, null, "en-SG", null, null, "Dollar Singapore" },
                    { new Guid("176874d6-6c06-48d7-9b63-6fb134e49942"), "IDR", null, null, "id-ID", null, null, "Indonesia Rupiah" },
                    { new Guid("ae970c42-6ed5-4097-8ab6-845d2473c1b5"), "USD", null, null, "en-US", null, null, "Dollar USD" },
                    { new Guid("af28147c-d8bd-428b-ad8e-095d596a13ea"), "MYR", null, null, "ms-MY", null, null, "Ringgit Malaysia" }
                });

            migrationBuilder.InsertData(
                table: "laundry_service",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "LaundryProcess", "Name" },
                values: new object[,]
                {
                    { new Guid("04d9f89f-0e1a-4eea-b0a0-fbf956c39f6f"), null, null, null, null, 3, "KARPET/GORDEN" },
                    { new Guid("280e3ddd-fbc4-4517-bbf8-46c2c2d06fd7"), null, null, null, null, 4, "PAKET BULANAN SETRIKA" },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), null, null, null, null, 3, "SATUAN" },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), null, null, null, null, 3, "BED COVER & SELIMUT" },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), null, null, null, null, 3, "SPREI & ALAS KASUR" },
                    { new Guid("5a432017-9a14-4fc7-a711-07fadb625cbc"), null, null, null, null, 3, "SEPATU DAN TAS" },
                    { new Guid("89d1c832-b679-4e9d-87fe-36e1787f6aa2"), null, null, null, null, 7, "KILOAN LENGKAP" },
                    { new Guid("a7e40294-dcc2-46f6-a093-0819f3901a6c"), null, null, null, null, 7, "PAKET BULANAN LENGKAP" },
                    { new Guid("e9be97d0-ccd6-425a-b158-c0673c62c163"), null, null, null, null, 3, "BANTAL/BONEKA" },
                    { new Guid("f7e5faa1-7cae-4c96-8878-31962d77826b"), null, null, null, null, 7, "KILOAN SETRIKA/CUCI LIPAT" }
                });

            migrationBuilder.InsertData(
                table: "menu_category",
                columns: new[] { "Id", "CategoryDisplayName", "CategoryName", "CategorySequence" },
                values: new object[,]
                {
                    { new Guid("0590c04c-8151-472d-a232-cadbc903bf38"), "Sales", "sales", 60 },
                    { new Guid("4bce05ab-064b-450e-a65b-34a0c67b52fd"), "Transaction", "transaction", 20 },
                    { new Guid("6030865d-6d2e-4552-aa9e-4de69a8311c7"), "Purchase", "purchase", 50 },
                    { new Guid("776bc8f8-98e4-4ecf-8595-1067fa478ecd"), "Master Data", "master-data", 30 },
                    { new Guid("a478658e-7cf7-4b7f-9781-eb7662ec8531"), "Dashboard", "dashboard", 10 },
                    { new Guid("e8314d9f-d217-451e-8e3c-1c36dd8e84e9"), "General Journal", "general-journal", 40 }
                });

            migrationBuilder.InsertData(
                table: "menu",
                columns: new[] { "Id", "MenuCategoryId", "MenuDisplayName", "MenuName", "MenuSequence" },
                values: new object[,]
                {
                    { new Guid("0c638caf-1e14-4b8c-8fe5-253c2b2d2bbd"), new Guid("776bc8f8-98e4-4ecf-8595-1067fa478ecd"), "Persediaan", "inventory", 40 },
                    { new Guid("2eb65cdd-bc17-45e3-8dbc-052a4fe00a37"), new Guid("0590c04c-8151-472d-a232-cadbc903bf38"), "Penjualan", "sales", 10 },
                    { new Guid("36795924-0dd0-43e8-a2cc-c630d109e2a6"), new Guid("776bc8f8-98e4-4ecf-8595-1067fa478ecd"), "Pemasok", "supplier", 30 },
                    { new Guid("4511118f-3148-43f7-8d44-0eee17db4721"), new Guid("776bc8f8-98e4-4ecf-8595-1067fa478ecd"), "Menu Harga", "price-menu", 60 },
                    { new Guid("469f4ec3-cc92-435c-84d4-8fabb8528ddf"), new Guid("776bc8f8-98e4-4ecf-8595-1067fa478ecd"), "Pelanggan", "customer", 20 },
                    { new Guid("478bea66-ab80-4d53-a57e-57572c8b14a7"), new Guid("4bce05ab-064b-450e-a65b-34a0c67b52fd"), "Tambah Pengeluaran", "expense", 20 },
                    { new Guid("4bc2d356-79a5-455c-931a-bd46c191c457"), new Guid("4bce05ab-064b-450e-a65b-34a0c67b52fd"), "Tambah Transaksi", "transaction", 10 },
                    { new Guid("53db20f0-f99f-4eeb-9ff5-c606b50733ed"), new Guid("6030865d-6d2e-4552-aa9e-4de69a8311c7"), "Pembelian", "purchase", 10 },
                    { new Guid("7cc563d8-d6a5-477f-aada-6aa5636bf92c"), new Guid("776bc8f8-98e4-4ecf-8595-1067fa478ecd"), "Chart of Account", "chart-of-account", 10 },
                    { new Guid("9eefffa3-f067-4edf-8add-95fca3dd26bf"), new Guid("a478658e-7cf7-4b7f-9781-eb7662ec8531"), "Dashboard", "dashboard", 10 },
                    { new Guid("adfb93a8-b182-4e53-9671-f395429ecdf6"), new Guid("e8314d9f-d217-451e-8e3c-1c36dd8e84e9"), "Jurnal Umum", "general-journal", 10 },
                    { new Guid("ba3c692e-c612-44c0-aebb-a477bc2091e0"), new Guid("776bc8f8-98e4-4ecf-8595-1067fa478ecd"), "Tipe Laundry", "laundry-service", 50 },
                    { new Guid("d152a663-02e5-4dc5-a392-d06b0e2dada1"), new Guid("0590c04c-8151-472d-a232-cadbc903bf38"), "Pembayaran Penjualan", "sales-payment", 20 }
                });

            migrationBuilder.InsertData(
                table: "price_menu",
                columns: new[] { "LaundryServiceId", "PriceMenuId", "Created", "CreatedBy", "DeliveryOption", "LastModified", "LastModifiedBy", "Name", "Price", "PricingOption", "ProcessingTime", "TimeUnit" },
                values: new object[,]
                {
                    { new Guid("280e3ddd-fbc4-4517-bbf8-46c2c2d06fd7"), new Guid("1f93a90c-1aee-43de-89c7-37f70ceb3aab"), null, null, 0, null, null, "100 Kgs", 475000m, 8, 0, 0 },
                    { new Guid("280e3ddd-fbc4-4517-bbf8-46c2c2d06fd7"), new Guid("4481234d-f123-471d-9015-df165d7427bf"), null, null, 0, null, null, "50 Kgs", 240000m, 8, 0, 0 },
                    { new Guid("280e3ddd-fbc4-4517-bbf8-46c2c2d06fd7"), new Guid("e6046a26-3b66-4f21-a11d-48e17ffbbdf7"), null, null, 0, null, null, "25 Kgs", 120000m, 8, 0, 0 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("31e60924-f23e-4958-b69e-62062602db02"), null, null, 1, null, null, "Dress Panjang", 15000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("5bb35bdf-fd5b-4754-ad74-fb89b346fee5"), null, null, 1, null, null, "Blazer Set", 25000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("6a56a6c8-49f8-4c95-b571-3acbfc0e6b69"), null, null, 1, null, null, "Dress Pendek", 15000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("6f9d6bdb-d8d9-4821-b157-6f6583593e01"), null, null, 1, null, null, "Handuk Sedang", 6000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("941347d1-80f1-4d88-af6f-08731185269b"), null, null, 1, null, null, "Handuk Besar", 7000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("a5853c80-1365-400a-91e5-a4edd5b3827b"), null, null, 1, null, null, "Blazer", 15000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("ae76c037-e417-4d0a-80fe-cc308a306aa7"), null, null, 1, null, null, "Jas Set", 30000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("c6120d9b-7006-42df-a28e-6e3bb9f3f849"), null, null, 1, null, null, "Jas", 20000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("d4683705-2607-4ae1-ad32-359f1d043a31"), null, null, 1, null, null, "Bawahan", 10000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("d949883e-dd9e-4177-b16d-74595edfd1cd"), null, null, 1, null, null, "Jaket/Sweater", 15000m, 1, 2, 2 },
                    { new Guid("284cac47-3b72-4cd9-8280-6a6fb92eedda"), new Guid("f75615f9-f200-4d30-a6f5-ab8dbcedf788"), null, null, 1, null, null, "Atasan", 10000m, 1, 2, 2 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("0d71d3a8-9d80-4a98-ad1b-03bcd68ab874"), null, null, 1, null, null, "Bed Cover King", 25000m, 1, 3, 2 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("0f3284dc-e566-41d3-a981-0af4d1865a66"), null, null, 4, null, null, "Bed Cover King Set", 76000m, 4, 6, 1 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("15b8b87c-c035-4022-9fd3-ebde50ef6e00"), null, null, 1, null, null, "Bed Cover Single Set", 27000m, 4, 3, 2 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("1efd47c7-ac74-4bb6-ad28-a99c391e872d"), null, null, 4, null, null, "Bed Cover Single", 30000m, 1, 6, 1 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("22ab0f8b-2fb1-4c00-9e7a-9ef26d620e9f"), null, null, 4, null, null, "Bed Cover Single Set", 54000m, 4, 6, 1 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("271aed3b-08bd-4bfb-9ba7-7a127132f40e"), null, null, 4, null, null, "Bed Cover Queen", 40000m, 1, 6, 1 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("2df7163c-2152-409e-903e-daf7a1851931"), null, null, 1, null, null, "Selimut Tipis", 10000m, 1, 3, 2 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("41374d56-7c98-44f1-8aaa-5cd560c53fa3"), null, null, 4, null, null, "Bed Cover King", 50000m, 1, 6, 1 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("49eaac18-5dc7-47af-9833-e5bf564e8329"), null, null, 4, null, null, "Selimut Tipis", 20000m, 1, 6, 1 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("704a120f-157a-45e4-8551-3a942c62bc40"), null, null, 1, null, null, "Bed Cover Single", 15000m, 1, 3, 2 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("846d2fe3-438a-4539-802a-6b8f0fbc000d"), null, null, 4, null, null, "Bed Cover Queen Set", 70000m, 4, 6, 1 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("a0ae3e41-e1f2-4892-9278-1c9c0971928f"), null, null, 1, null, null, "Bed Cover Queen Set", 35000m, 4, 3, 2 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("cc945e9b-70ce-46a0-bfb2-354e6af2d6a7"), null, null, 4, null, null, "Selimut", 30000m, 1, 6, 1 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("d3f83f77-51be-435c-8a83-fa5451b0dc27"), null, null, 1, null, null, "Bed Cover Queen", 20000m, 1, 3, 2 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("d4b4ed62-3165-41fd-a920-67d653f78283"), null, null, 1, null, null, "Selimut", 15000m, 1, 3, 2 },
                    { new Guid("35b4a114-f594-41be-873a-0199950a7bf0"), new Guid("d9a17633-28b3-40e8-9a43-b8d5a1a52980"), null, null, 1, null, null, "Bed Cover King Set", 38000m, 4, 3, 2 },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), new Guid("08684242-39e7-4783-9883-da6b1bea6e9d"), null, null, 1, null, null, "Alas Kasur Single", 15000m, 1, 3, 2 },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), new Guid("543f7e32-707a-4814-b1bf-e49603c8d97f"), null, null, 1, null, null, "Alas Kasur Queen", 20000m, 1, 3, 2 },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), new Guid("9bf4aa8f-8c44-488b-9045-5e18928d3e9b"), null, null, 1, null, null, "Sprei Single Set", 15000m, 4, 3, 2 },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), new Guid("a365cb46-40d0-4c46-9572-d7c3946d4948"), null, null, 1, null, null, "Sprei Single", 10000m, 1, 3, 2 },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), new Guid("c6fd370e-ecbd-46cc-8b76-1a69987d273b"), null, null, 1, null, null, "Sprei Quen Set", 20000m, 4, 3, 2 },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), new Guid("d1beca33-b4e4-48cb-ad40-cfd9ee64fb32"), null, null, 1, null, null, "Sprei King Set", 20000m, 4, 3, 2 },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), new Guid("daecf311-1db8-4d73-b372-e586079363dd"), null, null, 1, null, null, "Sprei Queen", 15000m, 1, 3, 2 },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), new Guid("dbb8bac3-e0cc-4dc1-930e-a01cd3763013"), null, null, 1, null, null, "Sprei King", 15000m, 1, 3, 2 },
                    { new Guid("515c63fc-9d49-4f50-bd59-baa5397a0829"), new Guid("eb7308dc-6ae3-43b8-bc6f-7f26d4c82bf7"), null, null, 1, null, null, "Alas Kasur King", 25000m, 1, 3, 2 },
                    { new Guid("5a432017-9a14-4fc7-a711-07fadb625cbc"), new Guid("855be10a-2c23-4249-8660-f0b16b16c7b9"), null, null, 1, null, null, "Sepatu", 25000m, 1, 2, 2 },
                    { new Guid("5a432017-9a14-4fc7-a711-07fadb625cbc"), new Guid("c71fb57c-7dd0-4d4f-8a3f-5e094921f978"), null, null, 1, null, null, "Tas Sedang", 25000m, 1, 2, 2 },
                    { new Guid("5a432017-9a14-4fc7-a711-07fadb625cbc"), new Guid("cd06fac7-869c-42b5-b423-80401f2626c6"), null, null, 1, null, null, "Tas Besar", 30000m, 1, 2, 2 },
                    { new Guid("5a432017-9a14-4fc7-a711-07fadb625cbc"), new Guid("d424edac-90fb-4791-a3c5-3c994b8bc600"), null, null, 1, null, null, "Tas Mini", 10000m, 1, 2, 2 },
                    { new Guid("5a432017-9a14-4fc7-a711-07fadb625cbc"), new Guid("ff87e2c7-aced-4b16-82c8-c32ab733a78c"), null, null, 1, null, null, "Tas Kecil", 15000m, 1, 2, 2 },
                    { new Guid("89d1c832-b679-4e9d-87fe-36e1787f6aa2"), new Guid("07bf165f-5500-4fba-84f6-24fb76c390fc"), null, null, 4, null, null, "Express", 10000m, 2, 6, 1 },
                    { new Guid("89d1c832-b679-4e9d-87fe-36e1787f6aa2"), new Guid("9dee9011-2587-4072-b518-13bdd1a48977"), null, null, 1, null, null, "Reguler", 6000m, 2, 2, 2 },
                    { new Guid("89d1c832-b679-4e9d-87fe-36e1787f6aa2"), new Guid("db1271dc-b3aa-4744-981d-53972af603a6"), null, null, 2, null, null, "One Day", 8000m, 2, 1, 2 },
                    { new Guid("a7e40294-dcc2-46f6-a093-0819f3901a6c"), new Guid("7e057a1c-a792-4511-9b9c-a7204c7b76d5"), null, null, 0, null, null, "100 Kgs", 550000m, 8, 0, 0 },
                    { new Guid("a7e40294-dcc2-46f6-a093-0819f3901a6c"), new Guid("8b4f8ada-aef1-4e7a-a786-d48de73afb64"), null, null, 0, null, null, "25 Kgs", 140000m, 8, 0, 0 },
                    { new Guid("a7e40294-dcc2-46f6-a093-0819f3901a6c"), new Guid("d86e17a5-ed36-4f8a-b1a3-eb884b4ec579"), null, null, 0, null, null, "50 Kgs", 275000m, 8, 0, 0 },
                    { new Guid("f7e5faa1-7cae-4c96-8878-31962d77826b"), new Guid("7a0db096-9368-4fe5-826c-91173a1f4fc2"), null, null, 4, null, null, "Express", 9000m, 2, 6, 1 },
                    { new Guid("f7e5faa1-7cae-4c96-8878-31962d77826b"), new Guid("af2edb70-d33f-4444-b21c-81feae35ef70"), null, null, 2, null, null, "One Day", 7000m, 2, 1, 2 },
                    { new Guid("f7e5faa1-7cae-4c96-8878-31962d77826b"), new Guid("ce8592e8-648e-468d-9729-ebab5680032b"), null, null, 1, null, null, "Reguler", 5000m, 2, 2, 2 }
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
                name: "IX_inventory_ItemNo",
                table: "inventory",
                column: "ItemNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_journal_entry_TransactionNo",
                table: "journal_entry",
                column: "TransactionNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_journal_item_ChartOfAccountId",
                table: "journal_item",
                column: "ChartOfAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_laundry_service_Name",
                table: "laundry_service",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_menu_MenuCategoryId_MenuName",
                table: "menu",
                columns: new[] { "MenuCategoryId", "MenuName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_menu_category_CategoryName",
                table: "menu_category",
                column: "CategoryName",
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
                name: "IX_user_menu_MenuId",
                table: "user_menu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_ApplicationRoleId",
                table: "user_role",
                column: "ApplicationRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "journal_item");

            migrationBuilder.DropTable(
                name: "purchase_detail");

            migrationBuilder.DropTable(
                name: "sales_detail");

            migrationBuilder.DropTable(
                name: "sales_payment");

            migrationBuilder.DropTable(
                name: "user_menu");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "chart_of_account");

            migrationBuilder.DropTable(
                name: "journal_entry");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "purchase_header");

            migrationBuilder.DropTable(
                name: "price_menu");

            migrationBuilder.DropTable(
                name: "sales_header");

            migrationBuilder.DropTable(
                name: "menu");

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
                name: "menu_category");

            migrationBuilder.DropTable(
                name: "currency");
        }
    }
}
