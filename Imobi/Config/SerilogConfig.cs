using Serilog;

namespace Imobi.Config;

public class SerilogConfig
{
    public static void Configure()
    {
        var root = Directory.GetCurrentDirectory();
        var logsDirectory = Path.Combine(root, "logs");

        if (!Directory.Exists(logsDirectory))
            Directory.CreateDirectory(logsDirectory);

        var logFilePath = Path.Combine(logsDirectory, "log-.txt");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();
    }
}
