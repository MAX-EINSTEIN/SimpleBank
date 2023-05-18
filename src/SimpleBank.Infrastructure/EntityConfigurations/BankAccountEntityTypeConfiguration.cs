using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBank.Domain.Models;
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

            builder.Property(b => b.BranchIFSC)
                .HasMaxLength(11)
                .IsRequired();

            builder.OwnsOne(b => b.AccountHolder, y =>
            {
                y.Property(a => a.Name);
                y.Property(a => a.Gender);
                y.Property(a => a.Email);
                y.Property(a => a.PhoneNumber);
                y.OwnsOne(a => a.Address, x =>
                {
                    x.Property(a => a.Street);
                    x.Property(a => a.City);
                    x.Property(a => a.Region);
                    x.Property(a => a.Country);
                    x.Property(a => a.ZipCode);
                });
            });


            builder.Property(b => b.TransactionLimit)
                .HasPrecision(9, 2);

            builder.Property(b => b.Currency)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property<decimal>("_balance")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Balance")
                .HasPrecision(9, 2)
                .IsRequired();

            builder.Ignore(a => a.Balance);

            var navigation = builder
                .Metadata
                .FindNavigation(nameof(BankAccount.TransactionRecords));
            navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
