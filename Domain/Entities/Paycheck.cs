using Domain.Enum;

namespace Domain.Entities;

public class Paycheck
{
    public DateOnly MesReferencia { get; set; }

    public IEnumerable<Lancamento> Lancamentos { get; set; }

    public decimal SalarioBruto { get; set; }

    public decimal TotalDescontos { get; set; }

    public decimal SalarioLiquido { get; set; }
}


