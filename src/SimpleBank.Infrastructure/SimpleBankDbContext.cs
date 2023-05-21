using Microsoft.EntityFrameworkCore;
using SimpleBank.Domain.Contracts;
using SimpleBank.Domain.Models;
using SimpleBank.Infrastructure.EntityConfigurations;

namespace SimpleBank.Infrastructure
{
    public class SimpleBankDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Bank> Banks { get; set; } = null!; 
        public DbSet<BankBranch> BankBranches { get; set; } = null!;
        public DbSet<BankAccount> BankAccounts { get; set; } = null!; 
        public DbSet<TransactionRecord> TransactionRecords { get; set; } = null!; 
        public DbSet<FundTransfer> FundTransfers { get; set; } = null!; 

        public SimpleBankDbContext(DbContextOptions<SimpleBankDbContext> options): base(options) { }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BankBranchEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BankAccountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionRecordEntityTypeConfiguration()); ;
            modelBuilder.ApplyConfiguration(new FundTransferEntityTypeConfiguration());
        }
    }


}
