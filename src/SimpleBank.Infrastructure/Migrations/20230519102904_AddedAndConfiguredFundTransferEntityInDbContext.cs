using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAndConfiguredFundTransferEntityInDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FundTransfer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceAccountNumber = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    SourceAccountBranchIFSC = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    DestinationAccountNumber = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    DestinationAccountBranchIFSC = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    BankReferenceNo = table.Column<string>(type: "nchar(12)", fixedLength: true, maxLength: 12, nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "IMPS"),
                    Remarks = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    _amount = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundTransfer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfer_BankReferenceNo",
                table: "FundTransfer",
                column: "BankReferenceNo");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransfer_TransferDate",
                table: "FundTransfer",
                column: "TransferDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundTransfer");
        }
    }
}
