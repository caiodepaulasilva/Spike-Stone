using Moq;
using Domain.Services;
using Domain.Entities;
using Application.Services;

namespace Tests
{
    public class PayrollServiceTest
    {
        [Fact]
        public async Task GetPayCheckSuccessful()
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

            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock.Setup(employee => employee.GetEmployee(It.IsAny<int>())).ReturnsAsync(employee);            

            var discountServiceMock = new Mock<IDiscountService>();
            discountServiceMock.Setup(discount => discount.INSS(It.IsAny<decimal>())).Returns(It.IsAny<decimal>());
            discountServiceMock.Setup(discount => discount.IRPF(It.IsAny<decimal>())).Returns(It.IsAny<decimal>());
            discountServiceMock.Setup(discount => discount.FGTS(It.IsAny<decimal>())).Returns(It.IsAny<decimal>());
            discountServiceMock.Setup(discount => discount.PlanoSaude(It.IsAny<decimal>())).Returns(It.IsAny<decimal>());
            discountServiceMock.Setup(discount => discount.PlanoDental(It.IsAny<decimal>())).Returns(It.IsAny<decimal>());
            discountServiceMock.Setup(discount => discount.ValeTransporte(It.IsAny<decimal>())).Returns(It.IsAny<decimal>());

            var payRollService = new PayrollService(discountServiceMock.Object, employeeServiceMock.Object);
            var result = await payRollService.GetPayCheck(employee.Id);            

            discountServiceMock.Verify(discount => discount.INSS(It.IsAny<decimal>()), Times.Once());
            discountServiceMock.Verify(discount => discount.IRPF(It.IsAny<decimal>()), Times.Once());
            discountServiceMock.Verify(discount => discount.FGTS(It.IsAny<decimal>()), Times.Once());
            discountServiceMock.Verify(discount => discount.PlanoSaude(It.IsAny<decimal>()), employee.DescontoPlanoSaude ? Times.Once() : Times.Never());
            discountServiceMock.Verify(discount => discount.PlanoDental(It.IsAny<decimal>()), employee.DescontoPlanoDental ? Times.Once() : Times.Never());
            discountServiceMock.Verify(discount => discount.ValeTransporte(It.IsAny<decimal>()), employee.DescontoValeTransporte? Times.Once() : Times.Never());

            Assert.NotNull(result);
            Assert.Equal(expected: employee.SalarioBruto, actual: result.SalarioBruto);            
        }    
    }
}