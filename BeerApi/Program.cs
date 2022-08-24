using Domain.Logger;
using Domain.Repositories;
using LoggerService;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;
using Repositories.Repositories;
using Serilog;
using Services;
using Services.Abstract;
using Services.Mappings;

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



//inject unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//inject ServiceWrapper implementation and IUnitOfWork
builder.Services.AddScoped<IServicesWrapper, ServicesWrapper>();
//inject mappings
DependencyInjectionMapping.addMappings(builder.Services);

//add loggermanager as a service
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

//versioning
builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //for production
}

//fot automatic EF core migrations to SQL database
app.MigrateDatabase();

//add controller to handle errors globally
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
