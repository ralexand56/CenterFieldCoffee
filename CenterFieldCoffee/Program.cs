using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CenterFieldCoffee.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CenterFieldCoffeeContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CenterFieldCoffeeContext") ?? throw new InvalidOperationException("Connection string 'CenterFieldCoffeeContext' not found.")));

// Add services to the container.

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
