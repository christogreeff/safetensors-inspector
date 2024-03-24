using System.Text.Json.Nodes;

namespace Safetensors.Core
{
    /// <summary>
    /// Safeensor metadata property
    /// </summary>
    public class SafetensorsMetadataProperty : ISafetensorsMetadataProperty
    {
        /// <summary>
        /// Safeensor metadata property name
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// Safetenso metadata property description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Safeensor metadata property value
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Safeensor metadata property value extended
        /// </summary>
        public JsonNode? ValueEx
        {
            get
            {
                return Convert.ToString(MetadataType)?.ToLower() switch
                {
                    "tagfrequency" or
                    "datasetdirs" => ParseJson(),
                    _ => null,
                };
            }
        }

        /// <summary>
        /// Safeensor metadata property type
        /// </summary>
        public string? MetadataType { get; set; } = string.Empty;

        /// <summary>
        /// Try parse Json
        /// </summary>
        /// <returns></returns>
        private JsonNode? ParseJson()
        {
            if (!string.IsNullOrEmpty(Value))
            {
                var node = JsonNode.Parse(Value);

                return node;
            }

            return null;
        }
    }
}