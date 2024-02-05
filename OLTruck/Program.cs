using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using OLTruck.ApiEndpoints;
using OLTruck.Infrastructure;
using OLTruck.Services;
using OLTruck.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OlTruckDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDatabase"));
// Display enum as string
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

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


