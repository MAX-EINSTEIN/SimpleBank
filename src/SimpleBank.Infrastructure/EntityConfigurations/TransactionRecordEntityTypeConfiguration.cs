using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBank.Domain.Models;

namespace SimpleBank.Infrastructure.EntityConfigurations
{
    internal class TransactionRecordEntityTypeConfiguration : IEntityTypeConfiguration<TransactionRecord>
    {
        public void Configure(EntityTypeBuilder<TransactionRecord> builder)
        {
            builder.ToTable("TransactionRecord");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd(); 

            builder.HasIndex(t => t.ReferenceId).IsUnique();
            builder.Property(t => t.ReferenceId).IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(t => t.DebitedAmount)
                .HasPrecision(9,2)
                .HasDefaultValue(0m)
                .IsRequired();

            builder.Property(t => t.CreditedAmount)
                .HasPrecision(9,2)
                .HasDefaultValue(0m)
                .IsRequired();

            builder.Property(t => t.UpdatedBalance)
                .HasPrecision(9, 2)
                .IsRequired();

            builder.Property(t => t.TransactionType)
                .HasMaxLength(6)
                .IsRequired();

            builder.Property(t => t.TransactionDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
