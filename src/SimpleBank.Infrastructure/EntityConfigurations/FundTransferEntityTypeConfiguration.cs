using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBank.Domain.FundTransferAggregate;

namespace SimpleBank.Infrastructure.EntityConfigurations
{
    internal class FundTransferEntityTypeConfiguration : IEntityTypeConfiguration<FundTransfer>
    {
        public void Configure(EntityTypeBuilder<FundTransfer> builder)
        {
            builder.ToTable("FundTransfer");

            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.BankReferenceNo);
            builder.HasIndex(t => t.TransferDate);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.SourceAccountNumber)
                .HasMaxLength(17)
                .IsRequired();

            builder.Property(t => t.SourceAccountBranchIFSC)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(t => t.DestinationAccountNumber)
                .HasMaxLength(17)
                .IsRequired();

            builder.Property(t => t.DestinationAccountBranchIFSC)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(t => t.BankReferenceNo)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property("_amount")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasPrecision(9, 2);

            builder.Property(t => t.PaymentMode)
                .HasDefaultValue("IMPS");

            builder.Property(t => t.Remarks)
                .HasMaxLength(256);

            builder.Property(t => t.TransferDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
