namespace Domain.Services;

public interface IDiscountService
{
    decimal INSS(decimal grossPay);

    decimal IRPF(decimal grossPay);

    decimal PlanoSaude(decimal grossPay);

    decimal PlanoDental(decimal grossPay);

    decimal ValeTransporte(decimal grossPay);

    decimal FGTS(decimal grossPay);   
}