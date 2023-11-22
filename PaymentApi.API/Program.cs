var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("api/map", async (context) =>
    {
        await context.Response.WriteAsync("common method Get");
    });
    endpoints.MapGet("api/mapget", async (context) =>
    {
        await context.Response.WriteAsync("only Get req's");
    });
    endpoints.MapPost("api/mappost", async (context) =>
    {
        await context.Response.WriteAsync("only Post req's");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync("only Post req's");
});

app.Run();
