using source.ConfigurationLayer;
using source.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Initialize database connection
using var db = new Postgress();

/// <summary>
/// Configures the web application to handle HTTP GET requests at the root ("/") URL.
/// </summary>
/// <returns>A string "Hello World!" as the response.</returns>
app.MapGet("/", () => "Hello World!");

/// <summary>
/// Runs the web application with a specified base URL.
/// The base URL is constructed using the value of the ASP_PORT environment variable.
/// </summary>
/// <param name="ASP_PORT">The port number to listen on. This value should be provided as an environment variable.</param>
app.Run($"http://*:{EnvironmentVariable.ASP_PORT}");