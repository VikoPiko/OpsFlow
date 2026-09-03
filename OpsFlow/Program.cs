using FluentValidation.AspNetCore;
using OpsFlow.Application;
using OpsFlow.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters
             .Add(new JsonStringEnumConverter());
     });

builder.Services.AddOpenApi();

builder.Services.AddMemoryCache();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();

app.Run();
