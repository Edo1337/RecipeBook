using RecipeBook.DAL.DependencyInjection;
using RecipeBook.Application.DependencyInjection;
using Serilog;
using RecipeBook.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwagger();

//Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

//Dependency Injection for DAL
builder.Services.AddDataAccessLayer(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeBook Swagger v1.0");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "RecipeBook Swagger v2.0");
        s.RoutePrefix = string.Empty    ;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
