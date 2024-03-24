namespace Safetensors.Core
{
    /// <summary>
    /// Safetensor file
    /// </summary>
    /// <remarks>
    /// https://huggingface.co/docs/safetensors/en/metadata_parsing
    /// https://cran.r-project.org/web/packages/safetensors/safetensors.pdf
    /// https://civitai.com/articles/827/safetensors-inspector-google-colab
    /// </remarks>
    public class SafetensorsFile(string path, string header) : ISafetensorsFile
    {
        /// <summary>
        /// Safetensor file location
        /// </summary>
        public string Path { get; private set; } = path;

        /// <summary>
        /// Safetensor raw metadata
        /// </summary>
        public string Header { get; private set; } = header;

        /// <summary>
        /// Safetensor metadata
        /// </summary>
        public ISafetensorsMetadata Metadata { get; private set; } = new SafetensorsMetadata();
    }
}