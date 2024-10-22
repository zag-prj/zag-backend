using BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);

{
    // configure services (DI)
    builder.Services.AddScoped<ClientService>();
    builder.Services.AddScoped<ContractService>();
    builder.Services.AddScoped<TechnicianService>();
    builder.Services.AddScoped<ContactService>();
    builder.Services.AddScoped<MaintenanceJobService>();
    builder.Services.AddControllers();
}

var app = builder.Build();

{
    // configure request pipeline
    app.MapControllers();
}

app.Run();