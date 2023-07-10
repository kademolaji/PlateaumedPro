using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlateaumedPro.Domain;
using PlateaumedPro.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextPool<DataContext>(options =>
             options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b =>
             b.MigrationsAssembly("PlateaumedPro.Domain")));

// Configure app services
builder.Services.RegisterServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
