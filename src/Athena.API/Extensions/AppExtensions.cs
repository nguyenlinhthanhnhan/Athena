using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Athena.API.Extensions;

public static class AppExtensions
{
    public static void UseSwaggerExtension(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                config.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });
    }
}