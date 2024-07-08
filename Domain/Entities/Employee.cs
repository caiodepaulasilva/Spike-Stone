using Domain.Enum;

namespace Domain.Entities;

public class Employee : BaseEntity
{
    public string? Nome { get; set; }

    public string? Sobrenome { get; set; }

    public string? Documento { get; set; }

    public Setor Setor { get; set; }

    public decimal SalarioBruto { get; set; }

    public DateTime DataAdmissao { get; set; }

    public bool DescontoPlanoSaude { get; set; }

    public bool DescontoPlanoDental { get; set; }

    public bool DescontoValeTransporte { get; set; }
}

