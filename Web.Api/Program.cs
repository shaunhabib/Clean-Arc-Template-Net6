using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport;
using Web.Framework.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var services = builder.Services;

// Add services to the container.

services.AddCors();
//services.AddAutoMapper();
services.AddFramework(builder.Configuration);
services.AddControllers();
services.AddApiVersioningExtension();
services.AddJsonMultipartFormDataSupport(JsonSerializerChoice.Newtonsoft);
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web.Api", Version = "v1" });
});


var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
var appSettingFile = isDevelopment ? $"appsettings.Development.json" : "appsettings.json";
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(appSettingFile, optional: true, reloadOnChange: true)
                .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.Api v1"));
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseApiErrorHandlingMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();

