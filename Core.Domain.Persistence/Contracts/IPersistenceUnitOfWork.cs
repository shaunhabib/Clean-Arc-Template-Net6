using Core.Domain.Persistence.Entities;
using LinqToDB.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Domain.Persistence.Contracts
{
    public interface IPersistenceUnitOfWork : IDisposable
    {
        public IRepositoryAsync<Employee> Employee { get; }


        DataConnection Linq2Db { get; }

        Task<int> SaveChangesAsync();
        
        Task<IDbContextTransaction> BeginTranscationAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}
