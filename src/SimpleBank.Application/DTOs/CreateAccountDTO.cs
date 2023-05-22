namespace SimpleBank.Application.DTOs
{
    public record CreateAccountDTO 
    (
        string Name,
        string Gender,
        string Email,
        string PhoneNumber,
        string Street,
        string City,
        string Region,
        string Country,
        string ZipCode,
        decimal TransactionLimit,
        string Currency,
        string BankCode,
        string BranchCode
    );
        
    
}
