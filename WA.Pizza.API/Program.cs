using Microsoft.EntityFrameworkCore;
using WA.Pizza.Infrastructure;
using WA.Pizza.Infrastructure.Data;
using WA.Pizza.Infrastructure.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<BasketDataService>();
builder.Services.AddScoped<CatalogDataService>();
builder.Services.AddScoped<OrderDataService>();

builder.Services.AddDbContext<PizzaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


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