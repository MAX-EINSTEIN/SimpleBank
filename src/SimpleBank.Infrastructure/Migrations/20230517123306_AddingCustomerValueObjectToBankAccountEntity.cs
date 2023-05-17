using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingCustomerValueObjectToBankAccountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountHolder_Address_City",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder_Address_Country",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder_Address_Region",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder_Address_Street",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder_Address_ZipCode",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder_Email",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder_Gender",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder_Name",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder_PhoneNumber",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountHolder_Address_City",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "AccountHolder_Address_Country",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "AccountHolder_Address_Region",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "AccountHolder_Address_Street",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "AccountHolder_Address_ZipCode",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "AccountHolder_Email",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "AccountHolder_Gender",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "AccountHolder_Name",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "AccountHolder_PhoneNumber",
                table: "BankAccount");
        }
    }
}
