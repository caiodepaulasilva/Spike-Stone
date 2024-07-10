using Domain.Entities;
using Domain.Enum;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Globalization;

namespace Spike_Stone.Validators
{
 
    public class ExampleSchema : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            string[] salarios = ["1045.00", "10045.01", "1903.38", "2089.60", "2089.61", "2826.66", "3134.40", "3134.41", "3751.05", "4664.68", "6101.06"];

            if (context.Type == typeof(Employee))
            {
                schema.Example = new OpenApiObject()
                {
                    ["Nome"] = new OpenApiString("Nome Teste"),
                    ["Sobrenome"] = new OpenApiString("Sobrenome Teste"),
                    ["Documento"] = new OpenApiString("998.878.950-59"),
                    ["Setor"] = new OpenApiString($"Marketing"),
                    ["SalarioBruto"] = new OpenApiString($"{salarios.GetValue(new Random().Next(salarios.Length))}"),
                    ["DataAdmissao"] = new OpenApiString($"{DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.DefaultThreadCurrentCulture)}"),
                    ["DescontoPlanoSaude"] = new OpenApiBoolean(new Random().Next(2) == 0),
                    ["DescontoPlanoDental"] = new OpenApiBoolean(new Random().Next(2) == 0),
                    ["DescontoValeTransporte"] = new OpenApiBoolean(new Random().Next(2) == 0),
                };
            }
        }
    }

}
