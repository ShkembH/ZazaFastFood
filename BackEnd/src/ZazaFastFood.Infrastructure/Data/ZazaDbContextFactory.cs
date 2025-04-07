using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; // Required for reading configuration
using System.IO;                         // Required for Path operations

namespace ZazaFastFood.Infrastructure.Data; // Or .Persistence if you used that folder

// Implement the IDesignTimeDbContextFactory interface
public class ZazaDbContextFactory : IDesignTimeDbContextFactory<ZazaDbContext>
{
    public ZazaDbContext CreateDbContext(string[] args)
    {
        // --- Step 1: Get the connection string ---

        // Create a ConfigurationBuilder to read appsettings.json
        // It needs to find the appsettings.json file of the API project
        // We assume this factory is in Infrastructure, and the API project is one level up
        // Adjust the path if your structure is different
        var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../ZazaFastFood.Api"));

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // Set the path to the API project's directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            // Optional: You might want to add appsettings.Development.json too
            // .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        // Get the connection string from the configuration
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json of the startup project.");
        }

        // --- Step 2: Create DbContextOptions ---
        var optionsBuilder = new DbContextOptionsBuilder<ZazaDbContext>();

        // Configure the optionsBuilder to use SQL Server with the connection string
        optionsBuilder.UseSqlServer(connectionString, sqlServerOptions =>
        {
            // IMPORTANT: Tell EF Core where migrations are stored for THIS context
            // Since this factory is in Infrastructure, typeof(ZazaDbContext).Assembly is correct
            sqlServerOptions.MigrationsAssembly(typeof(ZazaDbContext).Assembly.FullName);
        });

        // --- Step 3: Create and return the DbContext instance ---
        return new ZazaDbContext(optionsBuilder.Options);
    }
}