using Domain;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
    public class EmployeeService(IUnitOfWork unitOfWork) : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var exists = await _unitOfWork.EmployeeRepository.ExistAsync(x => x.Documento == employee.Documento);

            if (exists)
                throw new ConflictException("The employee already exist.");

            await _unitOfWork.EmployeeRepository.AddAsync(employee);
            var result = await _unitOfWork.SaveAsync();

            return employee;
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            var exists = await _unitOfWork.EmployeeRepository.ExistAsync(x => x.Id == employeeId);
            if (!exists)
                throw new NotFoundException("The employee doesn't exist.");

            return await _unitOfWork.EmployeeRepository.GetByIdAsync(employeeId);
        }
    }
}

