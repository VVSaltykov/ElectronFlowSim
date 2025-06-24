namespace ElectronFlowSim.API.Utils.AppDefinition;

public static class AppDefinitionExtensions
{
    public static IServiceCollection AddDefinitions(this IServiceCollection services, WebApplicationBuilder builder,
        Type programType)
    {
        var appDefinitions = GetDefinitions(programType);

        foreach (var appDefinition in appDefinitions) appDefinition.ConfigureServices(services, builder);

        return services;
    }

    public static IApplicationBuilder UseDefinitions(this WebApplication app, Type programType)
    {
        var appDefinitions = GetDefinitions(programType);

        foreach (var appDefinition in appDefinitions) appDefinition.Use(app);

        return app;
    }

    private static IEnumerable<AppDefinition> GetDefinitions(Type programType)
    {
        var appDefinitions = programType.Assembly
            .GetTypes()
            .Where(t => typeof(AppDefinition).IsAssignableFrom(t) && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<AppDefinition>();
        return appDefinitions;
    }
}