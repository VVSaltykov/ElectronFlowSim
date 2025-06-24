using ElectronFlowSim.API.Utils.AppDefinition;

namespace ElectronFlowSim.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDefinitions(builder, typeof(Program));

        var app = builder.Build();

        app.UseDefinitions(typeof(Program));

        app.Run();
    }
}