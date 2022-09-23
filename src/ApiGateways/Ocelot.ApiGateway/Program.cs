using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.WebHost.ConfigureAppConfiguration((ctx, conf) => {
    conf.AddJsonFile($"ocelot.{ctx.HostingEnvironment.EnvironmentName}.json", true, true);
});

builder.Services.AddOcelot();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
