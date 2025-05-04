using InvoiceService.Data;
using InvoiceService.Interfaces;
using InvoiceServiceImpl = InvoiceService.Services.InvoiceService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<InvoiceDbContext>(options =>
    options.UseInMemoryDatabase("InvoiceDb"));
builder.Services.AddScoped<IInvoiceService, InvoiceServiceImpl>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();