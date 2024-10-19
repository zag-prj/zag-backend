using System;
using DotNetEnv;


namespace source.ConfigurationLayer;

public class EnvironmentVariable
{
    public static string TEST => Environment.GetEnvironmentVariable("TEST") ?? string.Empty;
    public static int ASP_PORT => int.TryParse(Environment.GetEnvironmentVariable("ASP_PORT"), out int port) ? port : 8080;

    static EnvironmentVariable()
    {
        // Check if the app is running inside Docker by looking for Docker-specific environment variable
        bool isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != null;
        if (!isDocker)
        {
            // Load the .env file for local development if not in Docker
            Console.WriteLine("Running locally. Loading .env file...");
            Env.Load();
        }
        else
        {
            Console.WriteLine("Running in Docker. Using Docker environment variables...");
        }
    }

}
