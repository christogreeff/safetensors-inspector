namespace Safetensors.Core
{
    /// <summary>
    /// Safetensor metadata
    /// </summary>
    public class SafetensorsMetadata : ISafetensorsMetadata
    {
        /// <summary>
        /// Default __metadata__ property name
        /// </summary>
        public string PropertyName { get; set; } = "__metadata__";

        /// <summary>
        /// Safetensor metadata description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Safeensor metadata properties
        /// </summary>
        public List<ISafetensorsMetadataProperty> Properties { get; set; } = [];
    }
}