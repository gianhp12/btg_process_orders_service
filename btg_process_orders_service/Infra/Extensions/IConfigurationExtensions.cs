namespace btg_process_orders_service.Infra.Extensions;

public static class IConfigurationExtensions
{
    public static IConfigurationSection GetEnvironmentSettings(this IConfiguration configuration, string section)
    {
        var environment = configuration.GetEnvironmentString();
        var settings = configuration.GetSection($"{section}:{environment}");
        if (!settings.Exists())
            throw new Exception("Error: environment not detected");
        return settings;
    }

    public static string GetEnvironmentString(this IConfiguration configuration)
    {
        var environmentKey = configuration.GetValue<string>("EnvironmentKey")!;
        var environment = Environment.GetEnvironmentVariable(environmentKey);
        if (string.IsNullOrEmpty(environment)) throw new Exception("Error: environment not detected");
        return environment;
    }
}
