using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
    public class DiscountService() : IDiscountService
    {
        public decimal INSS(decimal grossPay)
        {
            decimal[] taxRateiNSS = [7.50M, 9.00M, 12.00M, 14.00M];

            return grossPay switch
            {
                decimal i when i <= 1045.00M => CalculateTaxRate(grossPay, taxRateiNSS[0]),
                decimal i when i > 1045.01M && i < 2089.60M => CalculateTaxRate(grossPay, taxRateiNSS[1]),
                decimal i when i > 2089.61M && i < 3134.40M => CalculateTaxRate(grossPay, taxRateiNSS[2]),
                decimal i when i > 3134.41M && i < 6101.06M => CalculateTaxRate(grossPay, taxRateiNSS[3]),
                _ => default,
            };
        }

        public decimal IRPF(decimal grossPay)
        {
            decimal[] taxRateIRPF = [0.00M, 7.50M, 15.00M, 22.50M, 27.50M];
            decimal[] quotaDeduct = [0.00M, 142.80M, 354.80M, 636.13M, 869.36M];
            decimal fee;

            switch (grossPay)
            {
                case decimal i when i > 1903.98M:
                    fee = CalculateTaxRate(grossPay, taxRateIRPF[0]);
                    return fee > quotaDeduct[0] ? quotaDeduct[0] : fee;

                case decimal i when i > 1903.99M && i < 2826.65M:
                    fee = CalculateTaxRate(grossPay, taxRateIRPF[1]);
                    return fee > quotaDeduct[1] ? quotaDeduct[1] : fee;

                case decimal i when i > 2826.66M && i < 3751.05M:
                    fee = CalculateTaxRate(grossPay, taxRateIRPF[2]);
                    return fee > quotaDeduct[2] ? quotaDeduct[2] : fee;

                case decimal i when i > 3751.06M && i < 4664.68M:
                    fee = CalculateTaxRate(grossPay, taxRateIRPF[3]);
                    return fee > quotaDeduct[3] ? quotaDeduct[3] : fee;

                case decimal i when i > 4664.68M:
                    fee = CalculateTaxRate(grossPay, taxRateIRPF[4]);
                    return fee > quotaDeduct[4] ? quotaDeduct[4] : fee;

                default:
                    return default;
            }
        }

        public decimal PlanoSaude(decimal grossPay)
        {
            return CalculateTaxRate(grossPay, 10.00M);
        }

        public decimal PlanoDental(decimal grossPay)
        {
            return CalculateTaxRate(grossPay, 5.00M);
        }

        public decimal ValeTransporte(decimal grossPay)
        {
            return grossPay switch
            {
                decimal i when i > 1500.00M => CalculateTaxRate(grossPay, 6.00M),
                _ => default,
            };
        }

        public decimal FGTS(decimal grossPay)
        {
            return CalculateTaxRate(grossPay, 8.00M);
        }

        private static decimal CalculateTaxRate(decimal grossPay, decimal rate) => grossPay * (rate / 100);
    }
}
