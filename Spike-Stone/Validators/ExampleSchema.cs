using Domain.Entities;
using Domain.Enum;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;

namespace Spike_Stone.Validators
{
 
    public class ExampleSchema : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            Array enumValues = Enum.GetValues(typeof(Setor));
            Setor setor = (Setor)enumValues.GetValue(new Random().Next(enumValues.Length));

            decimal[] salarios = [1045.00M, 10045.01M, 1903.38M, 2089.60M, 2089.61M, 2826.66M, 3134.40M, 3134.41M, 3751.05M, 4664.68M, 6101.06M];           

            if (context.Type == typeof(Employee))
            {
                schema.Example = new OpenApiObject()
                {
                    ["Nome"] = new OpenApiString("Nome Teste"),
                    ["Sobrenome"] = new OpenApiString("Sobrenome Teste"),
                    ["Documento"] = new OpenApiString("998.878.950-59"),
                    ["Setor"] = new OpenApiString($"{setor}"),
                    ["SalarioBruto"] = new OpenApiString($"{salarios.GetValue(new Random().Next(salarios.Length))}"),
                    ["DataAdmissao"] = new OpenApiString($"{DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd"), CultureInfo.GetCultureInfo("en-US"))}"),
                    ["DescontoPlanoSaude"] = new OpenApiBoolean(true),
                    ["DescontoPlanoDental"] = new OpenApiBoolean(true),
                    ["DescontoValeTransporte"] = new OpenApiBoolean(true)
                };
            }
        }
    }

}
