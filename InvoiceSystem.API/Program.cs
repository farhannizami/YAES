using InvoiceSystem.Application.Interfaces;
using InvoiceSystem.Application.Services;
using InvoiceSystem.Domain.Interfaces;
using InvoiceSystem.Infrastructure.Data;
using InvoiceSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerServices>();
            builder.Services.AddScoped<IInvoiceService, InvoiceServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
