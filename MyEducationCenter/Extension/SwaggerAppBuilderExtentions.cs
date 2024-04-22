using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class SwaggerAppBuilderExtentions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
