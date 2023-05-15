using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBank.Domain.BankAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Infrastructure.EntityConfigurations
{
    internal class BankAccountEntityTypeConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccount");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.AccountNumber)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property<long>("BankId")
                .IsRequired();

            builder.Property<decimal>("_balance")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Balance")
                .IsRequired();

            builder.Ignore(a => a.Balance);
            builder.Ignore(a => a.BranchIFSC);
        }
    }
}
