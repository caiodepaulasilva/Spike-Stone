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
            await _unitOfWork.SaveAsync();
   
            await Task.Delay(10);
            return default;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var exists = await _unitOfWork.EmployeeRepository.ExistAsync(x => x.Id == employee.Id);
            if (!exists)
                throw new ConflictException("The product doesn't exist.");
            
            await _unitOfWork.EmployeeRepository.UpdateAsync(employee);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteEmployee(int employeeId)
        {
            var exists = await _unitOfWork.EmployeeRepository.ExistAsync(x => x.Id == employeeId);
            if (!exists)
                throw new NotFoundException("The employee type doesn't exist.");

            var product = await _unitOfWork.EmployeeRepository.GetByIdAsync(employeeId);
            await _unitOfWork.EmployeeRepository.DeleteAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            var exists = await _unitOfWork.EmployeeRepository.ExistAsync(x => x.Id == employeeId);
            if (!exists)
                throw new NotFoundException("The employee doesn't exist.");

            return await _unitOfWork.EmployeeRepository.GetByIdAsync(employeeId);                        
        }

        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            return await _unitOfWork.EmployeeRepository.GetAllAsync();                       
        }
    }
}

