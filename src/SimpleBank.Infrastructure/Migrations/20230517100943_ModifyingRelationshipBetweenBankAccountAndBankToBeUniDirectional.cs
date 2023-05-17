using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingRelationshipBetweenBankAccountAndBankToBeUniDirectional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Bank_BankId",
                table: "BankAccount");

            migrationBuilder.AlterColumn<long>(
                name: "BankId",
                table: "BankAccount",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TransactionLimit",
                table: "BankAccount",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Bank_BankId",
                table: "BankAccount",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Bank_BankId",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "TransactionLimit",
                table: "BankAccount");

            migrationBuilder.AlterColumn<long>(
                name: "BankId",
                table: "BankAccount",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Bank_BankId",
                table: "BankAccount",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
