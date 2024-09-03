using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProductManagement.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ProductManagementDbContextFactory : IDesignTimeDbContextFactory<ProductManagementDbContext>
{
    public ProductManagementDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        ProductManagementEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<ProductManagementDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new ProductManagementDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ProductManagement.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
