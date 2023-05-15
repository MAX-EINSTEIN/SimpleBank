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

            builder.OwnsOne(b => b.Address, a =>
            {
                a.WithOwner();
            });

            builder.Property(b => b.BranchIFSC).IsRequired();
            builder.Property(b => b.TransactionLimit).IsRequired();
            builder.Property(b => b.Currency).IsRequired();

            var navigation = builder
                .Metadata
                .FindNavigation(nameof(Bank.Accounts));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            
        }
    }
}
