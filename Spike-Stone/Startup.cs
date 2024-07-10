using Domain;
using Infrastructure;
using Domain.Services;
using Application.Services;
using Spike_Stone.Validators;
using Infrastructure.Database;
using Spike_Stone.Middlewares;
using Microsoft.OpenApi.Models;
using Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FluentValidation;
using Domain.Entities;

namespace Spike_Stone.Controllers
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddDbContext<SQLDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection2")));

            _ = services.AddScoped<IUnitOfWork, UnitOfWork>(f =>
            {
                var scopeFactory = f.GetRequiredService<IServiceScopeFactory>();
                var context = f.GetService<SQLDBContext>();
                return new UnitOfWork(
                    context,
                    new EmployeeRepository(context.Employees)
                );
            });            

            services.AddScoped<IPayrollService, PayrollService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IValidator<Employee>, EmployeeValidator>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<ExampleSchema>();
            });

            services.AddFluentValidationAutoValidation();
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandler>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "v1");
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseHttpsRedirection();
        }
    }
}
