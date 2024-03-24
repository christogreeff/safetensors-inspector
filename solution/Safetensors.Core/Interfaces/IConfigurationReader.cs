namespace Safetensors.Core
{
    /// <summary>
    /// Safetensor configuration reader interface
    /// </summary>
    public interface IConfigurationReader
    {
        /// <summary>
        /// Load metadata property configuration
        /// </summary>
        /// <returns>List of Safetensor metadata properties</returns>

        Task<IList<SafetensorsMetadataProperty>?> LoadMetadataPropertyConfigAsync();
    }
}