using Domain.Entities;

namespace Domain.Services;

public interface IPayrollService
{
    Task<Paycheck> GetPayCheck(int employeeId);
}