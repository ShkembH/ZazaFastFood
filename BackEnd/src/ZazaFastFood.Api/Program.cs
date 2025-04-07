using ZazaFastFood.Infrastructure.Data; // Correct namespace for DbContext? Verify folder name (Data or Persistence)
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- Configure Services ---

// 1. Add DbContext (using SQL Server provider)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ZazaDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        // Tell EF Core where migrations are stored (assuming DbContext is in Infrastructure)
        sqlServerOptions.MigrationsAssembly(typeof(ZazaDbContext).Assembly.FullName);
    })
        .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()) // Optional: Dev logging
        .EnableDetailedErrors(builder.Environment.IsDevelopment())     // Optional: Dev logging
);

// 2. Add Controllers (for API controller classes)
builder.Services.AddControllers();

// 3. Add Standard Swagger/OpenAPI services (replaces AddOpenApi())
builder.Services.AddEndpointsApiExplorer(); // Needed for API discovery
builder.Services.AddSwaggerGen();          // Needed for generating OpenAPI spec

// --- Build the Application ---
var app = builder.Build();

// --- Configure the HTTP request pipeline ---

// Enable Swagger/SwaggerUI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      // Serves the generated swagger.json file
    app.UseSwaggerUI(); // Serves the interactive Swagger UI page (/swagger)
    // DO NOT ADD app.MapOpenApi(); here
}

app.UseHttpsRedirection();

// TODO: Add Authentication/Authorization middleware later
// app.UseAuthentication(); // Example
// app.UseAuthorization();

// TODO: Add CORS middleware later if needed for React frontend
// app.UseCors(...); // Example

app.MapControllers(); // Maps requests to your API Controllers

// --- Keep the WeatherForecast example for now (or remove it later) ---
var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
app.MapGet("/weatherforecast", () => { /* ... weather forecast logic ... */ })
.WithName("GetWeatherForecast");
// --- End WeatherForecast Example ---

app.Run();

// --- WeatherForecast Record (keep or remove later) ---
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) { /* ... Temp F logic ... */ }