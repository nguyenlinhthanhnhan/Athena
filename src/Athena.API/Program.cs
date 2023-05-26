using System.Reflection;
using Athena.API.Extensions;
using Athena.API.Filters;
using Athena.API.Helpers;
using Athena.Application;
using Athena.DataAccess;
using Athena.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;

const string defaultCorsPolicyName = "localhost";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Logging.AddSerilog();

// Add services to the container.
builder.Services.AddInfrastructureData(configuration).AddApplication(configuration);

builder.Services.AddControllers();

builder.Services.AddControllersWithViews(options => { options.Filters.Add(new ApiExceptionFilterAttribute()); });

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Swagger configs
builder.Services.AddApiVersioningExtension();
builder.Services.AddVersionedApiExplorerExtension();
builder.Services.AddSwaggerGenExtension();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddCors(
    options => options.AddPolicy(
        defaultCorsPolicyName,
        cp => cp
            .WithOrigins(
                // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                configuration["App:CorsOrigins"]!
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(o => o.RemovePostFix("/"))
                    .ToArray()
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    )
);

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerExtension(provider);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var assemblyName = Assembly.GetExecutingAssembly().GetName();

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", $"{assemblyName.Name}")
    .Enrich.WithProperty("Assembly", $"{assemblyName.Version}")
    .WriteTo.MongoDBBson(
        databaseUrl: builder.Configuration.GetSection("ConnectionStrings:SerilogConnection").Value!,
        collectionName: "Logs",
        restrictedToMinimumLevel: LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 1048576, rollOnFileSizeLimit: true)
    .CreateLogger();
try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}