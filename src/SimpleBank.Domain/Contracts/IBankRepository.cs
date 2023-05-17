using SimpleBank.Domain.Models;

namespace SimpleBank.Domain.Contracts
{
    public interface IBankRepository: IRepository<Bank>
    {
        public Task<Bank?> GetByName(string bankName);
        public Task<Bank?> GetByIFSC(string branchIFSC);
    }
}
