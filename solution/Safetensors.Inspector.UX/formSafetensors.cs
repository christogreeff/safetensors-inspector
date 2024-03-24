using Safetensors.Core;
using Safetensors.Storage;
namespace Safetensor.Inspector.UX
{
    /// <summary>
    /// Safetensors Inspector UX form
    /// </summary>
    public partial class formSafetensors : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public formSafetensors()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Drag and drop event handler for the tree view control.
        /// </summary>
        private void treeProperties_DragEnter(object sender, DragEventArgs e)
        {
            if (e?.Data != null)
                if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Drag and drop event handler for the tree view control
        /// </summary>
        private async void treeProperties_DragDrop(object sender, DragEventArgs e)
        {
            if ((e != null) && (e.Data != null))
            {
                // list of potential folders/files to be added
                List<string> listFiles = [];

                if (e.Data.GetData(DataFormats.FileDrop) is string[] droppedItems)
                {
                    foreach (string droppedItem in droppedItems)
                    {
                        FileAttributes attr = File.GetAttributes(droppedItem);

                        // check if a folder/directory
                        if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            if (Path.Exists(droppedItem))
                                listFiles.AddRange(Directory.GetFiles(droppedItem, "*.safetensors"));
                        }
                        else if (File.Exists(droppedItem))
                            listFiles.Add(droppedItem);
                    }

                    // if there are files to be added
                    if (listFiles.Count > 0)
                        await PopulateTreeViewWithFilesAsync([.. listFiles]);
                }
            }
        }

        /// <summary>
        /// Populate the treeview
        /// </summary>
        /// <remarks>
        /// Yes, this can be improved. For now it works well enough.
        /// </remarks>
        private async Task PopulateTreeViewWithFilesAsync(string[] files)
        {
            // clear UX tree view
            treeProperties.Nodes.Clear();

            // create temp list of nodes
            List<TreeNode> nodes = [];

            foreach (string file in files)
            {
                var fileNode = treeProperties.Nodes.Find(file, false);
                if (fileNode.Length == 0)
                {
                    // get file size in human readable format
                    var fiSize = new FileInfo(file).Length.SizeSuffix(2);

                    TreeNode safetensorsNode = new()
                    {
                        Name = file,
                        Text = $"{file} ({fiSize})",
                        ImageIndex = 0,
                        ToolTipText = $"The safetensors file {Path.GetFileName(file)} is {fiSize}"
                    };

                    // read a file and get its properties
                    SafetensorReader sr = new();
                    await sr.InitializeAsync();
                    var safetensor = await sr.ReadAsync(file);

                    if (safetensor != null)
                    {
                        foreach (var prop in safetensor.Metadata.Properties)
                        {
                            var propProps = prop?.ValueEx?.AsObject().ToList();
                            {
                                if (propProps == null)
                                {
                                    TreeNode propNode = new()
                                    {
                                        Name = prop?.PropertyName,
                                        ToolTipText = prop?.PropertyName,
                                        Text = $"{prop?.Description}: {prop?.Value}",
                                        ImageIndex = 1,
                                        SelectedImageIndex = 2
                                    };

                                    safetensorsNode.Nodes.Add(propNode);
                                }
                                else
                                {
                                    TreeNode propNode = new()
                                    {
                                        Name = prop?.PropertyName,
                                        ToolTipText = prop?.PropertyName,
                                        Text = $"{prop?.Description}",
                                        ImageIndex = 3,
                                        SelectedImageIndex = 4
                                    };

                                    propProps?.ForEach(propChild =>
                                    {
                                        TreeNode propChildNode = new()
                                        {
                                            Name = prop?.PropertyName,
                                            ToolTipText = $"{prop?.PropertyName} : {propChild.Key}",
                                            Text = $"{propChild.Key}",
                                            ImageIndex = 5,
                                            SelectedImageIndex = 6
                                        };

                                        propNode.Nodes.Add(propChildNode);

                                        var propChildProps = propChild.Value?.AsObject().ToList();
                                        propChildProps?
                                            .Select(z => new KeyValuePair<string, int>(Convert.ToString(z.Key).Trim(), Convert.ToInt32(z.Value?.GetValue<int>())))
                                            .OrderByDescending(z => z.Value).ThenBy(z => z.Key).ToList()
                                            .ForEach(y =>
                                            {
                                                propChildNode.Nodes.Add(new TreeNode()
                                                {
                                                    Name = prop?.PropertyName,
                                                    ToolTipText = $"{y.Key}",
                                                    Text = $"({y.Value}) {y.Key}",
                                                    ImageIndex = 5,
                                                    SelectedImageIndex = 6
                                                });
                                            });
                                    });

                                    safetensorsNode.Nodes.Add(propNode);
                                }
                            }
                        }
                    }

                    nodes.Add(safetensorsNode);
                }
            }

            // add list of nodes to UX tree
            treeProperties.Nodes.AddRange([.. nodes]);
        }
    }
}