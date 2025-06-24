using Electronflow;
using ElectronFlowSim.API.Utils.AppDefinition;

namespace ElectronFlowSim.API.Definitions.gRPC;

public class gRPCDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddGrpcClient<ElectronFlow.ElectronFlowClient>(options =>
        {
            options.Address = new Uri(builder.Configuration["GrpcServices:WorkerService"]
                                      ?? "https://localhost:7189");
        });

        services.AddGrpcClient<MagneticFields.MagneticFieldsClient>(options =>
        {
            options.Address = new Uri(builder.Configuration["GrpcServices:WorkerService"]
                                      ?? "https://localhost:7189");
        });

        //services.Configure<GrpcClientFactoryOptions>(options =>
        //{
        //    options.CallOptionsActions.Add(context =>
        //    {
        //        context.CallOptions = new CallOptions()
        //            .WithDeadline(DateTime.UtcNow.AddSeconds(30));
        //    });
        //});
    }
}
