using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBankBranchEntityAndModifiedBankEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "Address_Region",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "BranchIFSC",
                table: "Bank");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMode",
                table: "FundTransfer",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "IMPS",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "IMPS");

            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "Bank",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BankBranch",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBranch", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bank_BankCode",
                table: "Bank",
                column: "BankCode");

            migrationBuilder.CreateIndex(
                name: "IX_BankBranch_BranchCode",
                table: "BankBranch",
                column: "BranchCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankBranch");

            migrationBuilder.DropIndex(
                name: "IX_Bank_BankCode",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "Bank");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMode",
                table: "FundTransfer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "IMPS",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "IMPS");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Bank",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Bank",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Region",
                table: "Bank",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Bank",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "Bank",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchIFSC",
                table: "Bank",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }
    }
}
