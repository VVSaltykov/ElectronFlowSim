using ElectronFlowSim.API.Utils.AppDefinition;
using ElectronFlowSim.Common;

namespace ElectronFlowSim.API.Definitions.SignalR;

public class SignalRDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR()
            .AddStackExchangeRedis(builder.Configuration["Redis:Connection"]);
    }

    public override void Use(WebApplication app)
    {
        app.MapHub<NotificationHub>("/electronFlowHub");
    }
}
