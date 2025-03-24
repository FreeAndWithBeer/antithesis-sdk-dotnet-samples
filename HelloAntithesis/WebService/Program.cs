using Antithesis.SDK;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    Assert.Always(false, "Harcoded to Fail");
    
    return "Hello Antithesis!";
});

Lifecycle.SetupComplete();

Assert.Reachable("Immediately After Setup Complete");

app.Run();

Assert.Unreachable("app.Run Blocks Indefinitely");