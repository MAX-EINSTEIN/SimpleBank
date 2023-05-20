namespace SimpleBank.Application.DTOs
{
    public record CreateBankBranchDTO
    (
        string Name,
        string Street,
        string City,
        string Region,
        string Country,
        string ZipCode,
        string BranchCode,
        long BankId
    );
}
