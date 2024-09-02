using Microsoft.OpenApi.Models;
using CustomerRepository;
using CustomerRepositoryContract;
using System.Reflection;
using CustomerServiceContract.IService;
using CustomerService.Service;
using CustomerWebApi.Configuration.ServiceConfiguration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Adding controllers to support API routes
builder.Services.AddAutoMapperProfiles();
// Registering services and repositories in the DI container
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddValidators();
builder.Services.AddSingleton<CustomerDapperDBContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenAuthorization();

var app = builder.Build();
app.UseCors("kraftpolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
       // c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Map controller routes

app.Run();
