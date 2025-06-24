using ElectronFlowSim.API.Utils.AppDefinition;

namespace ElectronFlowSim.API.Definitions.Common;

public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers();
        
        services.AddEndpointsApiExplorer();
    }

    public override void Use(WebApplication app)
    {
        app.MapControllers();
    }
}
