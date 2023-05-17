namespace SimpleBank.API.DTOs
{
    public record CreateBankDTO(
        string Name,
        string Street,
        string City,
        string Region,
        string Country,
        string ZipCode
    );
}
