using Domain.Entities;

namespace Domain.Services;

public interface IEmployeeService
{
    Task<Employee> AddEmployee(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task DeleteEmployee(int employeeId);
    Task<Employee> GetEmployee(int employeeId);
    Task<IEnumerable<Employee>> GetEmployee();
}