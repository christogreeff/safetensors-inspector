using Safetensors.Core;
using System.Text.Json.Nodes;

namespace Safetensors.Storage
{
    /// <summary>
    /// Safeensor reader
    /// </summary>
    public class SafetensorReader : ISafetensorsReader
    {
        /// <summary>
        /// Safetensor header size in bytes
        /// </summary>
        private const int HEADER_SIZE = 8;

        /// <summary>
        /// Metadata configuration
        /// </summary>
        private IList<SafetensorsMetadataProperty>? _metadataConfig { get; set; }

        /// <summary>
        /// Initialize the Safetenso reader
        /// </summary>
        public async Task InitializeAsync()
        {
            var cr = new ConfigurationReader();
            _metadataConfig = await cr.LoadMetadataPropertyConfigAsync();
        }

        /// <summary>
        /// Async read of .safetensor file
        /// </summary>
        /// <param name="path">File path of the .safetensor file</param>
        /// <returns>SafetensorFile object</returns>
        public async Task<ISafetensorsFile?> ReadAsync(string path)
        {
            if (File.Exists(path))
            {
                // open file
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // get size of the header from first 8 bytes
                byte[] arrHeaderSize = new byte[HEADER_SIZE];
                await fs.ReadAsync(arrHeaderSize);

                // get header size
                UInt64 headerSize = BitConverter.ToUInt64(arrHeaderSize, 0);

                // reader header into buffer
                byte[] arrHeader = new byte[headerSize];
                await fs.ReadAsync(arrHeader);

                // convert header to string
                var strHeader = System.Text.Encoding.ASCII.GetString(arrHeader);

                // create safetensor file object
                var sf = new SafetensorsFile(path, strHeader);

                // parse the header
                JsonNode? jsonNode = JsonNode.Parse(strHeader);
                if (jsonNode != null)
                {
                    // get the default __metadata__ property
                    var nodeMetadata = jsonNode[sf.Metadata.PropertyName];

                    // if property exists and metadata configuration exists
                    if ((nodeMetadata != null) && (_metadataConfig != null))
                    {
                        await PopulateMetadataProperties(sf.Metadata, nodeMetadata);
                    }
                }

                return sf;
            }

            return null;
        }

        /// <summary>
        /// Populate metadata properties for the safetensor file object
        /// </summary>
        /// <param name="metadata">Safetensor metadata</param>
        /// <param name="nodeMetadata">Json node data loaded from the safetensor file</param>
        /// <returns></returns>
        private async Task PopulateMetadataProperties(ISafetensorsMetadata metadata, JsonNode nodeMetadata)
        {
            await Task.Run(() =>
                _metadataConfig?.ToList().ForEach(p =>
                {
                    // try to get string value
                    var data = nodeMetadata[p.PropertyName]?.ToString();

                    // check if data exists and add if it does
                    if (!string.IsNullOrEmpty(data))
                        metadata.Properties.Add(
                            new SafetensorsMetadataProperty
                            {
                                PropertyName = p.PropertyName,
                                Description = p.Description,
                                Value = data,
                                MetadataType = p.MetadataType
                            });
                })
            );
        }
    }
}
