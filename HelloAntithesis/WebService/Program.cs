using Antithesis.SDK;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var random = Antithesis.SDK.Random.SharedThrowIfNativeLibraryNotExists;

app.MapGet("/", async () =>
{
    Assert.Always(random.NextDouble() < 0.9999, "Should Fail Randomly 1 of 10k");

    await Task.Delay(100);
    
    return "Hello Antithesis!";
});

Lifecycle.SetupComplete();

Assert.Reachable("Immediately After Setup Complete");

app.Run();

Assert.Unreachable("app.Run Blocks Indefinitely");