using ElectronFlowSim.API.Utils.AppDefinition;

namespace ElectronFlowSim.API.Definitions.CORS;

public class CorsDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials();
            });
        });
    }

    public override void Use(WebApplication app)
    {
        app.UseCors("AllowAll");
    }
}
