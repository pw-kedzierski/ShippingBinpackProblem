var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();

// Configure CORS to allow all connections
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// CORS must be enabled before other middleware
app.UseCors("AllowAll");
app.UseHttpsRedirection();

// Proxy endpoint for fleet API
app.MapGet("/api/fleets/random", async (HttpContext context, IHttpClientFactory httpClientFactory) =>
{
    var httpClient = httpClientFactory.CreateClient();
    httpClient.Timeout = TimeSpan.FromSeconds(30);
    var externalApiUrl = "https://esa.instech.no/api/fleets/random";

    try
    {
        var response = await httpClient.GetAsync(externalApiUrl);

        if (!response.IsSuccessStatusCode)
        {
            context.Response.StatusCode = (int)response.StatusCode;
            context.Response.ContentType = "application/json";
            var errorContent = await response.Content.ReadAsStringAsync();
            await context.Response.WriteAsJsonAsync(new { error = $"External API returned status {response.StatusCode}", details = errorContent });
            return;
        }

        var content = await response.Content.ReadAsStringAsync();
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(content);
    }
    catch (TaskCanceledException)
    {
        context.Response.StatusCode = 504;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { error = "Request timeout", message = "The external API did not respond in time" });
    }
    catch (HttpRequestException ex)
    {
        context.Response.StatusCode = 502;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { error = "Bad gateway", message = ex.Message });
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { error = "Internal server error", message = ex.Message });
    }
})
.WithName("GetRandomFleet");

app.Run();
