using ElectronFlowSim.API.Utils.AppDefinition;

namespace ElectronFlowSim.API.Definitions.AutoMapper
{
    public class AutoMapperDefinition : AppDefinition
    {
        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
            => services.AddAutoMapper(typeof(AutoMapperDefinition));

        public override void Use(WebApplication app)
        {
        }
    }
}
