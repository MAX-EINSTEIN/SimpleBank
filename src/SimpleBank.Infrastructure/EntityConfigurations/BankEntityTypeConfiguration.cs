using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBank.Domain.Models;

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

            builder.OwnsOne(b => b.Address, x =>
            {
                x.Property(a => a.Street);
                x.Property(a => a.City);
                x.Property(a => a.Region);
                x.Property(a => a.Country);
                x.Property(a => a.ZipCode);
            });

            builder.Property(b => b.BranchIFSC).IsRequired();

            var navigation = builder
                .Metadata
                .FindNavigation(nameof(Bank.Accounts));
            navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
