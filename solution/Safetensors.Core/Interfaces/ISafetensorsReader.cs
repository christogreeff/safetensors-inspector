namespace Safetensors.Core
{
    /// <summary>
    /// Safeensor reader interface
    /// </summary>
    public interface ISafetensorsReader
    {
        /// <summary>
        /// Initialize the Safetenso reader
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Async read of .safetensor file
        /// </summary>
        /// <param name="path">File path of the .safetensor file</param>
        /// <returns>SafetensorFile object</returns>
        Task<ISafetensorsFile?> ReadAsync(string path);
    }
}