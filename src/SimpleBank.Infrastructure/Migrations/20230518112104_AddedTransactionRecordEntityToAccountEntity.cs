using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTransactionRecordEntityToAccountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DebitedAmount = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false, defaultValue: 0m),
                    CreditedAmount = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false, defaultValue: 0m),
                    UpdatedBalance = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionRecord_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecord_BankAccountId",
                table: "TransactionRecord",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecord_ReferenceId",
                table: "TransactionRecord",
                column: "ReferenceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionRecord");
        }
    }
}
