using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI_Jueves_2023II.DAL__DataAccessLayer_;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;
using ShoppingAPI_Jueves_2023II.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Linea de codigo que se necesita para configurar la conexi�n a la BD
builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Es para darle un ciclo de vida a la interfaz desde que se hace el request hasta el response
builder.Services.AddScoped<ICountryService, CountryService>();
//Por cada nuevo servicio/interfaz que yo creo en mi API, debo agregar aqui esa nueva dependencia

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
