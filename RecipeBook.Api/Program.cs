using RecipeBook.DAL.DependencyInjection;
using RecipeBook.Application.DependencyInjection;
using Serilog;
using RecipeBook.Api;
using RecipeBook.Domain.Settings;
using RecipeBook.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//Связываем RabbitMqSettings.cs и appsettings.json 
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));
//Связываем JwtSettings.cs и appsettings.json 
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAuthenticationAndAuthhorization(builder);
builder.Services.AddSwagger();

//Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

//Dependency Injection for DAL
builder.Services.AddDataAccessLayer(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeBook Swagger v1.0");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "RecipeBook Swagger v2.0");
        s.RoutePrefix = string.Empty;
    });
}

//Cors
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
