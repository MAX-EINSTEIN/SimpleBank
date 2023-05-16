﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleBank.Infrastructure;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    [DbContext(typeof(SimpleBankDbContext))]
    partial class SimpleBankDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SimpleBank.Domain.BankAggregate.Bank", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("BranchIFSC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<decimal>("TransactionLimit")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.ToTable("Bank", (string)null);
                });

            modelBuilder.Entity("SimpleBank.Domain.BankAggregate.BankAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long>("BankId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("_balance")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("Balance");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("BankAccount", (string)null);
                });

            modelBuilder.Entity("SimpleBank.Domain.BankAggregate.Bank", b =>
                {
                    b.OwnsOne("SimpleBank.Domain.Shared.Address", "Address", b1 =>
                        {
                            b1.Property<long>("BankId")
                                .HasColumnType("bigint");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Region")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BankId");

                            b1.ToTable("Bank");

                            b1.WithOwner()
                                .HasForeignKey("BankId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleBank.Domain.BankAggregate.BankAccount", b =>
                {
                    b.HasOne("SimpleBank.Domain.BankAggregate.Bank", null)
                        .WithMany("Accounts")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleBank.Domain.BankAggregate.Bank", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
