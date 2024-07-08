using Domain.Entities;

namespace Domain.Services;

public interface IEmployeeService
{
    Task<Employee> AddEmployee(Employee employee);

    Task<Employee> GetEmployee(int employeeId);
}