using Domain;
using Domain.Interfaces.Repositories;
using Infrastructure.Database;

namespace Infrastructure
{
    public class UnitOfWork(SQLDBContext SQLDBContext, IEmployeeRepository employeeRepository) : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; set; } = employeeRepository;
        private readonly SQLDBContext _SQLDBContext = SQLDBContext;

        public async Task<int> SaveAsync() => await _SQLDBContext.SaveChangesAsync();

        public void Dispose() => _SQLDBContext.Dispose();
    }
}
