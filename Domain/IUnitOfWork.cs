using Domain.Interfaces.Repositories;

namespace Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; set; }

        Task<int> SaveAsync();
    }
}

