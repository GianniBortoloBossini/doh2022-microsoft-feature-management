using System;
using Microsoft.EntityFrameworkCore;

public class FeaturesContext : DbContext
{
    public DbSet<FeatureFlagDefinitionEntity> Features { get; set; }

    public string CnnStr { get; }

    public FeaturesContext()
    {
        var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
 
        var configuration = builder.Build();
        CnnStr = configuration.GetConnectionString("features_db").ToString();
        Console.WriteLine(CnnStr);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={CnnStr}");
}
