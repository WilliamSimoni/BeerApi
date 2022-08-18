using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;
using Services;
using Services.Abstract;
using Domain.Repositories;
using Repositories.Repositories;
using MapsterMapper;
using Mapster;
using System.Reflection;
using Serilog;
using LoggerService;
using Domain.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateBootstrapLogger();

builder.Logging.ClearProviders();
builder.Host.UseSerilog();

//for swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//inject database context
builder.Services.AddDbContext<AppDbContext>(opts =>
opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"),
    opts => opts.MigrationsAssembly("BeerApi")));

//inject ServiceWrapper implementation
builder.Services.AddScoped<IServicesWrapper, ServicesWrapper>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

//add loggermanager as a service
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} else
{
    //for production
}

//add controller to handle errors globally
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
