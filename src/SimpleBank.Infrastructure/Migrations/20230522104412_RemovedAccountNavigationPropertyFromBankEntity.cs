using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAccountNavigationPropertyFromBankEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Bank_BankId",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_BankId",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "BankAccount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BankId",
                table: "BankAccount",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_BankId",
                table: "BankAccount",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Bank_BankId",
                table: "BankAccount",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id");
        }
    }
}
