using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBank.Domain.BankAggregate;

namespace SimpleBank.Infrastructure.EntityConfigurations
{
    internal class BankEntityTypeConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.ToTable("Bank");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder.HasIndex(b => b.BankCode);
            builder.Property(b => b.BankCode)
                .HasMaxLength(4)
                .IsRequired();
        }
    }
}
