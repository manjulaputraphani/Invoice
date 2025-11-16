using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MediatR;                                // <- required for AddMediatR
using SriDurgaHariHaraBackend.Infrastructure.Persistence;
using SriDurgaHariHaraBackend.Application;

var builder = WebApplication.CreateBuilder(args);

// Add controllers and swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core (AppDbContext in Infrastructure)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Server=(localdb)\\mssqllocaldb;Database=SriDurgaDb;Trusted_Connection=True;MultipleActiveResultSets=true"));

// Register MediatR and discover handlers in the Application assembly

builder.Services.AddMediatR(typeof(ProductDto).Assembly);

// (optional) register your DI extension if present
// builder.Services.AddSriDurgaServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
