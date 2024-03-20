using Microsoft.AspNetCore.Builder;

namespace SistemaBico.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IApplicationBuilder UseSwaggerESwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bico Sistema");
            });

            return app;
        }
    }
}
