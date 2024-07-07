using Application.Services;
using System.Globalization;

namespace Tests
{
    public class DiscountServiceTest()
    {
        private static readonly DiscountService discountService = new();

        [Theory]
        [InlineData("1045.00")]
        [InlineData("1045.01")]
        [InlineData("2089.60")]
        [InlineData("2089.61")]
        [InlineData("3134.40")]
        [InlineData("3134.41")]
        [InlineData("6101.06")]
        [InlineData("8000.00")]
        public void INSS_Tax_Rate_Verify(string value)
        {
            var grossPay = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            var discount = discountService.INSS(Convert.ToDecimal(grossPay, CultureInfo.InvariantCulture));

            var aliquot = (discount / grossPay) * 100;

            Assert.Contains(aliquot, discountService.INSS_taxRate);
        }

        [Theory]
        [InlineData("1903.98")]
        [InlineData("1903.99")]
        [InlineData("2826.65")]
        [InlineData("2826.66")]
        [InlineData("3751.05")]
        [InlineData("3751.06")]
        [InlineData("4664.68")]
        [InlineData("8000.00")]
        public void IRPF_Tax_Rate_Verify(string value)
        {
            var grossPay = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            var discount = discountService.IRPF(Convert.ToDecimal(grossPay, CultureInfo.InvariantCulture));

            if (discountService.IRPF_deduct.Contains(discount))
            {
                Assert.Contains(discount, discountService.IRPF_deduct);
            }
            else
            {
                var aliquot = (discount / grossPay) * 100;
                Assert.Contains(aliquot, discountService.IRPF_taxRate);
            }
        }

        [Theory]
        [InlineData("2000.25")]
        [InlineData("3500.50")]
        [InlineData("5000.75")]
        [InlineData("6500.00")]
        public void FGTS_Tax_Rate_Verify(string value)
        {
            var grossPay = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            var discount = discountService.FGTS(Convert.ToDecimal(grossPay, CultureInfo.InvariantCulture));

            var aliquot = (discount / grossPay) * 100;
            Assert.Equal(expected: discountService.FGTS_taxRate, actual: aliquot);
        }

        [Theory]
        [InlineData("1000.00")]
        [InlineData("2500.50")]
        public void ValeTransporte_Tax_Rate_Verify(string value)
        {
            var grossPay = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            var discount = discountService.ValeTransporte(Convert.ToDecimal(grossPay, CultureInfo.InvariantCulture));

            var aliquot = (discount / grossPay) * 100;

            if (grossPay > 1500.00M)
                Assert.Equal(expected: discountService.ValeTransporte_taxRate, actual: aliquot);
            else
                Assert.Equal(expected: 0, actual: aliquot);
        }

        [Theory]
        [InlineData("1000.00")]
        [InlineData("5000.00")]
        public void PlanoSaude_Tax_Rate_Verify(string value)
        {
            var grossPay = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            var discount = discountService.PlanoSaude(Convert.ToDecimal(grossPay, CultureInfo.InvariantCulture));

            var remainder = grossPay - discount;

            Assert.Equal(expected: discountService.PlanoSaude_deduct, actual: grossPay - remainder);
        }

        [Theory]
        [InlineData("1000.00")]
        [InlineData("5000.00")]
        public void PlanoDental_Tax_Rate_Verify(string value)
        {
            var grossPay = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            var discount = discountService.PlanoDental(Convert.ToDecimal(grossPay, CultureInfo.InvariantCulture));

            var remainder = grossPay - discount;

            Assert.Equal(expected: discountService.PlanoDental_deduct, actual: grossPay - remainder);
        }
    }
}