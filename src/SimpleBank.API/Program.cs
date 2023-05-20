using Microsoft.EntityFrameworkCore;
using SimpleBank.Infrastructure;
//using Autofac;
//using Autofac.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SimpleBank.Application;
using SimpleBank.Domain;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Extension Methods defined in the different Layers
builder.Services.AddDbContext(connectionString!);
builder.Services.AddPersistence();
builder.Services.AddApplicatonServices();
builder.Services.AddDomainServices();

//builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
//{
//    //containerBuilder.RegisterModule(new DefaultCoreModule());
//    containerBuilder.RegisterModule(new DefaultInfrastructureModule());
//});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple Bank API", Version = "v1" });
});

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
