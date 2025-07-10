using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Billing.Core.Migrations
{
    public partial class SetupInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            #region Customers

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: ["Id", "Name", "Email", "Address", "Active"],
                values: new object[,]
                {
                    { Guid.NewGuid().ToString(), "Elyseo M. Mesquita InsertData", "versatiletech2021@gmail.com", "Street 25 - nr 20", 1 },
                    { "12081264-5645-407a-ae37-78d5da96fe59", "Cliente Exemplo 1", "cliente1@example.com", "Rua Exemplo 1, 123", 1 },
                    { "12081374-5645-407a-ae37-78d5da96de78", "Cliente Exemplo 2", "cliente2@example.com", "Rua Exemplo 2, 456", 1 },
                    { "12081264-5645-407a-ae37-78d5da96fe58", "Cliente Exemplo 3", "cliente3@example.com", "Rua Exemplo 3, 789", 1 },
                    { "12081374-5645-407a-ae37-78d5da96de76", "Cliente Exemplo 4", "cliente4@example.com", "Rua Exemplo 4, 101", 1 },
                    { "12081374-5645-407a-ae37-78d5da96de77", "Cliente Exemplo 5", "cliente5@example.com", "Rua Exemplo 5, 202", 1 },
                    { "12081264-5645-407a-ae37-78d5da96fe56", "Cliente Exemplo 6", "cliente6@example.com", "Rua Exemplo 6, 303", 1 },
                    { "12081264-5645-407a-ae37-78d5da96fe55", "Cliente Exemplo 7", "cliente7@example.com", "Rua Exemplo 7, 404", 1 },
                    { "12081374-5645-407a-ae37-78d5da96de79", "Cliente Exemplo 8", "cliente8@example.com", "Rua Exemplo 8, 505", 1 },
                    { "12081264-5645-407a-ae37-78d5da96fe54", "Cliente Exemplo 9", "cliente9@example.com", "Rua Exemplo 9, 606", 1 },
                    { "12081374-5645-407a-ae37-78d5da96de75", "Cliente Exemplo 10", "cliente10@example.com", "Rua Exemplo 10, 707", 1 }
                })
                .GetInfrastructure().ColumnTypes = ["varchar(36)", "nvarchar(100)", "nvarchar(100)", "nvarchar(150)", "int"];

            #endregion

            #region Products

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                }
            );

            migrationBuilder.InsertData(
                table: "Products",
                columns: ["Id", "ProductName", "Active"],
                values: new object[,]
                {
                    { "48c6dc20-a943-4f8f-83ca-1e1cf094a683", "Produto 1", 1 },
                    { "48c6dc20-a943-4f8f-83ca-1e1cf094a612", "Produto 2", 1 },
                    { "48c6dc20-a943-4f8f-83ca-1e1cf094a611", "Produto 3", 1 },
                    { "48c6dc20-a943-4f8f-83ca-1e1cf094a610", "Produto 4", 1 },
                    { "48c6dc20-a943-4f8f-83ca-1e1cf094a685", "Produto 5", 1 },
                    { "48c6dc20-a943-4f8f-83ca-1e1cf094a617", "Produto 6", 1 },
                    { "2e7959c7-1a09-4f93-8578-63cbdb995bd3", "Produto 7", 1 },
                    { "2e7959c7-1a09-4f93-8578-63cbdb995bd4", "Produto 8", 1 },
                    { "50a04444-2d0f-4e8c-a9bb-48efc695d141", "Produto 9", 1 },

                    { "c2658aa3-0a26-4762-863c-27df1ff67d21", "Produto 10", 1 },
                    { "daa3775c-d669-4298-8235-dd37f655346e", "Produto 11", 1 },
                    { "9be485c7-4e88-4ad3-b434-42c7c8bb35d7", "Produto 12", 1 },
                    { "535b6d57-dc50-4946-a26c-241f4eb7f54f", "Produto 13", 1 },
                    { "c6694774-94b4-4f97-9684-50f45497f941", "Produto 14", 1 },
                    { "1fa5cb59-5736-4b3d-a58e-2b83a669d5c5", "Produto 15", 1 },
                    { "c8196b9d-bff7-472b-942b-1eafd7f47395", "Produto 16", 1 },
                    { "88345de2-41db-436e-a46a-f5896b675584", "Produto 17", 1 },
                    { "985bbcc4-4bb8-4ffa-a03a-cf08af16fde6", "Produto 18", 1 },
                    { "c7848a01-eeb9-44d3-94ea-c7dc02971aa3", "Produto 19", 1 },
                    { "fcee7f5f-3827-453b-9688-231fa5c586a3", "Produto 20", 1 },

                })
                .GetInfrastructure().ColumnTypes = ["varchar(36)", "nvarchar(100)", "int"];

            #endregion

            #region Invoices

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<string>(type: "char(36)", maxLength: 36, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Date = table.Column<DateTime>(type: "DATE", nullable: true),
                    DueDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    InvoiceDate = table.Column<long>(type: "BIGINT", nullable: true),
                    InvoiceAmount = table.Column<decimal>(type: "DECIMAL(11,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "DECIMAL(11,2)", nullable: true),
                    BillingLines = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Active = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                }
            );

            #endregion

            #region InvoiceLines

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<long>(type: "BIGINT", nullable: false),
                    ProductId = table.Column<string>(type: "char(36)", maxLength: 36, nullable: true),
                    Quantity = table.Column<int>(type: "INT",  nullable: true),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(11,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "DECIMAL(11,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.Id);
                }
            );

            #endregion

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Customers");
            migrationBuilder.DropTable(
               name: "Products");
            migrationBuilder.DropTable(
               name: "Invoices");
            migrationBuilder.DropTable(
               name: "InvoiceLines");
        }
    }
}
