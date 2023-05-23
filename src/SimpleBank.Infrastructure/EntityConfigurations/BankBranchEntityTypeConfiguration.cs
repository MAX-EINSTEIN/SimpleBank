using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBank.Domain.BankBranchAggregate;

namespace SimpleBank.Infrastructure.EntityConfigurations
{
    internal class BankBranchEntityTypeConfiguration : IEntityTypeConfiguration<BankBranch>
    {
        public void Configure(EntityTypeBuilder<BankBranch> builder)
        {
            builder.ToTable("BankBranch");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.Name)
                .IsRequired();

            builder.HasIndex(b => b.BranchCode);
            builder.Property(b => b.BranchCode)
                .HasMaxLength(6)
                .IsRequired();

            builder.OwnsOne(b => b.Address, x =>
            {
                x.Property(a => a.Street);
                x.Property(a => a.City);
                x.Property(a => a.Region);
                x.Property(a => a.Country);
                x.Property(a => a.ZipCode);
            });

            builder.Property(b => b.BankId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

        }
    }
}
