
namespace Safetensor.Inspector.UX
{
    partial class formSafetensors
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSafetensors));
            treeProperties = new TreeView();
            imageList = new ImageList(components);
            SuspendLayout();
            // 
            // treeProperties
            // 
            treeProperties.AllowDrop = true;
            treeProperties.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeProperties.ImageIndex = 0;
            treeProperties.ImageList = imageList;
            treeProperties.Location = new Point(12, 12);
            treeProperties.Name = "treeProperties";
            treeProperties.SelectedImageIndex = 0;
            treeProperties.ShowNodeToolTips = true;
            treeProperties.Size = new Size(899, 593);
            treeProperties.TabIndex = 0;
            treeProperties.DragDrop += treeProperties_DragDrop;
            treeProperties.DragEnter += treeProperties_DragEnter;
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = Color.Transparent;
            imageList.Images.SetKeyName(0, "safetensor.ico");
            imageList.Images.SetKeyName(1, "PropertyGray.ico");
            imageList.Images.SetKeyName(2, "PropertyGreen.ico");
            imageList.Images.SetKeyName(3, "XtraGray.ico");
            imageList.Images.SetKeyName(4, "XtraRed.ico");
            imageList.Images.SetKeyName(5, "HashGray.ico");
            imageList.Images.SetKeyName(6, "HashMagenta.ico");
            // 
            // formSafetensors
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(932, 617);
            Controls.Add(treeProperties);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "formSafetensors";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Safetensors Inspector";
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeProperties;
        private ImageList imageList;
    }
}
