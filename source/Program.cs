using source.ConfigurationLayer;
using source.DataAccessLayer;
using source.BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);
{
    // configure services (DI)
    builder.Services.AddScoped<ClientService>();
    builder.Services.AddScoped<ContractService>();
    builder.Services.AddScoped<TechnicianService>();
    builder.Services.AddScoped<ContactService>();
    builder.Services.AddScoped<MaintenanceJobService>();
    builder.Services.AddScoped<Postgress>();
    builder.Services.AddControllers();
}
var app = builder.Build();
{

    //add delay to wait for postgress to start
    //System.Threading.Thread.Sleep(10000);
    // Initialize database connection
    //using var db = new Postgress();

    /// <summary>
    /// Configures the web application to handle HTTP GET requests at the root ("/") URL.
    /// </summary>
    /// <returns>A string "Hello World!" as the response.</returns>
    //app.MapGet("/", () => "Hello World!");

    // configure routing
    app.MapControllers();

    // configure logging
    app.UseDeveloperExceptionPage();
}

/// <summary>
/// Runs the web application with a specified base URL.
/// The base URL is constructed using the value of the ASP_PORT environment variable.
/// </summary>
/// <param name="ASP_PORT">The port number to listen on. This value should be provided as an environment variable.</param>
app.Run($"http://*:{EnvironmentVariable.ASP_PORT}");