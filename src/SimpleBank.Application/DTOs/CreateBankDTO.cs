namespace SimpleBank.Application.DTOs
{
    public record CreateBankDTO(
        string Name,
        string Street,
        string City,
        string Region,
        string Country,
        string ZipCode,
        string BankCode,
        string BranchCode
    );
}
