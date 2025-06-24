using ElectronFlowSim.API.Utils.AppDefinition;
using Microsoft.AspNetCore.Mvc;

namespace ElectronFlowSim.API.Definitions.Swagger;

public class SwaggerDefinition : AppDefinition
{
    private const string SwaggerConfig = "/swagger/v1/swagger.json";

    public override void Use(WebApplication app)
    {
        //if (!app.Environment.IsDevelopment()) return;


        app.UseSwagger();
        app.UseSwaggerUI(settings =>
        {
            settings.SwaggerEndpoint(SwaggerConfig, "ServiceName v. 1.0.0");
        });
    }

    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();
        builder.Services.AddSwaggerGen();
    }
}
