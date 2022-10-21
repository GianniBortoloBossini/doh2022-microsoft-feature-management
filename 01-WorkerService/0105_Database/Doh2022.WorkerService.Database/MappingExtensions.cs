using Microsoft.Extensions.Primitives;
using Microsoft.FeatureManagement.FeatureFilters;

public static class MappingExtensions
{
    public static FeatureFlagDefinition ToDefinition(this FeatureFlagDefinitionEntity entity)
        => entity.Type switch 
        {
            "Flag" => entity.Value == "AlwaysOn" ?
                        new FeatureFlagDefinition{
                            Name = entity.Name,
                            EnabledFor =  new[] {
                                new FeatureFilterConfiguration{
                                    Name = entity.Value
                                }
                            }
                        } : new FeatureFlagDefinition {
                            Name = entity.Name
                        },
            "Percentage" => new FeatureFlagDefinition{
                Name = entity.Name,
                EnabledFor = new[] {
                    new  FeatureFilterConfiguration{
                        Name = "Percentage",
                        Parameters = new PercentageConfiguration(int.Parse(entity.Value))
                    }
                }
            }, 
            _ => throw new NotImplementedException()
        };

    internal class PercentageConfiguration : IConfiguration
    {
        private readonly int _percentage;

        public PercentageConfiguration(int percentage)
        {
            _percentage = percentage;
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            // NOTE: no children
            return Enumerable.Empty<IConfigurationSection>();
        }

        public IChangeToken GetReloadToken()
        {
            // NOTE: this is not supported and not consumed either
            throw new NotSupportedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            // NOTE: this is not supported and not consumed either
            throw new NotSupportedException();
        }

        public string this[string key]
        {
            get => _percentage.ToString(); // this produces the requested value

            // NOTE: this is not supported and not consumed either
            set => throw new NotSupportedException();
        }
    }
}