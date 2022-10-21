public class SqlLiteFeatureDefinitionProvider : IFeatureFlagDefinitionProvider
{
    public Task<FeatureFlagDefinition> GetFeatureFlagDefinitionAsync(string featureflagName, CancellationToken cancellationToken = default)
    {
        var featureDefinition = featureflagName switch 
            {
                nameof(FeatureFlags.LogTime) => GetFeatureFlag(featureflagName),
                _ => throw new NotSupportedException("The requested feature is not supported.")
            };

        return Task.FromResult(featureDefinition);
    }

    public async IAsyncEnumerable<FeatureFlagDefinition> GetAllFeatureFlagDefinitionsAsync(CancellationToken cancellationToken = default)
    {
        foreach (var featureDefinition in new[]
            {
                await GetFeatureFlagDefinitionAsync(nameof(FeatureFlags.LogTime))
        })
        {
            yield return featureDefinition;
        }
    }

    private FeatureFlagDefinition GetFeatureFlag(string featureName)
    {
        Console.WriteLine($"Alla ricerca della feature con Name={featureName}");
        FeaturesContext db = new FeaturesContext();
        var val = db.Features
             .First(f => f.Name == featureName)
             .ToDefinition();   
        Console.WriteLine($"featureName={featureName} => {(val.EnabledFor.Any() ? val.EnabledFor.First().Name : "???")}");
        return val;
    }
}