using Safetensors.Core;
using System.Text.Json;

namespace Safetensors.Storage
{
    /// <summary>
    /// Configuration reader
    /// </summary>
    public class ConfigurationReader : IConfigurationReader
    {
        /// <summary>
        /// Load metadata property configuration
        /// </summary>
        /// <returns>List of Safetensor metadata properties</returns>
        public async Task<IList<SafetensorsMetadataProperty>?> LoadMetadataPropertyConfigAsync()
        {
            using var reader = new StreamReader(@"./metadata.json");

            var results = await JsonSerializer.DeserializeAsync<IList<SafetensorsMetadataProperty>>(reader.BaseStream);

            return results;
        }
    }
}
