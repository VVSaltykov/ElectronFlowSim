using ElectronFlowSim.API.Services.ElectronFlow;
using ElectronFlowSim.API.Services.File;
using ElectronFlowSim.API.Services.Kafka;
using ElectronFlowSim.API.Utils.AppDefinition;

namespace ElectronFlowSim.API.Definitions.DependencyContainer;

public class ContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
        services.AddSingleton<ITxtFileService, TxtFileService>();

        services.AddTransient<IElectronFlowService, ElectronFlowService>();
    }
}
