using ElectronFlowSim.AnalysisService.BackGroundServices;
using ElectronFlowSim.AnalysisService.Services.ElectronFlow;
using ElectronFlowSim.AnalysisService.Services.Kafka;
using ElectronFlowSim.Common;

namespace ElectronFlowSim.AnalysisService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSignalR()
            .AddStackExchangeRedis("localhost:6379");

        builder.Services.AddSingleton<IElectronFlowService, ElectronFlowService>();
        builder.Services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();

        builder.Services.AddGrpc();

        builder.Services.AddHostedService<KafkaBackgroundService>();

        var app = builder.Build();

        app.MapHub<NotificationHub>("/electronFlowHub");

        app.Run();

    }
}