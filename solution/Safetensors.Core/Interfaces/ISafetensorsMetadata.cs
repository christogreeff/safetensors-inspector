
namespace Safetensors.Core
{
    public interface ISafetensorsMetadata
    {
        string Description { get; set; }
        List<ISafetensorsMetadataProperty> Properties { get; set; }
        string PropertyName { get; set; }
    }
}