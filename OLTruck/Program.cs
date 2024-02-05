using Microsoft.EntityFrameworkCore;
using OLTruck.ApiEndpoints;
using OLTruck.Infrastructure;
using OLTruck.Services;
using OLTruck.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OlTruckDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDatabase"));

builder.Services.AddScoped<ITempService, TempService>();

var app = builder.Build();
DataInitializer.InitializeData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapTruckEndpoints();
app.Run();


