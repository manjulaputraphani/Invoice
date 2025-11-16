using Microsoft.EntityFrameworkCore;
using MediatR;
using SriDurgaHariHaraBackend.Application;
using SriDurgaHariHaraBackend.Data.Persistence;
using SriDurgaHariHaraBackend.Api.Attributes;
using SriDurgaHariHaraBackend.Data.Models;
using SriDurgaHariHaraBackend.Application.ProductValidations;
using FluentValidation;
using SriDurgaHariHaraBackend.Application.Interfaces;
using SriDurgaHariHaraBackend.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add controllers and swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core (AppDbContext in Infrastructure)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Server=(localdb)\\mssqllocaldb;Database=SriDurgaDb;Trusted_Connection=True;MultipleActiveResultSets=true"));

// Register MediatR and discover handlers in the Application assembly

builder.Services.AddMediatR(typeof(Invoice).Assembly);

// Register FluentValidation validators from the Application assembly
// Replace 'CreateProductValidator' with any validator type in your Application project
builder.Services.AddValidatorsFromAssemblyContaining<InvoiceValidation>();

// (optional) register your DI extension if present
//builder.Services.AddSriDurgaServices(builder.Configuration);

// Register application/data services as needed
builder.Services.AddScoped<IInvoiceRepository, InvoiceReadRepo>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

}

app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        // ensure the swagger UI appears at /swagger/index.html
        options.RoutePrefix = "swagger";
        // or to host swagger at root use RoutePrefix = string.Empty;
    });

app.UseRouting();
// use the middleware early
app.UseMiddleware<RequestValidationMiddleware>();

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
