﻿namespace SimpleBank.Application.DTOs
{
    public record CreateFundTransferDTO(
        string SourceAccountNumber,
        string SourceAccountBranchIFSC,
        string DestinationAccountNumber,
        string DestinationAccontBranchIFSC,
        decimal Amount,
        string? PaymentMode,
        string Remarks
    );
}
