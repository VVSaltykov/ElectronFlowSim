using Calabonga.UnitOfWork;
using ElectronFlowSim.AnalysisService.Common.Interfaces;
using ElectronFlowSim.AnalysisService.Common.Repositories;
using ElectronFlowSim.AnalysisService.Data;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using ElectronFlowSim.AnalysisService.GRPC.Services;
using ElectronFlowSim.DTO.AnalysisService;
using Microsoft.EntityFrameworkCore;

namespace ElectronFlowSim.AnalysisService.GRPC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddGrpc();

        builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DbConnection"), b => b.MigrationsAssembly("ElectronFlowSim.AnalysisService.GRPC")
        ));

        builder.Services.AddUnitOfWork<AppDbContext>();

        builder.Services.AddAutoMapper(typeof(InputDataRepository).Assembly);

        builder.Services.AddTransient<InputDataRepository>();

        var app = builder.Build();

        app.MapGrpcService<ElectronFlowGrpcService>();
        app.MapGrpcService<MagneticFieldsGrpcService>();
        app.MapGrpcService<DBCommunicationGrpcService>();

        app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}