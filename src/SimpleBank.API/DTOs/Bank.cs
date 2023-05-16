namespace SimpleBank.API.DTOs
{

    public record BankDTO(
        long Id,
        string Name,
        string Address,
        string IFSC,
        decimal TransactionLimit,
        string Currency,
        int NumberOfAccounts
    );

    public record CreateBankDTO(
        string Name,
        string Street,
        string City,
        string Region,
        string Country,
        string ZipCode,
        decimal TransactionLimit,
        string Currency
    );
}
