// using EasyOC.AuditTrail.Drivers;
using EasyOC.AuditTrail.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.AuditTrail.Services.Models;
using OrchardCore.Modules;
using System;

namespace EasyOC.AuditTrail
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<AuditTrailOptions>, ContentTypeAuditTrailEventConfiguration>();
            //services.AddScoped<IContentDefinitionEventHandler, AuditTrailContentTypeHandler>();
            // services.AddScoped<IDisplayDriver<AuditTrailEvent>, AuditTrailContentTypeEventDisplayDriver>();

        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Home",
                areaName: "EasyOC.AuditTrail",
                pattern: "Home/Index",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
