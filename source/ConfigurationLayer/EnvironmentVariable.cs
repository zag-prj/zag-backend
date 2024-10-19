using DotNetEnv;

namespace source.ConfigurationLayer;

/// <summary>
/// This class provides static properties for accessing environment variables.
/// It also handles loading .env files when running locally.
/// </summary>
public class EnvironmentVariable
{
    /// <summary>
    /// Gets the ASP_PORT environment variable as an integer.
    /// If the variable is not set or cannot be parsed, returns 8080 as the default value.
    /// </summary>
    public static int ASP_PORT => int.TryParse(Environment.GetEnvironmentVariable("ASP_PORT"), out int port) ? port : 8080;

    /// <summary>
    /// Gets the POSTGRES_CONNECTION_STRING environment variables and constructs a connection string.
    /// If any variable is not set, uses default values.
    /// </summary>
    public static string POSTGRES_CONNECTION_STRING =>
        $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost"};" +
        $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432"};" +
        $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres"};" +
        $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "postgres"};" +
        $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "postgres"}";

    /// <summary>
    /// Constructor that checks if the application is running in a Docker container.
    /// If running locally, loads the .env file.
    /// If running in Docker, uses the Docker environment variables.
    /// </summary>
    static EnvironmentVariable()
    {
        bool isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != null;
        if (!isDocker)
        {
            Console.WriteLine("Running locally. Loading .env file...");
            Env.Load();
        }
        else
        {
            Console.WriteLine("Running in Docker. Using Docker environment variables...");
        }
    }
}
