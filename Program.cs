using Browlines_API.Data;
using Browlines_API.Interface;
using Browlines_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BrowlinesDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Add services to the container.
builder.Services.AddControllers();

//Add Interfaces

builder.Services.AddScoped<IUser_Services, User_Services>();
builder.Services.AddScoped<ICustomer_Services, Customer_Services>();
builder.Services.AddScoped<IChampions_Services, Champions_Services>();
builder.Services.AddScoped<IProcedures_Services, Procedures_Services>();
builder.Services.AddScoped<ITransaction_Services, Transaction_Services>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
