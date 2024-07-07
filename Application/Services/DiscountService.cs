using Domain.Exceptions;
using Domain.Services;

namespace Application.Services
{
    public class  DiscountService : IDiscountService
    {
        public readonly decimal[] INSS_taxRate;
        public readonly decimal[] IRPF_taxRate;
        public readonly decimal[] IRPF_deduct;

        public readonly decimal ValeTransporte_taxRate;
        public readonly decimal PlanoSaude_deduct;
        public readonly decimal PlanoDental_deduct;
        public readonly decimal FGTS_taxRate;

        public DiscountService()
        {
            INSS_taxRate = [0.00M, 7.50M, 9.00M, 12.00M, 14.00M];
            IRPF_taxRate = [0.00M, 7.50M, 15.00M, 22.50M, 27.50M];
            IRPF_deduct = [0.00M, 142.80M, 354.80M, 636.13M, 869.36M];

            ValeTransporte_taxRate = 6.00M;
            PlanoSaude_deduct = 10.00M;
            PlanoDental_deduct = 5.00M;
            FGTS_taxRate = 8.00M;
        }

        public decimal INSS(decimal grossPay)
        {           
            return grossPay switch
            {
                decimal i when i <= 1045.00M => CalculateTaxRate(grossPay, INSS_taxRate[1]),
                decimal i when i >= 1045.01M && i <= 2089.60M => CalculateTaxRate(grossPay, INSS_taxRate[2]),
                decimal i when i >= 2089.61M && i <= 3134.40M => CalculateTaxRate(grossPay, INSS_taxRate[3]),
                decimal i when i >= 3134.41M && i <= 6101.06M => CalculateTaxRate(grossPay, INSS_taxRate[4]),
                _ => default,
            };
        }

        public decimal IRPF(decimal grossPay)
        {                        
            decimal fee;

            switch (grossPay)
            {
                case decimal i when i <= 1903.98M:
                    fee = CalculateTaxRate(grossPay, IRPF_taxRate[0]);
                    return fee > IRPF_deduct[0] ? IRPF_deduct[0] : fee;

                case decimal i when i >= 1903.99M && i <= 2826.65M:
                    fee = CalculateTaxRate(grossPay, IRPF_taxRate[1]);
                    return fee > IRPF_deduct[1] ? IRPF_deduct[1] : fee;

                case decimal i when i >= 2826.66M && i <= 3751.05M:
                    fee = CalculateTaxRate(grossPay, IRPF_taxRate[2]);
                    return fee > IRPF_deduct[2] ? IRPF_deduct[2] : fee;

                case decimal i when i >= 3751.06M && i <= 4664.68M:
                    fee = CalculateTaxRate(grossPay, IRPF_taxRate[3]);
                    return fee > IRPF_deduct[3] ? IRPF_deduct[3] : fee;

                case decimal i when i > 4664.68M:
                    fee = CalculateTaxRate(grossPay, IRPF_taxRate[4]);
                    return fee > IRPF_deduct[4] ? IRPF_deduct[4] : fee;

                default:
                    return default;
            }
        }

        public decimal PlanoSaude(decimal grossPay)
        {
            return PlanoSaude_deduct;
        }

        public decimal PlanoDental(decimal grossPay)
        {
            return PlanoDental_deduct;
        }

        public decimal ValeTransporte(decimal grossPay)
        {
            return grossPay switch
            {
                decimal i when i > 1500.00M => CalculateTaxRate(grossPay, ValeTransporte_taxRate),
                _ => default,
            };
        }

        public decimal FGTS(decimal grossPay)
        {
            return CalculateTaxRate(grossPay, FGTS_taxRate);
        }

        private static decimal CalculateTaxRate(decimal grossPay, decimal rate) => grossPay * (rate / 100);
    }
}
