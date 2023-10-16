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
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CultureName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
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
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ItemPrice = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chart_of_account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountHeaderNo = table.Column<int>(type: "int", nullable: false),
                    AccountHeaderName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AccountNo = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerCode = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierCode = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "general_account_detail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralAccountHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChartOfAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general_account_detail", x => x.Id);
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
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SalesDate = table.Column<DateTime>(type: "date", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sales_detail_inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales_payment", x => x.Id);
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_detail", x => x.Id);
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
                table: "currency",
                columns: new[] { "Id", "Code", "CreatedBy", "CultureName", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("003d9172-a838-4fe4-a738-31c7482e7e39"), "EUR", null, "sms-FI", null, "Finland" },
                    { new Guid("0117e0b9-4908-447a-9644-6330d791e87a"), "COP", null, "es-CO", null, "Colombia" },
                    { new Guid("0320f2be-7d18-49f3-a389-97f8282b0b45"), "INR", null, "ta-IN", null, "India" },
                    { new Guid("04714501-34d7-4040-85c9-35bdca999efa"), "ZAR", null, "ts-ZA", null, "South Africa" },
                    { new Guid("048da342-91eb-448c-ba1b-9954654f2739"), "IRR", null, "fa-IR", null, "Iran" },
                    { new Guid("04b57335-e320-41ca-b7d8-31d4e70f8f42"), "INR", null, "or-IN", null, "India" },
                    { new Guid("04b7bb27-2197-4329-be58-d036954fc04b"), "EUR", null, "el-GR", null, "Greece" },
                    { new Guid("04dba359-e4b1-4cd9-8f3f-15f193f5ea57"), "CNY", null, "mn-Mong-CN", null, "China" },
                    { new Guid("05f075b3-b4dd-45f0-8a11-3819561f0cc3"), "BAM", null, "bs-Latn-BA", null, "Bosnia & Herzegovina" },
                    { new Guid("065c98cc-6206-4892-b2be-0ea0f33edbf2"), "NGN", null, "yo-NG", null, "Nigeria" },
                    { new Guid("0956c560-b635-4a1c-9d63-945cccde42e3"), "KWD", null, "ar-KW", null, "Kuwait" },
                    { new Guid("0995eeb6-901a-4bc7-8483-214218ef510b"), "MMK", null, "my-MM", null, "Myanmar" },
                    { new Guid("0a2b0d34-5200-4298-8558-d746aa8e1254"), "EUR", null, "eu-ES", null, "Spain" },
                    { new Guid("0b6b7bd3-032a-427f-b551-bd2818dab64f"), "MAD", null, "tzm-Tfng-MA", null, "Morocco" },
                    { new Guid("0d5b6313-1f78-4557-94f7-e644f86a0043"), "CLP", null, "arn-CL", null, "Chile" },
                    { new Guid("0d6c9b68-27fd-4650-aab5-3aa3dc657b75"), "NGN", null, "bin-NG", null, "Nigeria" },
                    { new Guid("0f8c5cbd-4938-4006-9c47-8f47a7a752ed"), "NOK", null, "se-NO", null, "Norway" },
                    { new Guid("113758ac-76e4-47e2-a582-2f8f7a6af7a2"), "HKD", null, "en-HK", null, "Hong Kong SAR" },
                    { new Guid("134d82c2-e22e-4d82-8c15-55a934f015e3"), "BOB", null, "es-BO", null, "Bolivia" },
                    { new Guid("137feb93-c9b3-42d2-a808-ac638e557047"), "EUR", null, "de-AT", null, "Austria" },
                    { new Guid("14557187-d1ba-4661-85c9-0a583ed60e65"), "USD", null, "es-PR", null, "Puerto Rico" },
                    { new Guid("158349f5-1d39-48cc-aaf5-c55090fe673b"), "IQD", null, "ar-IQ", null, "Iraq" },
                    { new Guid("16c94b92-76e0-4f7c-bbe0-486544353686"), "EUR", null, "ga-IE", null, "Ireland" },
                    { new Guid("17b6ab1a-3313-4fc4-8d12-4f44e48a9397"), "UZS", null, "uz-Latn-UZ", null, "Uzbekistan" },
                    { new Guid("17db9a73-cc6f-4cfe-a657-2d3e811c163b"), "GBP", null, "cy-GB", null, "United Kingdom" },
                    { new Guid("18fe64bb-0a27-4603-b34b-45badfa57f24"), "ZAR", null, "en-ZA", null, "South Africa" },
                    { new Guid("1b720263-1440-458f-8900-56cdd721dcaa"), "EUR", null, "gsw-FR", null, "France" },
                    { new Guid("1c730154-335f-4918-8634-c68312d131af"), "DZD", null, "ar-DZ", null, "Algeria" },
                    { new Guid("1dfd7bce-5d92-45b6-b55e-a4297183ef58"), "USD", null, "es-EC", null, "Ecuador" },
                    { new Guid("1f115aaf-bb96-43b7-bf16-eb5638e74d73"), "EUR", null, "sr-Latn-ME", null, "Montenegro" },
                    { new Guid("219e1835-9c29-4401-8720-49d0ec2a1ca7"), "MNT", null, "mn-Mong-MN", null, "Mongolia" },
                    { new Guid("24071dcc-72fc-4fb0-805e-7e9646b67075"), "SGD", null, "en-SG", null, "Singapore" },
                    { new Guid("259586d6-4a15-4523-9f94-49d374ffa96f"), "EUR", null, "gl-ES", null, "Spain" },
                    { new Guid("27f285af-03fd-4c99-a43b-3ba998967003"), "PYG", null, "es-PY", null, "Paraguay" },
                    { new Guid("289bbe22-9991-4b5a-ab08-974861cd203e"), "¤¤", null, "es-419", null, "Latin America" },
                    { new Guid("28ea78f5-156e-415d-b34b-511dcc1dd5e1"), "XOF", null, "fr-CI", null, "Côte d’Ivoire" },
                    { new Guid("2941be33-b478-49de-bb78-729103f6a502"), "RON", null, "ro-RO", null, "Romania" },
                    { new Guid("2a78b478-02e1-490f-ae93-829d2c531d47"), "ILS", null, "he-IL", null, "Israel" },
                    { new Guid("2ac84674-914e-4b99-bad7-95eec1dded0e"), "DKK", null, "da-DK", null, "Denmark" },
                    { new Guid("2f740b25-2f75-4826-abd4-cfa33374bd2b"), "EUR", null, "nl-BE", null, "Belgium" },
                    { new Guid("30d98de9-ded1-4a11-8f68-e18721fb4f7f"), "NGN", null, "ig-NG", null, "Nigeria" },
                    { new Guid("325ce1a7-37fe-40e3-937b-d613da6e48ea"), "EGP", null, "ar-EG", null, "Egypt" },
                    { new Guid("330f17fc-4f85-4691-a465-bcd4f3bf10a4"), "XCD", null, "fr-029", null, "Caribbean" },
                    { new Guid("34b93b17-4a91-4316-8593-0fac746585be"), "EUR", null, "fi-FI", null, "Finland" },
                    { new Guid("353b077b-e356-4081-9f29-02149969989f"), "MDL", null, "ru-MD", null, "Moldova" },
                    { new Guid("3a31d0ba-ae65-4fb4-b344-1b8ef8934307"), "INR", null, "kok-IN", null, "India" },
                    { new Guid("3bcac94d-3f3a-4746-b56e-1090c73b23a8"), "BZD", null, "en-BZ", null, "Belize" },
                    { new Guid("3d57a3e8-112d-443b-82a5-099b59e80755"), "OMR", null, "ar-OM", null, "Oman" },
                    { new Guid("3e52c5e2-aa34-493b-8570-99f523bb4a81"), "CHF", null, "fr-CH", null, "Switzerland" },
                    { new Guid("3f393f3b-521a-4d24-90cb-1a2ca445239e"), "IDR", null, "id-ID", null, "Indonesia" },
                    { new Guid("3f43d457-d1e1-4ccb-b3a6-53c1017d540d"), "BAM", null, "hr-BA", null, "Bosnia & Herzegovina" },
                    { new Guid("3fbcf3b4-e473-409a-a219-59b88705a270"), "EUR", null, "sk-SK", null, "Slovakia" },
                    { new Guid("3fe587a8-56a6-4502-9091-b72685cc17cb"), "ERN", null, "ti-ER", null, "Eritrea" },
                    { new Guid("41b734f6-c800-43c8-a50c-c61a53fcb6fb"), "XCD", null, "en-029", null, "Caribbean" },
                    { new Guid("4245299e-d4d9-482c-b3ad-e641f4e3027f"), "RUB", null, "ba-RU", null, "Russia" },
                    { new Guid("428aaa3a-4917-433c-9044-25b7acfdf32f"), "XOF", null, "wo-SN", null, "Senegal" },
                    { new Guid("431d5b80-d7d6-44f1-acfb-6f84c9cd906f"), "ZAR", null, "ve-ZA", null, "South Africa" },
                    { new Guid("43336ccd-3769-4cdd-8718-b52cc9f7dfd3"), "PKR", null, "sd-Arab-PK", null, "Pakistan" },
                    { new Guid("433a8477-283d-4d14-b4ff-5bfcdcb6ba03"), "USD", null, "haw-US", null, "United States" },
                    { new Guid("43fbf5fa-eff5-4dfc-9ddc-33ee110c78d9"), "SEK", null, "se-SE", null, "Sweden" },
                    { new Guid("4462d27e-c0c8-4d4b-ba8d-17e925286f10"), "EUR", null, "fr-RE", null, "Réunion" },
                    { new Guid("45db8a92-ce89-42e4-9440-ed09b4f9043d"), "KES", null, "sw-KE", null, "Kenya" },
                    { new Guid("4e5eaf1c-9e65-44b1-a70c-83159e049b9f"), "MAD", null, "fr-MA", null, "Morocco" },
                    { new Guid("4f73db89-2614-4728-af9a-d96971e44c07"), "EUR", null, "fr-BE", null, "Belgium" },
                    { new Guid("50535963-c0cb-4de9-8cb8-9703fd0f33c8"), "MNT", null, "mn-MN", null, "Mongolia" },
                    { new Guid("53fde722-fbb1-4a8a-83b3-66649e197afe"), "ZAR", null, "tn-ZA", null, "South Africa" },
                    { new Guid("547e7dbf-bc37-4f6f-95d1-14b5b8b2e80c"), "CHF", null, "it-CH", null, "Switzerland" },
                    { new Guid("54c62de0-3a8c-42d3-8e4b-1b539d7a4e94"), "EUR", null, "ca-ES", null, "Spain" },
                    { new Guid("55fba65c-b436-49f7-bf1b-b2af6f009b8a"), "DKK", null, "kl-GL", null, "Greenland" },
                    { new Guid("585afc24-3698-4503-acd7-909ec8e26380"), "EUR", null, "lv-LV", null, "Latvia" },
                    { new Guid("59c9f566-9d83-4893-bf3c-c6a12f3d0f9e"), "EUR", null, "smn-FI", null, "Finland" },
                    { new Guid("5ad182d6-0f8e-4a9f-b4f7-014855da3217"), "HTG", null, "fr-HT", null, "Haiti" },
                    { new Guid("5c9dff53-543b-47f5-85c7-7a039ca38aba"), "CAD", null, "moh-CA", null, "Canada" },
                    { new Guid("5e15f60a-55a2-41fa-b3f8-f0d5f52062f8"), "EUR", null, "fr-MC", null, "Monaco" },
                    { new Guid("5e674d47-108f-4c3a-beb2-4c3c433a3fde"), "PEN", null, "es-PE", null, "Peru" },
                    { new Guid("5ec5f52f-62e5-4074-a11f-ee3eb52d6ba1"), "EUR", null, "de-DE", null, "Germany" },
                    { new Guid("5f0c694f-b6a8-4ec8-89c7-ad7e3cda1880"), "ZAR", null, "nso-ZA", null, "South Africa" },
                    { new Guid("6066575c-b566-4806-a15c-0d64c5ea49e0"), "RSD", null, "sr-Cyrl-RS", null, "Serbia" },
                    { new Guid("61382457-6313-46ea-bc4f-8553b0c8928c"), "LKR", null, "ta-LK", null, "Sri Lanka" },
                    { new Guid("61ea25f6-3e5f-4d53-bbf3-932f0d0592f9"), "LBP", null, "ar-LB", null, "Lebanon" },
                    { new Guid("62f281d7-53b1-4352-b8a1-18f66ccb937b"), "BAM", null, "sr-Latn-BA", null, "Bosnia & Herzegovina" },
                    { new Guid("630b83f3-86aa-4077-9707-768d9cc8af07"), "INR", null, "mr-IN", null, "India" },
                    { new Guid("639e534f-59f0-489b-84e4-b013d4dd3e91"), "EUR", null, "sv-FI", null, "Finland" },
                    { new Guid("6601e161-16a5-4475-9645-51a67efc6254"), "¤¤", null, "yi-001", null, "World" },
                    { new Guid("69324869-f2da-4a83-a451-a2ba466391bd"), "EUR", null, "fr-LU", null, "Luxembourg" },
                    { new Guid("6a0830f3-d7d4-4a4c-a01e-56489c088ce5"), "MYR", null, "ms-MY", null, "Malaysia" },
                    { new Guid("6c75c63d-aa2c-4f56-9443-792690b3b814"), "MYR", null, "en-MY", null, "Malaysia" },
                    { new Guid("6d93563d-eb21-4ea7-a316-96d6d411db83"), "EUR", null, "es-ES", null, "Spain" },
                    { new Guid("6e566d9e-a7d9-4d01-b292-7abd62858ed9"), "RUB", null, "tt-RU", null, "Russia" },
                    { new Guid("6f3f5e81-ec56-4c0a-85ba-9c4e78778dca"), "ZAR", null, "xh-ZA", null, "South Africa" },
                    { new Guid("70910d60-e7d7-4320-9100-6f30e736f639"), "PYG", null, "gn-PY", null, "Paraguay" },
                    { new Guid("715cdb25-f69b-4833-ad2d-a68adf15535e"), "TRY", null, "tr-TR", null, "Turkey" },
                    { new Guid("72ae4287-628f-4fc8-8bb7-f528f65f0f35"), "GEL", null, "ka-GE", null, "Georgia" },
                    { new Guid("72d153cb-773b-4e4f-a0d6-0decb30e4e12"), "VND", null, "vi-VN", null, "Vietnam" },
                    { new Guid("7346e313-0f43-415e-90b1-561dc41bf005"), "USD", null, "es-US", null, "United States" },
                    { new Guid("73917117-c481-4b69-bac4-5e5bfc5b0a13"), "NOK", null, "nn-NO", null, "Norway" },
                    { new Guid("7455f378-1773-418e-9e8e-bbf1243ebc12"), "BDT", null, "bn-BD", null, "Bangladesh" },
                    { new Guid("74c029ea-2ad7-4c4b-a363-edc462383838"), "USD", null, "es-SV", null, "El Salvador" },
                    { new Guid("74dc41c8-dfed-4cf2-808c-ade3f10d22de"), "RUB", null, "sah-RU", null, "Russia" },
                    { new Guid("7637b984-3243-452b-b7fb-ae2f62ca5178"), "EUR", null, "de-LU", null, "Luxembourg" },
                    { new Guid("76c29fac-1bb5-44a9-9244-ac173c7a5d9a"), "XAF", null, "fr-CM", null, "Cameroon" },
                    { new Guid("76f589af-7dce-4a4f-928d-36135e02d8d1"), "SEK", null, "sv-SE", null, "Sweden" },
                    { new Guid("7808c69d-938f-4a6c-a45f-698a6b222cf3"), "DOP", null, "es-DO", null, "Dominican Republic" },
                    { new Guid("78224db3-5d65-4257-861b-f00bab0fd456"), "EUR", null, "br-FR", null, "France" },
                    { new Guid("798ef6c3-ed26-4d29-b523-6e3ccc16ac44"), "TND", null, "ar-TN", null, "Tunisia" },
                    { new Guid("7a719f50-35b4-4c5d-9b7e-6f4118ade72c"), "XOF", null, "ff-Latn-SN", null, "Senegal" },
                    { new Guid("7c7fa62e-b948-4360-8681-f03cbbc273a6"), "GBP", null, "en-GB", null, "United Kingdom" },
                    { new Guid("7cb4fe40-7b60-42fc-bb90-3c1678059728"), "HRK", null, "hr-HR", null, "Croatia" },
                    { new Guid("7cda4173-be53-48f6-afdb-d03fa267603c"), "CHF", null, "de-LI", null, "Liechtenstein" },
                    { new Guid("7e96bb6a-ec59-4b77-ad3b-b5c26eb6240a"), "EUR", null, "fr-FR", null, "France" },
                    { new Guid("80383bee-0999-4059-8aff-548eb2a890ca"), "EUR", null, "sl-SI", null, "Slovenia" },
                    { new Guid("803a8040-bf53-4911-97c7-8848c9a04df1"), "NOK", null, "nb-NO", null, "Norway" },
                    { new Guid("82929ddd-5cb2-489a-9a69-9c59594b82ad"), "INR", null, "ur-IN", null, "India" },
                    { new Guid("834a3b58-9804-47a6-8c00-a029c317fb8e"), "KHR", null, "km-KH", null, "Cambodia" },
                    { new Guid("83dc8800-afda-4be1-b6e8-53aa46821b76"), "MXN", null, "es-MX", null, "Mexico" },
                    { new Guid("86236855-bb13-4d9d-973a-b327345a5300"), "BAM", null, "bs-Cyrl-BA", null, "Bosnia & Herzegovina" },
                    { new Guid("8701e495-c26a-4bd7-b1fd-08884d0062e9"), "USD", null, "en-US", null, "United States" },
                    { new Guid("88e867e1-56c2-442e-874a-08a82160b2a5"), "PKR", null, "pa-Arab-PK", null, "Pakistan" },
                    { new Guid("8a1a6de8-2212-452e-8cdc-073d8e7b5394"), "KZT", null, "kk-KZ", null, "Kazakhstan" },
                    { new Guid("8b14ce29-c0b9-46ec-a35b-ab6be41ca88c"), "ZAR", null, "af-ZA", null, "South Africa" },
                    { new Guid("8b78e160-540a-4e47-8445-60638de1abc8"), "BAM", null, "sr-Cyrl-BA", null, "Bosnia & Herzegovina" },
                    { new Guid("8b8347aa-6943-4ef1-bae8-fa36851e7d4a"), "LYD", null, "ar-LY", null, "Libya" },
                    { new Guid("8e1f4d2d-4796-4313-8006-f9794a7961e1"), "INR", null, "sa-IN", null, "India" },
                    { new Guid("8eb7145c-37fd-44db-9c57-c4ae44830293"), "INR", null, "ml-IN", null, "India" },
                    { new Guid("8ecbd1a6-ba06-4e30-96df-367c0c11b764"), "INR", null, "te-IN", null, "India" },
                    { new Guid("8f3fb957-b4ed-417e-9fb5-54c289bec5ca"), "HUF", null, "hu-HU", null, "Hungary" },
                    { new Guid("8f7ac9a0-5c50-4630-8492-e10540f1a68d"), "ARS", null, "es-AR", null, "Argentina" },
                    { new Guid("90e2bf80-c976-4028-a04b-6fe914e8b682"), "INR", null, "ne-IN", null, "India" },
                    { new Guid("9363c5f4-8814-427b-8094-2b94d0f29a20"), "XOF", null, "fr-SN", null, "Senegal" },
                    { new Guid("93d47ad6-e6dc-4482-868f-9ca5999573f1"), "AFN", null, "ps-AF", null, "Afghanistan" },
                    { new Guid("943dd73f-6f3e-44a0-99c4-11633d536986"), "UZS", null, "uz-Cyrl-UZ", null, "Uzbekistan" },
                    { new Guid("94732da7-f5a4-48f5-bdeb-0f6656045951"), "ISK", null, "is-IS", null, "Iceland" },
                    { new Guid("949fb2ea-6279-4420-b377-82bd3f8550d3"), "MVR", null, "dv-MV", null, "Maldives" },
                    { new Guid("97008ff7-8187-436a-bf2b-c9bb742212ce"), "THB", null, "th-TH", null, "Thailand" },
                    { new Guid("980ed62f-badc-4989-9475-7cdc62184a72"), "CHF", null, "rm-CH", null, "Switzerland" },
                    { new Guid("9a04e0b0-f166-466e-8614-caac6aecff87"), "JPY", null, "ja-JP", null, "Japan" },
                    { new Guid("9a783a4e-f659-463d-a09d-affd4a6587d4"), "SAR", null, "ar-SA", null, "Saudi Arabia" },
                    { new Guid("9b2bde51-fcc4-4994-b1fa-59456c333cc1"), "QAR", null, "ar-QA", null, "Qatar" },
                    { new Guid("9b9cbea5-863a-4ba1-bfe1-c2beed6789b7"), "IDR", null, "en-ID", null, "Indonesia" },
                    { new Guid("9ba45779-1c75-449c-9576-187e23f31434"), "AZN", null, "az-Cyrl-AZ", null, "Azerbaijan" },
                    { new Guid("9c2dfacf-d62c-4e1e-b510-5dbae6e67845"), "NOK", null, "smj-NO", null, "Norway" },
                    { new Guid("9c4f8acf-f11e-41dd-8cef-a305518b201a"), "CZK", null, "cs-CZ", null, "Czechia" },
                    { new Guid("9cc5b8d7-53ac-49f7-b173-980284805a1d"), "EUR", null, "co-FR", null, "France" },
                    { new Guid("9cd19c5e-1533-43e0-b183-ca1531da3a1c"), "PKR", null, "ur-PK", null, "Pakistan" },
                    { new Guid("9d22fd49-c9d5-49fa-918e-bed7e9ab6df4"), "INR", null, "kn-IN", null, "India" },
                    { new Guid("9d926b25-ed32-4d88-9370-745fa5a2df73"), "JMD", null, "en-JM", null, "Jamaica" },
                    { new Guid("a1d71016-4fb1-4f88-aebe-f3049892ea82"), "TMT", null, "tk-TM", null, "Turkmenistan" },
                    { new Guid("a245abf4-5763-40ca-affa-1f4a7329aa0a"), "AZN", null, "az-Latn-AZ", null, "Azerbaijan" },
                    { new Guid("a3c0aff3-d194-45c7-9648-6a4e04cd13a5"), "AMD", null, "hy-AM", null, "Armenia" },
                    { new Guid("a4211128-91af-4dcf-adf5-10a320b87568"), "MAD", null, "tzm-Arab-MA", null, "Morocco" },
                    { new Guid("a602e514-819f-4cd3-a850-68052e2d8249"), "NZD", null, "en-NZ", null, "New Zealand" },
                    { new Guid("a6a966ed-1dd7-4c4d-af57-2204dcd9eb5d"), "NPR", null, "ne-NP", null, "Nepal" },
                    { new Guid("a7618450-2637-47fd-b0cc-078a5f3be2f8"), "EUR", null, "it-IT", null, "Italy" },
                    { new Guid("a8dfb430-f738-4560-90d5-f3f62b888316"), "DKK", null, "fo-FO", null, "Faroe Islands" },
                    { new Guid("a9387514-bc32-40b2-b222-350eb7c2b44f"), "XCD", null, "pap-029", null, "Caribbean" },
                    { new Guid("a951cad2-95af-4aab-9ad6-9a2b0b43c09f"), "MKD", null, "mk-MK", null, "North Macedonia" },
                    { new Guid("aa6450ae-3e27-4d40-ac9a-0d43dec6c866"), "EUR", null, "dsb-DE", null, "Germany" },
                    { new Guid("ab5a87fc-2191-49df-bc7b-05658ecc0d55"), "CUP", null, "es-CU", null, "Cuba" },
                    { new Guid("ac8a3b0d-bab9-4bd8-8e4a-06e1a58b620f"), "UYU", null, "es-UY", null, "Uruguay" },
                    { new Guid("ae1e0090-a8ca-4da6-b932-bc10b14c7135"), "CNY", null, "bo-CN", null, "China" },
                    { new Guid("aebe7caa-4fc5-48f0-906b-79001ba6e055"), "LAK", null, "lo-LA", null, "Laos" },
                    { new Guid("af533090-3333-418d-8e22-f286ef924dab"), "GBP", null, "gd-GB", null, "United Kingdom" },
                    { new Guid("b2fbb3f3-74b0-4f9a-906b-cb3bd3eb5bb6"), "RUB", null, "ru-RU", null, "Russia" },
                    { new Guid("b365e4f8-d7cc-46d8-938d-c4b007bf87e8"), "BGN", null, "bg-BG", null, "Bulgaria" },
                    { new Guid("b3e3c5f0-f615-4ea1-852a-102a988df85e"), "MAD", null, "ar-MA", null, "Morocco" },
                    { new Guid("b5969d76-937f-4019-9b5f-820c36ea5f8e"), "BHD", null, "ar-BH", null, "Bahrain" },
                    { new Guid("b6a8fff7-67a0-4a08-9a76-df2581d24811"), "SOS", null, "so-SO", null, "Somalia" },
                    { new Guid("b7065234-1f11-41af-84d0-3f4dac806dd1"), "SEK", null, "smj-SE", null, "Sweden" },
                    { new Guid("b75ca32c-c061-4203-b6dd-5efcd7516369"), "EUR", null, "hsb-DE", null, "Germany" },
                    { new Guid("b85d4eb9-ba5e-4ab7-b643-641713d76f40"), "GTQ", null, "es-GT", null, "Guatemala" },
                    { new Guid("b978ea41-da26-4c9f-abac-7a099121682e"), "KGS", null, "ky-KG", null, "Kyrgyzstan" },
                    { new Guid("b9a20790-b58a-4cdb-a9a8-71a564555a52"), "ETB", null, "am-ET", null, "Ethiopia" },
                    { new Guid("bb18d6c9-d70a-4d53-b778-4963ce270bc2"), "CDF", null, "fr-CD", null, "Congo (DRC)" },
                    { new Guid("bbeed0c5-e14d-4815-8a66-04235bf3bcec"), "INR", null, "ks-Deva-IN", null, "India" },
                    { new Guid("bca1fa8a-0cf3-41ed-b407-540b5f373837"), "BTN", null, "dz-BT", null, "Bhutan" },
                    { new Guid("bce28c60-bf5c-4134-bdaa-7ee2b800fe67"), "RWF", null, "rw-RW", null, "Rwanda" },
                    { new Guid("be164980-8c0a-425c-a23a-185df9893d42"), "RSD", null, "sr-Latn-RS", null, "Serbia" },
                    { new Guid("be93ba16-6321-4e70-a515-3f363a389bb8"), "KRW", null, "ko-KR", null, "Korea" },
                    { new Guid("beb47c01-1f72-4325-8e4e-9540010bb410"), "NIO", null, "es-NI", null, "Nicaragua" },
                    { new Guid("bf9c02a8-0e94-48f6-9a3e-90fae58f018b"), "CAD", null, "iu-Latn-CA", null, "Canada" },
                    { new Guid("c1307f6a-4221-463e-942d-f13b74ce056c"), "CLP", null, "es-CL", null, "Chile" },
                    { new Guid("c17f6e72-803d-4d3e-837f-a6e8bd39f8ba"), "ETB", null, "ti-ET", null, "Ethiopia" },
                    { new Guid("c29284f8-e457-4f0d-b50c-082ddb1249c5"), "EUR", null, "se-FI", null, "Finland" },
                    { new Guid("c3e0a40e-934b-4677-b4ee-97f222202aaa"), "BYN", null, "be-BY", null, "Belarus" },
                    { new Guid("c4dcabdc-afb1-4c16-a56a-b2379e0f5bba"), "INR", null, "en-IN", null, "India" },
                    { new Guid("c602acf8-913e-4b79-9944-f3f249d1aa13"), "INR", null, "as-IN", null, "India" },
                    { new Guid("c656f2c3-53ed-4bca-bbec-88475c4a0379"), "PHP", null, "fil-PH", null, "Philippines" },
                    { new Guid("c9665e60-9372-4b16-b4de-77542782ad9c"), "CNY", null, "ug-CN", null, "China" },
                    { new Guid("ca35fedb-d52b-441e-9e43-5a7ebf620a38"), "LKR", null, "si-LK", null, "Sri Lanka" },
                    { new Guid("ca6ae518-7e6e-4116-87b0-b07ee093b184"), "EUR", null, "lt-LT", null, "Lithuania" },
                    { new Guid("cb6da3b8-06f7-48da-8481-a97ffad77809"), "VES", null, "es-VE", null, "Venezuela" },
                    { new Guid("cdb9acea-5c7a-4d06-8fe2-453b023c3b3e"), "EUR", null, "pt-PT", null, "Portugal" },
                    { new Guid("cf5c8611-d1a0-4c4c-90f1-a5a9ed6f6220"), "INR", null, "hi-IN", null, "India" },
                    { new Guid("d00a4695-e799-4f00-b418-27bc5f2e51c0"), "AUD", null, "en-AU", null, "Australia" },
                    { new Guid("d15cafb2-e6a9-42fb-b1c1-491d5263276d"), "NZD", null, "mi-NZ", null, "New Zealand" },
                    { new Guid("d22a5227-2e1b-4b65-82d1-53b9f2950d50"), "NGN", null, "ibb-NG", null, "Nigeria" },
                    { new Guid("d3936439-71e0-4462-b594-dc87b2b7d112"), "ZAR", null, "zu-ZA", null, "South Africa" },
                    { new Guid("d4bf91fe-77e7-40a1-8964-f1fcf191677a"), "UAH", null, "uk-UA", null, "Ukraine" },
                    { new Guid("d602adf5-cf5e-4247-9c39-7fff205b6e30"), "BWP", null, "tn-BW", null, "Botswana" },
                    { new Guid("d8b4d8bd-ebf5-4fff-adb8-e3fcf005f661"), "CNY", null, "ii-CN", null, "China" },
                    { new Guid("d95e2c49-f788-40a5-9f69-e21b3319c609"), "XOF", null, "fr-ML", null, "Mali" },
                    { new Guid("d99572dd-58e0-4eb0-b619-6d351082fd8b"), "INR", null, "sd-Deva-IN", null, "India" },
                    { new Guid("d9f68591-9aca-4890-9f9c-a80670a091ca"), "SEK", null, "sma-SE", null, "Sweden" },
                    { new Guid("da249049-6e6e-4878-8595-2ca9e2d68ea5"), "SYP", null, "ar-SY", null, "Syria" },
                    { new Guid("db6bcfc6-3c0c-4614-8e4f-4bdbc86265a9"), "JOD", null, "ar-JO", null, "Jordan" },
                    { new Guid("dc53d73b-4c3c-4009-be01-e7c5c6e9d963"), "ALL", null, "sq-AL", null, "Albania" },
                    { new Guid("de882442-6133-4501-ab17-b44d3b95a314"), "PAB", null, "es-PA", null, "Panama" },
                    { new Guid("de9e6a50-3ca2-4bf9-b00c-0bbcad260dde"), "EUR", null, "oc-FR", null, "France" },
                    { new Guid("df1d7642-92fb-4a55-a47f-1afbbb4b670c"), "INR", null, "gu-IN", null, "India" },
                    { new Guid("df257b3d-49e8-4d5a-ba08-2049bd62a291"), "CAD", null, "fr-CA", null, "Canada" },
                    { new Guid("e02c3dbb-ce6b-4d36-acf4-a123db620a5e"), "CHF", null, "de-CH", null, "Switzerland" },
                    { new Guid("e1934225-0d48-4c0f-ae77-c547514ddda0"), "ETB", null, "om-ET", null, "Ethiopia" },
                    { new Guid("e22c60e7-5868-4372-be45-4e27b874bce8"), "BND", null, "ms-BN", null, "Brunei" },
                    { new Guid("e2d7e382-bc1d-4cdf-b8d2-5cd1e531c910"), "HNL", null, "es-HN", null, "Honduras" },
                    { new Guid("e2e4caea-ac8f-4c80-a7cc-a8bb0ed55e44"), "EUR", null, "mt-MT", null, "Malta" },
                    { new Guid("e623534f-8670-43dd-a6f8-008deb9e069d"), "PHP", null, "en-PH", null, "Philippines" },
                    { new Guid("e90c1b7a-01b2-40dc-b728-a2bb1734aa07"), "SYP", null, "syr-SY", null, "Syria" },
                    { new Guid("ea075b87-e49a-4ae9-a3ee-04f59d8bd3a8"), "YER", null, "ar-YE", null, "Yemen" },
                    { new Guid("eb4d08e4-ee99-44d5-aa35-89bad9ba11b8"), "USD", null, "en-ZW", null, "Zimbabwe" },
                    { new Guid("ee52bcc5-dc8c-448e-8b82-19d6274ff5a6"), "MDL", null, "ro-MD", null, "Moldova" },
                    { new Guid("ef69a41b-576f-474a-ab16-ebead3583f02"), "EUR", null, "en-IE", null, "Ireland" },
                    { new Guid("ef7ae94c-98ee-4ddf-a26c-a078fb642616"), "CAD", null, "en-CA", null, "Canada" },
                    { new Guid("f097eecb-ce70-4b98-b7c4-a0c0fd4ac787"), "BRL", null, "pt-BR", null, "Brazil" },
                    { new Guid("f0b5ccb4-0aa0-47dd-b052-06ed40b9e0eb"), "TTD", null, "en-TT", null, "Trinidad & Tobago" },
                    { new Guid("f18ed067-a94e-45cd-9802-2151640e0ab7"), "NOK", null, "sma-NO", null, "Norway" },
                    { new Guid("f1d2f6f6-d6f4-45ce-8d90-8d877ef6ed5d"), "EUR", null, "nl-NL", null, "Netherlands" },
                    { new Guid("f33e0966-4995-4a87-8289-5df38496d1b2"), "CRC", null, "es-CR", null, "Costa Rica" },
                    { new Guid("f460f599-1ffa-4303-83b4-cdf9ed445f32"), "INR", null, "bn-IN", null, "India" },
                    { new Guid("f66e9831-ff66-4a2b-8d86-0e8a1bd8d3b7"), "PLN", null, "pl-PL", null, "Poland" },
                    { new Guid("f71e3749-c796-426e-9391-b8b014e98bd1"), "EUR", null, "et-EE", null, "Estonia" },
                    { new Guid("f77d2a39-9bec-4d9f-9c1e-bb645e45f3e7"), "EUR", null, "sr-Cyrl-ME", null, "Montenegro" },
                    { new Guid("f80dab2f-9035-4d70-b524-49a199e60a65"), "EUR", null, "fy-NL", null, "Netherlands" },
                    { new Guid("fccbffd7-12a4-49e9-bd86-5f4a79763a3b"), "EUR", null, "lb-LU", null, "Luxembourg" },
                    { new Guid("fe0cf85f-97ce-4994-ac97-618c3381d197"), "ZAR", null, "st-ZA", null, "South Africa" },
                    { new Guid("fe57fc64-e4c0-4bb6-8bd3-3a4306757b00"), "AED", null, "ar-AE", null, "United Arab Emirates" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_account_AccountNo",
                table: "chart_of_account",
                column: "AccountNo",
                unique: true,
                filter: "[AccountNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_chart_of_account_CurrencyId",
                table: "chart_of_account",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_currency_Name_Code_CultureName",
                table: "currency",
                columns: new[] { "Name", "Code", "CultureName" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [Code] IS NOT NULL AND [CultureName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_customer_CurrencyId",
                table: "customer",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_customer_CustomerCode",
                table: "customer",
                column: "CustomerCode",
                unique: true,
                filter: "[CustomerCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_general_account_detail_ChartOfAccountId",
                table: "general_account_detail",
                column: "ChartOfAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_general_account_detail_GeneralAccountHeaderId",
                table: "general_account_detail",
                column: "GeneralAccountHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_general_account_header_TransactionNo",
                table: "general_account_header",
                column: "TransactionNo",
                unique: true,
                filter: "[TransactionNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_ItemNo",
                table: "inventory",
                column: "ItemNo",
                unique: true,
                filter: "[ItemNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_detail_InventoryId",
                table: "purchase_detail",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_detail_PurchaseHeaderId",
                table: "purchase_detail",
                column: "PurchaseHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_header_CurrencyId",
                table: "purchase_header",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_header_PurchaseNo",
                table: "purchase_header",
                column: "PurchaseNo",
                unique: true,
                filter: "[PurchaseNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_header_SupplierId",
                table: "purchase_header",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_detail_InventoryId",
                table: "sales_detail",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_detail_SalesHeaderId",
                table: "sales_detail",
                column: "SalesHeaderId");

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
                unique: true,
                filter: "[SalesNo] IS NOT NULL");

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
                unique: true,
                filter: "[SupplierCode] IS NOT NULL");
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
                name: "chart_of_account");

            migrationBuilder.DropTable(
                name: "general_account_header");

            migrationBuilder.DropTable(
                name: "purchase_header");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "sales_header");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "currency");
        }
    }
}
