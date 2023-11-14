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
                name: "general_journal_header",
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
                    table.PrimaryKey("PK_general_journal_header", x => x.Id);
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
                name: "general_journal_detail",
                columns: table => new
                {
                    GeneralJournalHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralJournalDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_general_journal_detail", x => new { x.GeneralJournalHeaderId, x.GeneralJournalDetailId });
                    table.ForeignKey(
                        name: "FK_general_journal_detail_chart_of_account_ChartOfAccountId",
                        column: x => x.ChartOfAccountId,
                        principalTable: "chart_of_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_general_journal_detail_general_journal_header_GeneralJournalHeaderId",
                        column: x => x.GeneralJournalHeaderId,
                        principalTable: "general_journal_header",
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
                    { new Guid("126bba2e-af19-4b10-8987-9cb6ef6ddbcc"), "Asset", 100, "Sewa dibayar di muka", 115, null, null, null, null, null },
                    { new Guid("20e9e0be-7446-41a0-9893-584d45b80175"), "Asset", 100, "Kas", 111, null, null, null, null, null },
                    { new Guid("3808a200-1676-4e3b-8b0f-ece3b72daa61"), "Liabilitas", 200, "Utang Usaha", 201, null, null, null, null, null },
                    { new Guid("5745b241-3359-455f-9eec-f05fa4100458"), "Asset", 100, "Bank", 112, null, null, null, null, null },
                    { new Guid("76b37fdd-f6b2-4744-8ee1-9325ee315013"), "Asset", 100, "Peralatan", 121, null, null, null, null, null },
                    { new Guid("7e258f97-8875-4ee7-a121-c8a7b604930e"), "Pengeluaran", 600, "Beban Depresiasi", 606, null, null, null, null, null },
                    { new Guid("7f9d2f5b-b3ca-4ab6-828b-ee0bdb3afc13"), "Pengeluaran", 600, "Beban Utilitas", 603, null, null, null, null, null },
                    { new Guid("8e716434-a6a7-4ab7-81c7-c673a8e405c4"), "Pengeluaran", 600, "Beban Perlengkapan", 605, null, null, null, null, null },
                    { new Guid("91effaa7-34f4-4527-960e-100decafd25c"), "Harga Pokok Penjualan", 500, "Pembelian", 501, null, null, null, null, null },
                    { new Guid("9472ca38-f325-4e7e-8df2-a8335647fb16"), "Asset", 100, "Perlengkapan", 114, null, null, null, null, null },
                    { new Guid("9c7b506c-04fb-45e9-af53-d53c44f9f6b1"), "Pengeluaran", 600, "Beban Sewa", 602, null, null, null, null, null },
                    { new Guid("9f275366-66bc-4a84-b6bb-4d97cc19a19f"), "Pendapatan", 400, "Pendapatan Penjualan", 401, null, null, null, null, null },
                    { new Guid("c5bb4cb1-58f9-44ea-99ad-832bc3d8aea1"), "Asset", 100, "Persediaan", 113, null, null, null, null, null },
                    { new Guid("c8005361-1a02-4f01-a1c7-1302a2070a8b"), "Harga Pokok Penjualan", 500, "Persediaan Akhir", 521, null, null, null, null, null },
                    { new Guid("d1e17d8f-992f-4122-8dd7-de46a057d615"), "Harga Pokok Penjualan", 500, "Persediaan Awal", 511, null, null, null, null, null },
                    { new Guid("d86a09a8-9275-4d49-8ebc-9247a0e1e294"), "Ekuitas", 300, "Ekuitas Pemilik Usaha", 301, null, null, null, null, null },
                    { new Guid("d9328084-7759-44a4-ab91-2a0f1d53818b"), "Pengeluaran", 600, "Beban Listrik", 604, null, null, null, null, null },
                    { new Guid("e49061b6-4e61-4114-8118-c5e753a87fc6"), "Pengeluaran", 600, "Beban Gaji", 601, null, null, null, null, null },
                    { new Guid("f4e5f9c5-87ba-43c3-ab16-14fa6e6a3c7f"), "Asset", 100, "Akumulasi Depresiasi - Mesin Cuci", 122, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "currency",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "CultureName", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("4e5f5870-e543-4d6b-82ae-c582f9dd746d"), "USD", null, null, "en-US", null, null, "Dollar USD" },
                    { new Guid("5986c9b3-b392-46b0-82b6-0fe6f0544383"), "MYR", null, null, "ms-MY", null, null, "Ringgit Malaysia" },
                    { new Guid("c0bc3aee-031e-4c4e-bd51-5bb1fc0f2129"), "IDR", null, null, "id-ID", null, null, "Indonesia Rupiah" },
                    { new Guid("c83607a0-aa7f-40b6-a5be-bb7e11cbabe6"), "SGD", null, null, "en-SG", null, null, "Dollar Singapore" }
                });

            migrationBuilder.InsertData(
                table: "laundry_service",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "LaundryProcess", "Name" },
                values: new object[,]
                {
                    { new Guid("1278cc21-6dd5-4524-be6e-70abf62fa72d"), null, null, null, null, 3, "BANTAL/BONEKA" },
                    { new Guid("1bc86c21-a291-464a-9f1b-cf1242593d62"), null, null, null, null, 4, "PAKET BULANAN SETRIKA" },
                    { new Guid("253194b4-064a-45db-ba44-9105a26e1065"), null, null, null, null, 3, "KARPET/GORDEN" },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), null, null, null, null, 3, "BED COVER & SELIMUT" },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), null, null, null, null, 3, "SATUAN" },
                    { new Guid("79d6307f-0a2d-434b-9369-14ca2a7b0964"), null, null, null, null, 7, "KILOAN SETRIKA/CUCI LIPAT" },
                    { new Guid("9dcfba5f-8deb-48fb-8e7a-1e89972576f1"), null, null, null, null, 7, "KILOAN LENGKAP" },
                    { new Guid("b8274b3f-b266-4f73-ab55-1697442c2334"), null, null, null, null, 7, "PAKET BULANAN LENGKAP" },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), null, null, null, null, 3, "SPREI & ALAS KASUR" },
                    { new Guid("e9694b85-29ec-4e0f-8a5b-6c9aeadf43b8"), null, null, null, null, 3, "SEPATU DAN TAS" }
                });

            migrationBuilder.InsertData(
                table: "menu_category",
                columns: new[] { "Id", "CategoryDisplayName", "CategoryName", "CategorySequence" },
                values: new object[,]
                {
                    { new Guid("1be8a5b4-43e9-4f25-a737-3ba50ed86e2d"), "Purchase", "purchase", 50 },
                    { new Guid("25c10c35-315e-4ee5-a896-8ec287d13ee0"), "Master Data", "master-data", 30 },
                    { new Guid("2b125185-f2d4-40be-81de-adbbf8932d6d"), "General Journal", "general-journal", 40 },
                    { new Guid("5f88078c-c20c-4b3c-800e-a976c1140b02"), "Sales", "sales", 60 },
                    { new Guid("620f0bfd-5891-4d19-9add-0645599726a3"), "Transaction", "transaction", 20 },
                    { new Guid("7438f325-b9ba-45cb-b140-98ef709d1242"), "Dashboard", "dashboard", 10 }
                });

            migrationBuilder.InsertData(
                table: "menu",
                columns: new[] { "Id", "MenuCategoryId", "MenuDisplayName", "MenuName", "MenuSequence" },
                values: new object[,]
                {
                    { new Guid("2e6f34c4-8ab6-4b2b-a3dc-34fee9f5fca0"), new Guid("25c10c35-315e-4ee5-a896-8ec287d13ee0"), "Menu Harga", "price-menu", 60 },
                    { new Guid("60121a4c-2615-471f-a7b1-f3323dbd5a1b"), new Guid("25c10c35-315e-4ee5-a896-8ec287d13ee0"), "Tipe Laundry", "laundry-service", 50 },
                    { new Guid("6a18aaa5-0e3f-4bbb-8efe-1ca28dc358db"), new Guid("25c10c35-315e-4ee5-a896-8ec287d13ee0"), "Pemasok", "supplier", 30 },
                    { new Guid("798c3387-73d8-4d87-9166-6fee51467196"), new Guid("620f0bfd-5891-4d19-9add-0645599726a3"), "Tambah Pengeluaran", "expense", 20 },
                    { new Guid("80eee7db-4076-4945-8868-ff5adfa7ce3b"), new Guid("25c10c35-315e-4ee5-a896-8ec287d13ee0"), "Chart of Account", "chart-of-account", 10 },
                    { new Guid("a4d2bbbb-b728-4ec4-98a1-0c9c13fc93ab"), new Guid("25c10c35-315e-4ee5-a896-8ec287d13ee0"), "Persediaan", "inventory", 40 },
                    { new Guid("a9664959-e933-4ba0-8a2a-dda86703bfc7"), new Guid("1be8a5b4-43e9-4f25-a737-3ba50ed86e2d"), "Pembelian", "purchase", 10 },
                    { new Guid("ac5cc0b3-6c6e-4b59-a8a9-14bd57f37818"), new Guid("25c10c35-315e-4ee5-a896-8ec287d13ee0"), "Pelanggan", "customer", 20 },
                    { new Guid("b3fb0010-bff0-4e76-8916-32855e7500ab"), new Guid("5f88078c-c20c-4b3c-800e-a976c1140b02"), "Penjualan", "sales", 10 },
                    { new Guid("c210a099-6c14-4800-985b-606d9e28d436"), new Guid("620f0bfd-5891-4d19-9add-0645599726a3"), "Tambah Transaksi", "transaction", 10 },
                    { new Guid("ca109359-d856-41ed-986c-36f03365b2c7"), new Guid("2b125185-f2d4-40be-81de-adbbf8932d6d"), "Jurnal Umum", "general-journal", 10 },
                    { new Guid("ee558897-5562-4092-8e67-1dece1677335"), new Guid("7438f325-b9ba-45cb-b140-98ef709d1242"), "Dashboard", "dashboard", 10 },
                    { new Guid("faac0c52-e69f-4ae3-a3b5-e0f4a437498b"), new Guid("5f88078c-c20c-4b3c-800e-a976c1140b02"), "Pembayaran Penjualan", "sales-payment", 20 }
                });

            migrationBuilder.InsertData(
                table: "price_menu",
                columns: new[] { "LaundryServiceId", "PriceMenuId", "Created", "CreatedBy", "DeliveryOption", "LastModified", "LastModifiedBy", "Name", "Price", "PricingOption", "ProcessingTime", "TimeUnit" },
                values: new object[,]
                {
                    { new Guid("1bc86c21-a291-464a-9f1b-cf1242593d62"), new Guid("4bfbb1f2-bab1-48e1-8004-fe711d3c2db0"), null, null, 0, null, null, "100 Kgs", 475000m, 8, 0, 0 },
                    { new Guid("1bc86c21-a291-464a-9f1b-cf1242593d62"), new Guid("90cb7a61-7b4b-4c11-8de9-bdceaaeb67bf"), null, null, 0, null, null, "50 Kgs", 240000m, 8, 0, 0 },
                    { new Guid("1bc86c21-a291-464a-9f1b-cf1242593d62"), new Guid("a301b3cf-0501-4be8-a3d7-799324a8dcad"), null, null, 0, null, null, "25 Kgs", 120000m, 8, 0, 0 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("21e00ace-3d3d-4d7d-9fc0-fada6f52c3c6"), null, null, 1, null, null, "Bed Cover King", 25000m, 1, 3, 2 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("26b20d9e-d00b-412f-bd13-8e7d58336ec0"), null, null, 4, null, null, "Bed Cover King Set", 76000m, 4, 6, 1 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("3dabc956-38e5-4dfe-ab06-20ba116b5699"), null, null, 4, null, null, "Bed Cover Queen", 40000m, 1, 6, 1 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("5050f5fe-8ade-4a37-83dc-aee897fa3ba1"), null, null, 4, null, null, "Bed Cover Single Set", 54000m, 4, 6, 1 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("561db35f-040d-4b3d-a12b-3a71c8ed78d1"), null, null, 4, null, null, "Selimut", 30000m, 1, 6, 1 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("5823c7a2-6f8d-496b-8986-67a602d007c4"), null, null, 1, null, null, "Selimut", 15000m, 1, 3, 2 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("622051d4-33cb-4846-a243-04de8261acf1"), null, null, 1, null, null, "Bed Cover Single Set", 27000m, 4, 3, 2 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("6b9465d1-2dcf-4ae4-91fb-255ab18be15b"), null, null, 1, null, null, "Selimut Tipis", 10000m, 1, 3, 2 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("90793a83-075e-4c24-a604-978d52c23b73"), null, null, 1, null, null, "Bed Cover Queen Set", 35000m, 4, 3, 2 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("952c1f81-a383-4459-828f-07fdccec5b5c"), null, null, 4, null, null, "Bed Cover Queen Set", 70000m, 4, 6, 1 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("b1164962-4589-4ab4-acf9-ef5f57c4fbe1"), null, null, 1, null, null, "Bed Cover King Set", 38000m, 4, 3, 2 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("c2cfa26c-2649-40ee-8ff4-e91e558e3cbd"), null, null, 1, null, null, "Bed Cover Single", 15000m, 1, 3, 2 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("c5b7cfe8-62dd-4b12-877d-7f2e7bd874bf"), null, null, 4, null, null, "Bed Cover Single", 30000m, 1, 6, 1 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("c6b6f8b1-f615-4a23-947d-cee89666bfca"), null, null, 1, null, null, "Bed Cover Queen", 20000m, 1, 3, 2 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("df8d34f4-a955-4b2a-8a43-e7037554106f"), null, null, 4, null, null, "Selimut Tipis", 20000m, 1, 6, 1 },
                    { new Guid("3170542f-2798-4e04-a611-513030deeb60"), new Guid("ff69965c-46b5-4e7c-87cf-1ce32d7d3ed4"), null, null, 4, null, null, "Bed Cover King", 50000m, 1, 6, 1 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("06282237-afa5-494d-a481-eb8918a39efe"), null, null, 1, null, null, "Bawahan", 10000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("103df9db-ced7-4890-b1ef-edefcce52419"), null, null, 1, null, null, "Handuk Besar", 7000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("227c686a-cf81-46b8-9674-a68a8192d819"), null, null, 1, null, null, "Atasan", 10000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("352fa960-6053-4aca-961a-94d870954527"), null, null, 1, null, null, "Blazer Set", 25000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("817659dc-6674-4b77-95a3-10416611d935"), null, null, 1, null, null, "Blazer", 15000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("9c61ed8d-45b5-4691-a7e3-0592c5f9cad3"), null, null, 1, null, null, "Jaket/Sweater", 15000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("ab8c2990-7887-4712-8116-5351904f884e"), null, null, 1, null, null, "Dress Pendek", 15000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("adf049ee-f4ce-41ce-ae3d-185acfadfb87"), null, null, 1, null, null, "Dress Panjang", 15000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("f0c22a66-79ff-4be3-bf79-436bf2fa365b"), null, null, 1, null, null, "Handuk Sedang", 6000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("f4691731-9614-4ab4-9b86-425b3e7732c1"), null, null, 1, null, null, "Jas", 20000m, 1, 2, 2 },
                    { new Guid("5855f493-a1b8-423a-8160-db046a751d3c"), new Guid("f877003c-362c-4f6d-b185-9a0327c65077"), null, null, 1, null, null, "Jas Set", 30000m, 1, 2, 2 },
                    { new Guid("79d6307f-0a2d-434b-9369-14ca2a7b0964"), new Guid("336e869a-22b2-4916-9fa3-62c35a7053ff"), null, null, 2, null, null, "One Day", 7000m, 2, 1, 2 },
                    { new Guid("79d6307f-0a2d-434b-9369-14ca2a7b0964"), new Guid("6531c6f0-d57c-4545-a970-50d19dc1d6c5"), null, null, 1, null, null, "Reguler", 5000m, 2, 2, 2 },
                    { new Guid("79d6307f-0a2d-434b-9369-14ca2a7b0964"), new Guid("88df50cd-64da-4390-8931-0c68eea0b0ce"), null, null, 4, null, null, "Express", 9000m, 2, 6, 1 },
                    { new Guid("9dcfba5f-8deb-48fb-8e7a-1e89972576f1"), new Guid("77055c24-49ff-408a-bff3-cd779550299b"), null, null, 4, null, null, "Express", 10000m, 2, 6, 1 },
                    { new Guid("9dcfba5f-8deb-48fb-8e7a-1e89972576f1"), new Guid("77ccb963-7d8e-4383-abd9-f6f1c65046fc"), null, null, 2, null, null, "One Day", 8000m, 2, 1, 2 },
                    { new Guid("9dcfba5f-8deb-48fb-8e7a-1e89972576f1"), new Guid("7b791afa-bc83-490b-8a7a-21ffa603ca7b"), null, null, 1, null, null, "Reguler", 6000m, 2, 2, 2 },
                    { new Guid("b8274b3f-b266-4f73-ab55-1697442c2334"), new Guid("690720fe-d787-45f1-8d41-6fabe1ba0114"), null, null, 0, null, null, "100 Kgs", 550000m, 8, 0, 0 },
                    { new Guid("b8274b3f-b266-4f73-ab55-1697442c2334"), new Guid("93415812-109e-4e6b-a814-2384f48b8ea6"), null, null, 0, null, null, "25 Kgs", 140000m, 8, 0, 0 },
                    { new Guid("b8274b3f-b266-4f73-ab55-1697442c2334"), new Guid("f5d7dde6-2b27-4a35-ae14-60cced8eb360"), null, null, 0, null, null, "50 Kgs", 275000m, 8, 0, 0 },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), new Guid("08f1bea0-3a28-448b-96ff-22276e9236a9"), null, null, 1, null, null, "Sprei King Set", 20000m, 4, 3, 2 },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), new Guid("110572e4-5446-4464-adb8-00bd33e8f50d"), null, null, 1, null, null, "Sprei Single", 10000m, 1, 3, 2 },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), new Guid("1eb8c6cd-a15a-4763-a33c-053afcabc955"), null, null, 1, null, null, "Alas Kasur Single", 15000m, 1, 3, 2 },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), new Guid("32f1fead-42d6-431a-8438-520e7874362e"), null, null, 1, null, null, "Sprei Queen", 15000m, 1, 3, 2 },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), new Guid("3e02d27d-1b93-4d81-affd-a30bd3087f4e"), null, null, 1, null, null, "Alas Kasur King", 25000m, 1, 3, 2 },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), new Guid("5417a310-57bb-40c4-9c35-0d567979824f"), null, null, 1, null, null, "Sprei Quen Set", 20000m, 4, 3, 2 },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), new Guid("72f41098-5f5b-40c2-b6c0-54ad4447630f"), null, null, 1, null, null, "Sprei Single Set", 15000m, 4, 3, 2 },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), new Guid("7ff3bd7c-6e24-4e2a-ba0a-df5f8204a066"), null, null, 1, null, null, "Alas Kasur Queen", 20000m, 1, 3, 2 },
                    { new Guid("bf3bbb9c-6a89-40f4-a0a7-27eb6b44e2f4"), new Guid("e8622568-908b-4b42-9fb0-52663a3b4ef3"), null, null, 1, null, null, "Sprei King", 15000m, 1, 3, 2 },
                    { new Guid("e9694b85-29ec-4e0f-8a5b-6c9aeadf43b8"), new Guid("14952d2e-9050-4317-bc01-fc7f4b4e8582"), null, null, 1, null, null, "Tas Mini", 10000m, 1, 2, 2 },
                    { new Guid("e9694b85-29ec-4e0f-8a5b-6c9aeadf43b8"), new Guid("37b22bed-6c1e-4f86-bb2d-b98d51af44ab"), null, null, 1, null, null, "Tas Besar", 30000m, 1, 2, 2 },
                    { new Guid("e9694b85-29ec-4e0f-8a5b-6c9aeadf43b8"), new Guid("65cf5865-626b-40f2-8965-fb39e2bc3aa5"), null, null, 1, null, null, "Tas Kecil", 15000m, 1, 2, 2 },
                    { new Guid("e9694b85-29ec-4e0f-8a5b-6c9aeadf43b8"), new Guid("80531b22-8b1a-4686-8cc1-8847b5f4727f"), null, null, 1, null, null, "Tas Sedang", 25000m, 1, 2, 2 },
                    { new Guid("e9694b85-29ec-4e0f-8a5b-6c9aeadf43b8"), new Guid("f5425152-0634-457e-94f5-910b4a27a5a3"), null, null, 1, null, null, "Sepatu", 25000m, 1, 2, 2 }
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
                name: "IX_general_journal_detail_ChartOfAccountId",
                table: "general_journal_detail",
                column: "ChartOfAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_general_journal_header_TransactionNo",
                table: "general_journal_header",
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
                name: "general_journal_detail");

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
                name: "general_journal_header");

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
