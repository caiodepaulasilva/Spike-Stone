using Domain;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
    public class PayrollService(IUnitOfWork unitOfWork, IDiscountService discountService, IEmployeeService employeeService) : IPayrollService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IDiscountService _discountService = discountService;

        public async Task<Paycheck> GetPayCheck(int id, DateOnly date)
        {
            List<Lancamento> lancamentos = [ new Lancamento{ }];

            decimal discountTotal = 0.00M;
            decimal discountINSS;
            decimal discountIRPF;
            decimal discountFGTS;
            decimal discountPlanoSaude;
            decimal discountPlanoDental;
            decimal discountValeTransporte;

            Employee employee = await _employeeService.GetEmployee(id);

            lancamentos.Add(new Lancamento()
            {
                Tipo = Domain.Enum.TipoLancamento.Remuneracao,
                Valor = employee.SalarioBruto,
                Descricao = "Remuneração"
            });

            discountTotal += discountINSS = _discountService.INSS(employee.SalarioBruto);
            lancamentos.Add(new Lancamento()
            {
                Tipo = Domain.Enum.TipoLancamento.Desconto,
                Valor = discountINSS,
                Descricao = "INSS"
            });

            discountTotal += discountIRPF = _discountService.IRPF(employee.SalarioBruto);
            lancamentos.Add(new Lancamento()
            {
                Tipo = Domain.Enum.TipoLancamento.Desconto,
                Valor = discountIRPF,
                Descricao = "IRPF"
            });

            discountTotal += discountFGTS = _discountService.FGTS(employee.SalarioBruto);
            lancamentos.Add(new Lancamento()
            {
                Tipo = Domain.Enum.TipoLancamento.Desconto,
                Valor = discountFGTS,
                Descricao = "FGTS"
            });

            if (employee.DescontoPlanoSaude)
            {
                discountTotal += discountPlanoSaude = _discountService.PlanoSaude(employee.SalarioBruto);
                lancamentos.Add(new Lancamento()
                {
                    Tipo = Domain.Enum.TipoLancamento.Desconto,
                    Valor = discountPlanoSaude,
                    Descricao = "Plano de Saúde"
                });
            }

            if (employee.DescontoPlanoDental)
            {
                discountTotal += discountPlanoDental = _discountService.PlanoDental(employee.SalarioBruto);
                lancamentos.Add(new Lancamento()
                {
                    Tipo = Domain.Enum.TipoLancamento.Desconto,
                    Valor = discountPlanoDental,
                    Descricao = "Plano de Dental"
                });
            }

            if (employee.DescontoValeTransporte)
            {
                discountTotal += discountValeTransporte = _discountService.ValeTransporte(employee.SalarioBruto);
                lancamentos.Add(new Lancamento()
                {
                    Tipo = Domain.Enum.TipoLancamento.Desconto,
                    Valor = discountValeTransporte,
                    Descricao = "Vale Transporte"
                });
            }                       

            var netPay = employee.SalarioBruto - discountTotal;

            return new Paycheck()
            {
                MesReferencia = date,
                Lancamentos = lancamentos,
                SalarioBruto = employee.SalarioBruto,
                TotalDescontos = discountTotal,
                SalarioLiquido = netPay
            };
        }
    }
}

