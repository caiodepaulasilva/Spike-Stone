using Domain.Enum;

namespace Domain.Entities;

public class Lancamento
{
    public TipoLancamento Tipo { get; set; }

    public decimal Valor { get; set; }

    public string? Descricao { get; set; }

    public Lancamento()
    {

    }

    public Lancamento(TipoLancamento tipo, decimal valor, string? descricao)
    {
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;        
    }
}


