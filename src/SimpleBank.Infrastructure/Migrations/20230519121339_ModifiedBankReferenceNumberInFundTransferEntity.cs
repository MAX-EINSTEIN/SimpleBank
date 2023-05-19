using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedBankReferenceNumberInFundTransferEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SourceAccountBranchIFSC",
                table: "FundTransfer",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(11)",
                oldFixedLength: true,
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAccountBranchIFSC",
                table: "FundTransfer",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(11)",
                oldFixedLength: true,
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "BankReferenceNo",
                table: "FundTransfer",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(12)",
                oldFixedLength: true,
                oldMaxLength: 12);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SourceAccountBranchIFSC",
                table: "FundTransfer",
                type: "nchar(11)",
                fixedLength: true,
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAccountBranchIFSC",
                table: "FundTransfer",
                type: "nchar(11)",
                fixedLength: true,
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "BankReferenceNo",
                table: "FundTransfer",
                type: "nchar(12)",
                fixedLength: true,
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }
    }
}
