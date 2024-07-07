using Domain.Entities;
using Domain.Services;

namespace Application.Services
{
    public class PayrollService(IDiscountService discountService, IEmployeeService employeeService) : IPayrollService
    {        
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IDiscountService _discountService = discountService;

        public async Task<Paycheck> GetPayCheck(int id, DateTime date)
        {
            List<Lancamento> lancamentos = [ ];

            decimal discountTotal = 0.00M;
            decimal discountINSS;
            decimal discountIRPF;
            decimal discountFGTS;
            decimal discountPlanoSaude;
            decimal discountPlanoDental;
            decimal discountValeTransporte;

            Employee employee = await _employeeService.GetEmployee(id);
            lancamentos.Add(new (Domain.Enum.TipoLancamento.Remuneracao, employee.SalarioBruto, "Remuneração"));

            discountTotal += discountINSS = _discountService.INSS(employee.SalarioBruto);
            lancamentos.Add(new (Domain.Enum.TipoLancamento.Desconto, discountINSS, "INSS"));

            discountTotal += discountIRPF = _discountService.IRPF(employee.SalarioBruto);
            lancamentos.Add(new (Domain.Enum.TipoLancamento.Desconto, discountIRPF, "IRPF"));

            discountTotal += discountFGTS = _discountService.FGTS(employee.SalarioBruto);
            lancamentos.Add(new (Domain.Enum.TipoLancamento.Desconto, discountFGTS, "FGTS"));

            if (employee.DescontoPlanoSaude)
            {
                discountTotal += discountPlanoSaude = _discountService.PlanoSaude(employee.SalarioBruto);
                lancamentos.Add(new (Domain.Enum.TipoLancamento.Desconto, discountPlanoSaude, "Plano de Saúde"));
            }

            if (employee.DescontoPlanoDental)
            {
                discountTotal += discountPlanoDental = _discountService.PlanoDental(employee.SalarioBruto);
                lancamentos.Add(new (Domain.Enum.TipoLancamento.Desconto, discountPlanoDental, "Plano de Dental"));
            }

            if (employee.DescontoValeTransporte)
            {
                discountTotal += discountValeTransporte = _discountService.ValeTransporte(employee.SalarioBruto);
                lancamentos.Add(new (Domain.Enum.TipoLancamento.Desconto, discountValeTransporte, "Vale Transporte"));
            }                                   

            return new Paycheck()
            {
                MesReferencia = date,
                Lancamentos = lancamentos,
                TotalDescontos = discountTotal,
                SalarioBruto = employee.SalarioBruto,
                SalarioLiquido = employee.SalarioBruto - discountTotal
            };
        }
    }
}

