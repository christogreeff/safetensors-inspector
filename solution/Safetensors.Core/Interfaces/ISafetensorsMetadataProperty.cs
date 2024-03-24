using System.Text.Json.Nodes;

namespace Safetensors.Core
{
    public interface ISafetensorsMetadataProperty
    {
        string Description { get; set; }
        string? MetadataType { get; set; }
        string PropertyName { get; set; }
        string Value { get; set; }
        JsonNode? ValueEx { get; }
    }
}