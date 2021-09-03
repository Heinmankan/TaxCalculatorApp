using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxCalculatorDB.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostalCodeTaxCalculationLinks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxCalculationType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeTaxCalculationLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxBands",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromBand = table.Column<int>(type: "int", nullable: false),
                    ToBand = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxCalculationType = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Result = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PostalCodeTaxCalculationLinks",
                columns: new[] { "Id", "CreatedAt", "IsActive", "PostalCode", "TaxCalculationType" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), true, "7441", 1 },
                    { 2L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), true, "A100", 2 },
                    { 3L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), true, "7000", 3 },
                    { 4L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), true, "1000", 1 }
                });

            migrationBuilder.InsertData(
                table: "TaxBands",
                columns: new[] { "Id", "CreatedAt", "FromBand", "Rate", "ToBand" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), 0, 0.01m, 8350 },
                    { 2L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), 8351, 0.015m, 33950 },
                    { 3L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), 33951, 0.025m, 82250 },
                    { 4L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), 82251, 0.028m, 171550 },
                    { 5L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), 171551, 0.033m, 372950 },
                    { 6L, new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc), 372951, 0.035m, 2147483647 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostalCodeTaxCalculationLinks");

            migrationBuilder.DropTable(
                name: "TaxBands");

            migrationBuilder.DropTable(
                name: "TaxCalculations");
        }
    }
}
