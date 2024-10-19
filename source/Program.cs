using source.ConfigurationLayer;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Add port based on varable from environment
//define port from int
app.Run($"http://*:{EnvironmentVariable.ASP_PORT}");
