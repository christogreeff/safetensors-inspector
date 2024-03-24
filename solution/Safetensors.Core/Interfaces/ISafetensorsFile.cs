namespace Safetensors.Core
{
    public interface ISafetensorsFile
    {
        string Header { get; }
        ISafetensorsMetadata Metadata { get; }
        string Path { get; }
    }
}