using Moq;
using Domain.Entities;
using Application.Services;
using Domain;

namespace Tests
{
    public class EmployeeServiceTest
    {
        [Fact]
        public async Task AddEmployeeSuccessful()
        {
            Employee employee = new() 
            { 
                Id = 1,
                Nome = "Nome",
                Sobrenome = "Sobrenome",
                Documento = "44946324810",
                DescontoPlanoSaude =true,
                DescontoPlanoDental = true,
                DescontoValeTransporte = false,
                SalarioBruto = 8000.00M,
                Setor = Domain.Enum.Setor.Engenharia,
                DataAdmissao = DateTime.Now,
            };

            var UnitOfWork = new Mock<IUnitOfWork>();
            UnitOfWork.Setup(work => work.EmployeeRepository.ExistAsync(e => e.Documento == employee.Documento)).ReturnsAsync(It.IsAny<bool>());
            UnitOfWork.Setup(work => work.SaveAsync());

            var employeeService = new EmployeeService(UnitOfWork.Object);
            var result = await employeeService.AddEmployee(employee);

            UnitOfWork.Verify(work => work.EmployeeRepository.ExistAsync(e => e.Documento == employee.Documento), Times.Once());
            UnitOfWork.Verify(work => work.SaveAsync(), Times.Once());
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task GetEmployeeSuccessful()
        {
            var id = 1;
            Employee employee = new()
            {
                Id = 1,
                Nome = "Nome",
                Sobrenome = "Sobrenome",
                Documento = "44946324810",
                DescontoPlanoSaude = true,
                DescontoPlanoDental = true,
                DescontoValeTransporte = false,
                SalarioBruto = 8000.00M,
                Setor = Domain.Enum.Setor.Engenharia,
                DataAdmissao = DateTime.Now,
            };

            var UnitOfWork = new Mock<IUnitOfWork>();
            UnitOfWork.Setup(work => work.EmployeeRepository.ExistAsync(e => e.Id == id)).ReturnsAsync(true);
            UnitOfWork.Setup(work => work.EmployeeRepository.GetByIdAsync(id, null)).ReturnsAsync(employee);

            var employeeService = new EmployeeService(UnitOfWork.Object);
            var result = await employeeService.GetEmployee(id);

            UnitOfWork.Verify(work => work.EmployeeRepository.ExistAsync(e => e.Id == id), Times.Once());
            UnitOfWork.Verify(work => work.EmployeeRepository.GetByIdAsync(id, null), Times.Once());

            Assert.IsType<Employee>(result);
            Assert.NotNull(result);
        }
    }
}