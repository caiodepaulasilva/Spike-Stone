using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(p => p.Nome).IsRequired().HasMaxLength(150);
        builder.Property(p => p.Sobrenome).IsRequired().HasMaxLength(150);
        builder.Property(p => p.Documento).IsRequired().HasMaxLength(15);
        builder.Property(p => p.Setor).IsRequired();
        builder.Property(p => p.SalarioBruto).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.DataAdmissao).IsRequired();
        builder.Property(p => p.DescontoPlanoSaude).IsRequired();
        builder.Property(p => p.DescontoPlanoDental).IsRequired();
        builder.Property(p => p.DescontoValeTransporte).IsRequired();
    }
}
