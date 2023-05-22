using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAnAlternateCompositePrimaryKeyAndIndexForBankAccountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_BankAccount_AccountNumber_BranchIFSC",
                table: "BankAccount",
                columns: new[] { "AccountNumber", "BranchIFSC" });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_AccountNumber_BranchIFSC",
                table: "BankAccount",
                columns: new[] { "AccountNumber", "BranchIFSC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_BankAccount_AccountNumber_BranchIFSC",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_AccountNumber_BranchIFSC",
                table: "BankAccount");
        }
    }
}
