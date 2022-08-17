using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.DataContext;
using Services;
using Services.Abstract;
using Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add controllers defined in Controllers Project
builder.Services.AddMvc()
    .AddApplicationPart(typeof(BreweryQueryController).Assembly);

//for swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//inject database context
builder.Services.AddDbContext<AppDbContext>(opts =>
opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"),
    opts => opts.MigrationsAssembly("BeerApi")));

//inject ServiceWrapper implementation
builder.Services.AddScoped<IServicesWrapper, ServicesWrapper>();

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
