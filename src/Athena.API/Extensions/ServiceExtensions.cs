﻿using System.Reflection;
using Athena.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Athena.API.Extensions;

public static class ServiceExtensions
{
    public static void AddApiVersioningExtension(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });
    }

    public static void AddVersionedApiExplorerExtension(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });
    }

    public static void AddSwaggerGenExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.OperationFilter<SwaggerDefaultValues>();
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}