using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => Results.Content("""
<!doctype html><html lang="en"><meta charset="utf-8"><meta name="viewport" content="width=device-width">
<title>WP1 • Azure starter</title><style>body{font:18px system-ui;max-width:760px;margin:12vh auto;padding:24px;background:#101b2b;color:#eef6ff}h1{font-size:48px}a{color:#7dd3fc}code{color:#86efac}</style>
<p>MID-STATE • WEB PROGRAMMING 1</p><h1>Your .NET 10 app is running.</h1>
<p>Edit this page, commit to <code>main</code>, and watch GitHub Actions build and deploy your container.</p>
<p><a href="/health">Application health</a> · <a href="/health/db">Database connectivity</a></p>
<p>This is a teaching starter. Use fictional data. Add authentication before building private features.</p></html>
""", "text/html"));
app.MapGet("/health", () => Results.Ok(new { status = "ok", runtime = Environment.Version.ToString() }));
app.MapGet("/health/db", async () =>
{
    var connectionString = app.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionString))
        return Results.Problem("Database is not configured.", statusCode: 503);
    try
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        await using var command = new SqlCommand("SELECT 1", connection) { CommandTimeout = 10 };
        await command.ExecuteScalarAsync();
        return Results.Ok(new { database = "connected" });
    }
    catch (SqlException)
    {
        app.Logger.LogWarning("Database connectivity check failed. Inspect database configuration privately.");
        return Results.Problem("Database is unavailable.", statusCode: 503);
    }
});
app.Run();
