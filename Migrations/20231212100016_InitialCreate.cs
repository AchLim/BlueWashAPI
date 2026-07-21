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
                name: "closing_entry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_closing_entry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                });

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
                name: "message_template",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message_template", x => x.Id);
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
                name: "journal_entry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionNo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosingEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journal_entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_journal_entry_closing_entry_ClosingEntryId",
                        column: x => x.ClosingEntryId,
                        principalTable: "closing_entry",
                        principalColumn: "Id");
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
                    MobileNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
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
                    PricingOption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessingTime = table.Column<int>(type: "int", nullable: false),
                    TimeUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryOption = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTerm = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTerm = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Discount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
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
                    PaymentDate = table.Column<DateTime>(type: "date", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Discount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "purchase_payment",
                columns: table => new
                {
                    PurchaseHeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchasePaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "date", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_payment", x => new { x.PurchasePaymentId, x.PurchaseHeaderId });
                    table.ForeignKey(
                        name: "FK_purchase_payment_purchase_header_PurchaseHeaderId",
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
                    { new Guid("0faf503b-3842-4b38-b2ab-82a09a686810"), "Ekuitas", 300, "Ekuitas Pemilik Usaha", 301, null, null, null, null, null },
                    { new Guid("11ebb164-b8e0-4757-bd08-7cc409e8faf5"), "Ekuitas", 300, "Ikhtisar Laba-Rugi", 320, null, null, null, null, null },
                    { new Guid("15c09e8c-3439-4ca6-b63e-d0d3c2c2ddeb"), "Pengeluaran", 600, "Beban Gaji", 601, null, null, null, null, null },
                    { new Guid("1fc549e7-c60f-4440-beeb-eb4d6d580992"), "Asset", 100, "Perlengkapan", 114, null, null, null, null, null },
                    { new Guid("2585e6db-9f52-41f9-b022-6bac4ee39c4e"), "Asset", 100, "Akumulasi Depresiasi - Mesin Cuci", 122, null, null, null, null, null },
                    { new Guid("2cc907e5-9f6b-4dee-a3ab-3b7699fe9dfe"), "Harga Pokok Penjualan", 500, "Potongan Pembelian", 502, null, null, null, null, null },
                    { new Guid("40ffeb73-be96-4599-8f65-d2ebc9a26c0a"), "Pengeluaran", 600, "Beban Sewa", 602, null, null, null, null, null },
                    { new Guid("5014fce0-361a-4b9f-ad3e-2f680ede0030"), "Liabilitas", 200, "Utang Dagang", 201, null, null, null, null, null },
                    { new Guid("547b9264-083c-406e-b0e7-618f0241568b"), "Pendapatan", 400, "Pendapatan Penjualan", 401, null, null, null, null, null },
                    { new Guid("573b0f53-ae55-4540-b22f-895f76605db7"), "Asset", 100, "Sewa dibayar di muka", 115, null, null, null, null, null },
                    { new Guid("77068633-1c07-46d0-b2e8-e919780693ea"), "Harga Pokok Penjualan", 500, "Pembelian", 501, null, null, null, null, null },
                    { new Guid("7993c1f9-0fcc-4ccf-b19c-2b4add5782bf"), "Asset", 100, "Persediaan", 113, null, null, null, null, null },
                    { new Guid("7a09d43a-8464-4652-bafb-daf8ed0a001c"), "Asset", 100, "Peralatan", 121, null, null, null, null, null },
                    { new Guid("7fc23dc9-8ddc-4536-978b-6b4a833a8650"), "Asset", 100, "Piutang Dagang", 116, null, null, null, null, null },
                    { new Guid("87105a71-142b-4c4f-8568-b5ff60aee6eb"), "Asset", 100, "Bank", 112, null, null, null, null, null },
                    { new Guid("8af02038-2ada-4c2e-899c-353e211049f5"), "Ekuitas", 300, "Laba Ditahan", 310, null, null, null, null, null },
                    { new Guid("913e2edf-4e3b-49fc-884c-41876f4bcdb3"), "Asset", 100, "Kas", 111, null, null, null, null, null },
                    { new Guid("b1310f15-6de2-4119-9553-3080d4a660fb"), "Harga Pokok Penjualan", 500, "Persediaan Akhir", 521, null, null, null, null, null },
                    { new Guid("be3fa0e8-7f38-4a9d-82af-1da0a64d4c70"), "Harga Pokok Penjualan", 500, "Persediaan Awal", 511, null, null, null, null, null },
                    { new Guid("d8558de6-29a8-49cc-aea2-da282660cb43"), "Pengeluaran", 600, "Beban Utilitas", 603, null, null, null, null, null },
                    { new Guid("ddc0e86d-bbb3-49dd-a982-c666f1d16575"), "Pendapatan", 400, "Potongan Penjualan", 420, null, null, null, null, null },
                    { new Guid("ebaa3442-0334-4581-9049-1cc80a600c59"), "Pengeluaran", 600, "Beban Perlengkapan", 605, null, null, null, null, null },
                    { new Guid("ed97eb75-10a0-4df6-8e6c-5c11d37ae2cb"), "Pengeluaran", 600, "Beban Depresiasi", 606, null, null, null, null, null },
                    { new Guid("f0a715d4-86ac-4db4-bd44-3561f8e5daaa"), "Pengeluaran", 600, "Beban Listrik", 604, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "company",
                columns: new[] { "Id", "Address", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "MobileNumber", "Name" },
                values: new object[] { new Guid("040731b6-c2ce-4842-b134-9a51746f2ca9"), "Ruko Puri Loka Blok E No. 5, Sei Panas, Kec. Batam Kota, Kota Batam, Kepulauan Riau 29411", null, null, null, null, "+6282283025500", "BLUE WASH" });

            migrationBuilder.InsertData(
                table: "currency",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "CultureName", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("16888cd6-6cac-4d2d-b8a4-f26d953cffe9"), "MYR", null, null, "ms-MY", null, null, "Ringgit Malaysia" },
                    { new Guid("a03ef135-1a46-42d8-a0bf-427cf2e0b463"), "SGD", null, null, "en-SG", null, null, "Dollar Singapore" },
                    { new Guid("ebbbe2c7-2089-46dd-904d-2ed01a0fc550"), "USD", null, null, "en-US", null, null, "Dollar USD" },
                    { new Guid("effcd6d9-4e3a-441f-8f85-fede6cc03967"), "IDR", null, null, "id-ID", null, null, "Indonesia Rupiah" }
                });

            migrationBuilder.InsertData(
                table: "laundry_service",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "LaundryProcess", "Name" },
                values: new object[,]
                {
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), null, null, null, null, 3, "BED COVER & SELIMUT" },
                    { new Guid("521e5061-495f-4952-96c5-49fc24d27067"), null, null, null, null, 3, "KARPET/GORDEN" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), null, null, null, null, 3, "SPREI & ALAS KASUR" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), null, null, null, null, 3, "SATUAN" },
                    { new Guid("6e3d879e-9804-47db-8550-fee599a81b5b"), null, null, null, null, 7, "PAKET BULANAN LENGKAP" },
                    { new Guid("6f02fd67-f4c3-4d73-8856-a3b3c68d9276"), null, null, null, null, 4, "PAKET BULANAN SETRIKA" },
                    { new Guid("85c8f51f-9c9b-4cb7-85ea-742c6f3d22e7"), null, null, null, null, 3, "SEPATU DAN TAS" },
                    { new Guid("ce192b81-4fcb-4d98-b570-3c556585589a"), null, null, null, null, 3, "BANTAL/BONEKA" },
                    { new Guid("d925808e-80db-4d69-9059-d2a4720cc0c8"), null, null, null, null, 7, "KILOAN LENGKAP" },
                    { new Guid("de1ce154-4c00-4f12-a593-a73634b10d67"), null, null, null, null, 7, "KILOAN SETRIKA/CUCI LIPAT" }
                });

            migrationBuilder.InsertData(
                table: "menu_category",
                columns: new[] { "Id", "CategoryDisplayName", "CategoryName", "CategorySequence" },
                values: new object[,]
                {
                    { new Guid("248f896d-1eed-49e4-b7bd-a0a0a37ffb1c"), "Dashboard", "dashboard", 10 },
                    { new Guid("3b37be0b-107b-474e-84db-7d66b4982799"), "Sales", "sales", 50 },
                    { new Guid("62cee0bc-e597-4f11-bb64-b9b1aa85f0a8"), "Accounting", "accounting", 30 },
                    { new Guid("7aa59789-75a6-4414-9daf-b64c2fb3ebcd"), "Master Data", "master-data", 20 },
                    { new Guid("b71ed8ec-6644-4f0e-862e-de4a383d36d7"), "Configuration", "configuration", 1000 },
                    { new Guid("c81512f7-e526-474d-8b52-04e2c7b8f75b"), "Report", "report", 60 },
                    { new Guid("d694f409-4f9c-4692-a42f-2094e3b09008"), "Purchase", "purchase", 40 }
                });

            migrationBuilder.InsertData(
                table: "menu",
                columns: new[] { "Id", "MenuCategoryId", "MenuDisplayName", "MenuName", "MenuSequence" },
                values: new object[,]
                {
                    { new Guid("0906c674-b075-4f2d-b54f-43236c9d03ea"), new Guid("b71ed8ec-6644-4f0e-862e-de4a383d36d7"), "Perusahaan", "company", 10 },
                    { new Guid("1878d7e6-fce4-4074-8796-3d814f4cd444"), new Guid("c81512f7-e526-474d-8b52-04e2c7b8f75b"), "Penjualan", "report-sales", 60 },
                    { new Guid("19c7a7d5-811e-4b2a-97e1-e39e791deb78"), new Guid("d694f409-4f9c-4692-a42f-2094e3b09008"), "Pembayaran Pembelian", "purchase-payment", 20 },
                    { new Guid("2eb6b50f-f53d-4924-a49d-1effae422e64"), new Guid("c81512f7-e526-474d-8b52-04e2c7b8f75b"), "Neraca Saldo", "trial-balance", 20 },
                    { new Guid("2fb96518-bfb9-4d54-89a4-45a50698c905"), new Guid("d694f409-4f9c-4692-a42f-2094e3b09008"), "Pembelian", "purchase", 10 },
                    { new Guid("32581d67-ddf0-4a97-aabe-ea486bca3e97"), new Guid("62cee0bc-e597-4f11-bb64-b9b1aa85f0a8"), "Journal Entries", "journal-entry", 10 },
                    { new Guid("37c9a181-582c-4dde-9217-98d4dffc7700"), new Guid("3b37be0b-107b-474e-84db-7d66b4982799"), "Penjualan", "sales", 10 },
                    { new Guid("38804e0c-3e19-4044-8229-bb2026970a4d"), new Guid("3b37be0b-107b-474e-84db-7d66b4982799"), "Pembayaran Penjualan", "sales-payment", 20 },
                    { new Guid("3b616efc-ea0c-4fb8-848a-f1deec14e8c3"), new Guid("7aa59789-75a6-4414-9daf-b64c2fb3ebcd"), "Chart of Account", "chart-of-account", 10 },
                    { new Guid("3c96056a-8e67-49d7-9496-e5c97ee1e09d"), new Guid("7aa59789-75a6-4414-9daf-b64c2fb3ebcd"), "Persediaan", "inventory", 40 },
                    { new Guid("42c2d2cd-3f56-4bc0-840e-8769856edc0f"), new Guid("c81512f7-e526-474d-8b52-04e2c7b8f75b"), "Posisi Keuangan", "balance-sheet", 40 },
                    { new Guid("51ef7e8c-4913-4ebe-a6d0-5dd72ec24c19"), new Guid("c81512f7-e526-474d-8b52-04e2c7b8f75b"), "Pembelian", "report-purchase", 50 },
                    { new Guid("598b71b8-338e-4fa2-95ed-b3f97cfcd252"), new Guid("b71ed8ec-6644-4f0e-862e-de4a383d36d7"), "Template WhatsApp", "message-template", 100 },
                    { new Guid("61a61cb6-6589-4efc-b2be-9a41f067cc32"), new Guid("7aa59789-75a6-4414-9daf-b64c2fb3ebcd"), "Tipe Laundry", "laundry-service", 50 },
                    { new Guid("6c8f0943-2aa5-48fe-beb3-34b6f9928c99"), new Guid("7aa59789-75a6-4414-9daf-b64c2fb3ebcd"), "Pelanggan", "customer", 20 },
                    { new Guid("73d0eb70-ccf3-4ae8-9c1c-c157839c8bbe"), new Guid("c81512f7-e526-474d-8b52-04e2c7b8f75b"), "Buku Besar", "general-ledger", 10 },
                    { new Guid("7975f8b8-9f7a-4866-bf06-608daf33a4af"), new Guid("248f896d-1eed-49e4-b7bd-a0a0a37ffb1c"), "Dashboard", "dashboard", 10 },
                    { new Guid("a282abe9-3b20-4bb1-b6d9-444d4c1294b4"), new Guid("c81512f7-e526-474d-8b52-04e2c7b8f75b"), "Laba Rugi", "income-statement", 30 },
                    { new Guid("c200839f-7af3-45c8-91bb-8b0ab7ed66a9"), new Guid("7aa59789-75a6-4414-9daf-b64c2fb3ebcd"), "Menu Harga", "price-menu", 60 },
                    { new Guid("e429e924-4275-4eff-8926-ce88c3b1dcd3"), new Guid("7aa59789-75a6-4414-9daf-b64c2fb3ebcd"), "Pemasok", "supplier", 30 },
                    { new Guid("f3bc99da-2dfb-46d9-812c-57c301058d2c"), new Guid("62cee0bc-e597-4f11-bb64-b9b1aa85f0a8"), "Closing Entries", "closing-entry", 10 }
                });

            migrationBuilder.InsertData(
                table: "price_menu",
                columns: new[] { "LaundryServiceId", "PriceMenuId", "Created", "CreatedBy", "DeliveryOption", "LastModified", "LastModifiedBy", "Name", "Price", "PricingOption", "ProcessingTime", "TimeUnit" },
                values: new object[,]
                {
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("0761f843-b8ec-4f55-bf1f-64af6de3b417"), null, null, "Express", null, null, "Bed Cover Single Set", 54000m, "Set", 6, "Hour" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("1e00fa06-7de4-4b9d-94bc-00310f15a160"), null, null, "Express", null, null, "Bed Cover Queen Set", 70000m, "Set", 6, "Hour" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("20c0b667-f491-4aae-8374-e7ed5928f9c1"), null, null, "Reguler", null, null, "Selimut", 15000m, "Unit", 3, "Day" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("3c8812fb-7759-4b2a-aa88-82a0b4116519"), null, null, "Reguler", null, null, "Bed Cover Queen", 20000m, "Unit", 3, "Day" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("433ffecc-398f-43b7-9bb4-b48e77d15b76"), null, null, "Reguler", null, null, "Bed Cover King Set", 38000m, "Set", 3, "Day" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("493d5a0f-6fa2-4815-a41c-3122274ccb97"), null, null, "Reguler", null, null, "Bed Cover Single Set", 27000m, "Set", 3, "Day" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("79346961-e03e-49ce-a356-b5f836fa4555"), null, null, "Express", null, null, "Bed Cover King Set", 76000m, "Set", 6, "Hour" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("93e89b35-55fb-4069-b60f-1f55ad37ae60"), null, null, "Reguler", null, null, "Selimut Tipis", 10000m, "Unit", 3, "Day" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("983a9b58-09f0-4d9f-b453-138ee6aa6cb5"), null, null, "Express", null, null, "Bed Cover King", 50000m, "Unit", 6, "Hour" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("aa50318f-d0a2-4c30-8c0d-dc447807311e"), null, null, "Express", null, null, "Selimut", 30000m, "Unit", 6, "Hour" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("ad7d2d80-2bee-4112-8029-88cdae7b730e"), null, null, "Express", null, null, "Selimut Tipis", 20000m, "Unit", 6, "Hour" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("aee4b233-74e4-41a2-988a-70c52ed50292"), null, null, "Express", null, null, "Bed Cover Single", 30000m, "Unit", 6, "Hour" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("b678f1a9-7aed-402f-b348-88dee8901b8e"), null, null, "Express", null, null, "Bed Cover Queen", 40000m, "Unit", 6, "Hour" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("e1ebbf40-6911-4178-b68a-1d24f7cb76ee"), null, null, "Reguler", null, null, "Bed Cover Queen Set", 35000m, "Set", 3, "Day" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("e3766c6c-6237-454b-a59d-f804daf81bc0"), null, null, "Reguler", null, null, "Bed Cover King", 25000m, "Unit", 3, "Day" },
                    { new Guid("23249048-50e4-446d-ae97-bb9993a3c09a"), new Guid("f1d1ce2e-5555-481b-8a63-c1fec3b5da4d"), null, null, "Reguler", null, null, "Bed Cover Single", 15000m, "Unit", 3, "Day" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), new Guid("04f6d6d9-874f-4b2b-90cc-72450c767a42"), null, null, "Reguler", null, null, "Sprei King Set", 20000m, "Set", 3, "Day" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), new Guid("2a527b55-4947-448c-a56c-1afb523b09c0"), null, null, "Reguler", null, null, "Alas Kasur Single", 15000m, "Unit", 3, "Day" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), new Guid("8b99aa71-ec4f-4d90-8dd5-8b1a19b92945"), null, null, "Reguler", null, null, "Sprei Single Set", 15000m, "Set", 3, "Day" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), new Guid("90e19c42-a89b-4a29-82bc-04066b19f35c"), null, null, "Reguler", null, null, "Alas Kasur King", 25000m, "Unit", 3, "Day" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), new Guid("915970c9-6e07-4101-8e82-bde2b935b454"), null, null, "Reguler", null, null, "Sprei Single", 10000m, "Unit", 3, "Day" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), new Guid("954df4a5-e9c8-4a05-b893-2e8a68da9171"), null, null, "Reguler", null, null, "Sprei Quen Set", 20000m, "Set", 3, "Day" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), new Guid("cdf2af80-d63f-4585-8a41-200bcea47660"), null, null, "Reguler", null, null, "Alas Kasur Queen", 20000m, "Unit", 3, "Day" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), new Guid("f38cbf47-f2ce-4f39-9bdb-2b769c1614c7"), null, null, "Reguler", null, null, "Sprei King", 15000m, "Unit", 3, "Day" },
                    { new Guid("5e621eab-6b0b-49eb-b018-1425a0da8b72"), new Guid("f8214070-d518-4965-a864-b0611169b4b7"), null, null, "Reguler", null, null, "Sprei Queen", 15000m, "Unit", 3, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("00da1441-6fd1-46af-9d68-ae49cb00bc7f"), null, null, "Reguler", null, null, "Atasan", 10000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("036a5843-d09a-4c6c-bd92-f7036e706857"), null, null, "Reguler", null, null, "Handuk Besar", 7000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("10fc601c-c2e1-4b3d-9c59-6038981391b2"), null, null, "Reguler", null, null, "Bawahan", 10000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("1f70dd2e-b01d-4fb5-b573-4c86a18ec326"), null, null, "Reguler", null, null, "Dress Pendek", 15000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("65f2160e-5977-4488-bc8c-98de3e3655f4"), null, null, "Reguler", null, null, "Dress Panjang", 15000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("81305acb-43e8-4245-b7e7-ac79b25a5e20"), null, null, "Reguler", null, null, "Blazer Set", 25000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("994ef033-b02d-4452-83b6-e24c22294d91"), null, null, "Reguler", null, null, "Jaket/Sweater", 15000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("a4c5c479-7364-406e-8acd-a761161293eb"), null, null, "Reguler", null, null, "Blazer", 15000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("b1a8839a-9bde-450f-8eea-fbc7e1c0fc4d"), null, null, "Reguler", null, null, "Handuk Sedang", 6000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("ca3da966-ed51-42bc-8c37-698a8ca24b26"), null, null, "Reguler", null, null, "Jas Set", 30000m, "Unit", 2, "Day" },
                    { new Guid("69224789-77cf-44ac-b906-1d7c09868107"), new Guid("e8663646-4523-42bc-9916-14c7788eb9dd"), null, null, "Reguler", null, null, "Jas", 20000m, "Unit", 2, "Day" },
                    { new Guid("6e3d879e-9804-47db-8550-fee599a81b5b"), new Guid("02f2aad2-e154-4454-9ea2-f71dc88b7896"), null, null, "None", null, null, "100 Kgs", 550000m, "Package", 0, "None" },
                    { new Guid("6e3d879e-9804-47db-8550-fee599a81b5b"), new Guid("222744c7-845a-4c8b-b023-1cd488965784"), null, null, "None", null, null, "25 Kgs", 140000m, "Package", 0, "None" },
                    { new Guid("6e3d879e-9804-47db-8550-fee599a81b5b"), new Guid("afbc2e8a-e324-4baf-994e-2703221d0f49"), null, null, "None", null, null, "50 Kgs", 275000m, "Package", 0, "None" },
                    { new Guid("6f02fd67-f4c3-4d73-8856-a3b3c68d9276"), new Guid("3330723b-eb73-439d-8bcd-4e7a11dfe760"), null, null, "None", null, null, "100 Kgs", 475000m, "Package", 0, "None" },
                    { new Guid("6f02fd67-f4c3-4d73-8856-a3b3c68d9276"), new Guid("39d2e31c-fca5-4f44-8bf7-d0bec5027d79"), null, null, "None", null, null, "25 Kgs", 120000m, "Package", 0, "None" },
                    { new Guid("6f02fd67-f4c3-4d73-8856-a3b3c68d9276"), new Guid("bd4cb094-193e-4712-bed6-02ef2d91b30f"), null, null, "None", null, null, "50 Kgs", 240000m, "Package", 0, "None" },
                    { new Guid("85c8f51f-9c9b-4cb7-85ea-742c6f3d22e7"), new Guid("1a5a3143-a7a4-4335-be68-d4195601977f"), null, null, "Reguler", null, null, "Tas Sedang", 25000m, "Unit", 2, "Day" },
                    { new Guid("85c8f51f-9c9b-4cb7-85ea-742c6f3d22e7"), new Guid("1cc74945-64ee-4933-8147-1a01760e9c8a"), null, null, "Reguler", null, null, "Tas Kecil", 15000m, "Unit", 2, "Day" },
                    { new Guid("85c8f51f-9c9b-4cb7-85ea-742c6f3d22e7"), new Guid("238f014a-e593-4d13-946b-8aebda6d6f86"), null, null, "Reguler", null, null, "Tas Besar", 30000m, "Unit", 2, "Day" },
                    { new Guid("85c8f51f-9c9b-4cb7-85ea-742c6f3d22e7"), new Guid("3e7ecae5-adf9-40f8-9d00-a11f25c30eee"), null, null, "Reguler", null, null, "Tas Mini", 10000m, "Unit", 2, "Day" },
                    { new Guid("85c8f51f-9c9b-4cb7-85ea-742c6f3d22e7"), new Guid("abd24f3c-8784-4329-af85-af5431cf830f"), null, null, "Reguler", null, null, "Sepatu", 25000m, "Unit", 2, "Day" },
                    { new Guid("d925808e-80db-4d69-9059-d2a4720cc0c8"), new Guid("2afa7cd8-c18b-4aab-b9be-3767a9ff2a2c"), null, null, "OneDay", null, null, "One Day", 8000m, "Kilogram", 1, "Day" },
                    { new Guid("d925808e-80db-4d69-9059-d2a4720cc0c8"), new Guid("90cb34d3-a294-47a1-b39a-c21b3a740547"), null, null, "Express", null, null, "Express", 10000m, "Kilogram", 6, "Hour" },
                    { new Guid("d925808e-80db-4d69-9059-d2a4720cc0c8"), new Guid("a7dfabc2-89e7-4d4e-b7db-9afadeb7ab39"), null, null, "Reguler", null, null, "Reguler", 6000m, "Kilogram", 2, "Day" },
                    { new Guid("de1ce154-4c00-4f12-a593-a73634b10d67"), new Guid("5c86719c-f26f-423a-bd22-de328af45222"), null, null, "OneDay", null, null, "One Day", 7000m, "Kilogram", 1, "Day" },
                    { new Guid("de1ce154-4c00-4f12-a593-a73634b10d67"), new Guid("90539d35-6fe7-460d-85bf-911c93588903"), null, null, "Express", null, null, "Express", 9000m, "Kilogram", 6, "Hour" },
                    { new Guid("de1ce154-4c00-4f12-a593-a73634b10d67"), new Guid("a73c9ccf-1cd6-4b3b-adc5-b273112a7811"), null, null, "Reguler", null, null, "Reguler", 5000m, "Kilogram", 2, "Day" }
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
                name: "IX_journal_entry_ClosingEntryId",
                table: "journal_entry",
                column: "ClosingEntryId");

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
                name: "IX_message_template_Name",
                table: "message_template",
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
                name: "IX_purchase_header_SupplierId",
                table: "purchase_header",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_payment_PurchaseHeaderId",
                table: "purchase_payment",
                column: "PurchaseHeaderId");

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
                name: "company");

            migrationBuilder.DropTable(
                name: "journal_item");

            migrationBuilder.DropTable(
                name: "message_template");

            migrationBuilder.DropTable(
                name: "purchase_detail");

            migrationBuilder.DropTable(
                name: "purchase_payment");

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
                name: "closing_entry");

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
