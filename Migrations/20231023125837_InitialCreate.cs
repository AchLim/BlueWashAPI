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
                    table.PrimaryKey("PK_sales_detail", x => new { x.SalesHeaderId, x.SalesDetailId });
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
                    { new Guid("0ec22a83-caae-4c39-8655-6749b0ca94f2"), "Asset", 100, "Bank", 112, null, null, null, null, null },
                    { new Guid("3d2a3d9c-aace-40ab-b140-2377a3c2cadc"), "Asset", 100, "Akumulasi Depresiasi - Mesin Cuci", 122, null, null, null, null, null },
                    { new Guid("4605af1e-c7f2-42bb-90d0-ec7958641a80"), "Asset", 100, "Kas", 111, null, null, null, null, null },
                    { new Guid("4995d026-cbbb-488e-a4be-b17a667d7fb8"), "Ekuitas", 300, "Ekuitas Pemilik Usaha", 301, null, null, null, null, null },
                    { new Guid("681756e0-1175-4dd0-8ff6-1c0bb44d41ae"), "Liabilitas", 200, "Utang Usaha", 201, null, null, null, null, null },
                    { new Guid("6e26a36b-6ede-48a4-a778-53ac340f3895"), "Pengeluaran", 500, "Beban Sewa", 502, null, null, null, null, null },
                    { new Guid("8328a6b0-425c-45ac-8cf3-ded4b6f84904"), "Asset", 100, "Perlengkapan", 114, null, null, null, null, null },
                    { new Guid("88b47c68-a55e-4713-832a-39329222a14f"), "Asset", 100, "Sewa dibayar di muka", 115, null, null, null, null, null },
                    { new Guid("94fa4c84-ec63-420b-9c72-4e8ef614cf5e"), "Pengeluaran", 500, "Beban Depresiasi", 506, null, null, null, null, null },
                    { new Guid("aa31e39a-cd2d-4917-9849-4677f2976426"), "Pengeluaran", 500, "Beban Perlengkapan", 505, null, null, null, null, null },
                    { new Guid("cdcfaa90-6643-4380-ac3d-5c2b93f64418"), "Pengeluaran", 500, "Beban Gaji", 501, null, null, null, null, null },
                    { new Guid("d43cd617-baef-4ff9-a1ca-797ff37b44bd"), "Pengeluaran", 500, "Beban Utilitas", 503, null, null, null, null, null },
                    { new Guid("d81c0d49-91f6-4305-bfef-67405983f9e1"), "Asset", 100, "Peralatan", 121, null, null, null, null, null },
                    { new Guid("d960bcde-c868-4011-84a2-bc5b43ef4717"), "Pendapatan", 400, "Pendapatan Penjualan", 401, null, null, null, null, null },
                    { new Guid("de177da6-a35d-4f65-850b-bc6c08bb59fe"), "Asset", 100, "Persediaan", 113, null, null, null, null, null },
                    { new Guid("fb3070d2-a2e8-4a9f-81eb-4a4f82cf3986"), "Pengeluaran", 500, "Beban Listrik", 504, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "currency",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "CultureName", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("52034634-9fa8-4c45-8283-b115ba33c700"), "SGD", null, null, "en-SG", null, null, "Dollar Singapore" },
                    { new Guid("7e959d12-1c25-4e1a-b1e9-ebf890a66fce"), "IDR", null, null, "id-ID", null, null, "Indonesia Rupiah" },
                    { new Guid("94ea9b5a-c4a5-475c-8c97-ed2fc997f335"), "USD", null, null, "en-US", null, null, "Dollar USD" },
                    { new Guid("ee35d259-aefe-4b46-a4eb-9ba4f7911f9e"), "MYR", null, null, "ms-MY", null, null, "Ringgit Malaysia" }
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
                name: "IX_sales_detail_InventoryId",
                table: "sales_detail",
                column: "InventoryId");

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
                name: "purchase_header");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "sales_header");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "currency");
        }
    }
}
