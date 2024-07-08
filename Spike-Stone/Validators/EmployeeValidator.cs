using Domain.Entities;
using FluentValidation;

namespace Spike_Stone.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        private static bool IsCpf(string? cpf)
        {
            int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

            cpf = cpf?.Trim().Replace(".", "").Replace("-", "");
            if (cpf?.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf[..9];
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public EmployeeValidator()
        {
            RuleFor(employee => employee.Nome)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(employee => employee.Sobrenome)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(employee => employee.Documento)
                .NotNull()
                .NotEmpty()
                .Must(IsCpf)
                .Matches(@"([0-9]{2}[\.][0-9]{3}[\.][0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.][0-9]{3}[\.][0-9]{3}[-][0-9]{2})").WithMessage("Documento CPF está fora do padrão esperado.");

            RuleFor(employee => employee.Setor)
                .NotNull()
                .NotEmpty()
                .IsInEnum();

            RuleFor(employee => employee.SalarioBruto)
                .NotNull()
                .NotEmpty()
                .PrecisionScale(10, 2, true);

            RuleFor(employee => employee.DataAdmissao)
                .NotNull()
                .NotEmpty()
                .Must(data => data.GetType() == typeof(DateTime));

            RuleFor(employee => employee.DescontoPlanoSaude)
                .Must(data => data.GetType() == typeof(bool));

            RuleFor(employee => employee.DescontoPlanoDental)                
                .Must(data => data.GetType() == typeof(bool));

            RuleFor(employee => employee.DescontoValeTransporte)                
                .Must(data => data.GetType() == typeof(bool));
        }
    }
}
