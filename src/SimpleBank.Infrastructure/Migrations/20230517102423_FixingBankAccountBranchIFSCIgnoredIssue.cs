using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingBankAccountBranchIFSCIgnoredIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "BankAccount",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BranchIFSC",
                table: "BankAccount",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchIFSC",
                table: "BankAccount");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);
        }
    }
}
