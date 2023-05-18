﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleBank.Infrastructure;

#nullable disable

namespace SimpleBank.Infrastructure.Migrations
{
    [DbContext(typeof(SimpleBankDbContext))]
    [Migration("20230518113259_ModifiedTransactionRecordEntityToGenerateTransactionDateUsingHasDefaultValueSql")]
    partial class ModifiedTransactionRecordEntityToGenerateTransactionDateUsingHasDefaultValueSql
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SimpleBank.Domain.Models.Bank", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("BranchIFSC")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Bank", (string)null);
                });

            modelBuilder.Entity("SimpleBank.Domain.Models.BankAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long?>("BankId")
                        .HasColumnType("bigint");

                    b.Property<string>("BranchIFSC")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<decimal>("TransactionLimit")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("_balance")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("Balance");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("BankAccount", (string)null);
                });

            modelBuilder.Entity("SimpleBank.Domain.Models.TransactionRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("BankAccountId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("CreditedAmount")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("DebitedAmount")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)")
                        .HasDefaultValue(0m);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ReferenceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("TransactionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<decimal>("UpdatedBalance")
                        .HasPrecision(9, 2)
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("ReferenceId")
                        .IsUnique();

                    b.ToTable("TransactionRecord", (string)null);
                });

            modelBuilder.Entity("SimpleBank.Domain.Models.Bank", b =>
                {
                    b.OwnsOne("SimpleBank.Domain.Models.Address", "Address", b1 =>
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

            modelBuilder.Entity("SimpleBank.Domain.Models.BankAccount", b =>
                {
                    b.HasOne("SimpleBank.Domain.Models.Bank", null)
                        .WithMany("Accounts")
                        .HasForeignKey("BankId");

                    b.OwnsOne("SimpleBank.Domain.Models.Customer", "AccountHolder", b1 =>
                        {
                            b1.Property<long>("BankAccountId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Gender")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BankAccountId");

                            b1.ToTable("BankAccount");

                            b1.WithOwner()
                                .HasForeignKey("BankAccountId");

                            b1.OwnsOne("SimpleBank.Domain.Models.Address", "Address", b2 =>
                                {
                                    b2.Property<long>("CustomerBankAccountId")
                                        .HasColumnType("bigint");

                                    b2.Property<string>("City")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Country")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Region")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Street")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("ZipCode")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("CustomerBankAccountId");

                                    b2.ToTable("BankAccount");

                                    b2.WithOwner()
                                        .HasForeignKey("CustomerBankAccountId");
                                });

                            b1.Navigation("Address")
                                .IsRequired();
                        });

                    b.Navigation("AccountHolder")
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleBank.Domain.Models.TransactionRecord", b =>
                {
                    b.HasOne("SimpleBank.Domain.Models.BankAccount", null)
                        .WithMany("TransactionRecords")
                        .HasForeignKey("BankAccountId");
                });

            modelBuilder.Entity("SimpleBank.Domain.Models.Bank", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("SimpleBank.Domain.Models.BankAccount", b =>
                {
                    b.Navigation("TransactionRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
