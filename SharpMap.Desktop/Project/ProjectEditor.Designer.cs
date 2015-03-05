namespace SharpMap.Desktop
{
    partial class ProjectEditor
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectEditor));
            this.toolStripComboBoxMaps = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonAdd = new System.Windows.Forms.ToolStripSplitButton();
            this.addMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.vectorLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rasterLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.mapEditor = new SharpMap.Desktop.MapEditor();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripComboBoxMaps
            // 
            this.toolStripComboBoxMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxMaps.Name = "toolStripComboBoxMaps";
            this.toolStripComboBoxMaps.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBoxMaps.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxMaps_SelectedIndexChanged);
            this.toolStripComboBoxMaps.Click += new System.EventHandler(this.toolStripComboBoxMaps_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxMaps,
            this.toolStripSplitButtonAdd,
            this.toolStripButton1,
            this.toolStripDropDownButton1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(305, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripSplitButtonAdd
            // 
            this.toolStripSplitButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addMapToolStripMenuItem,
            this.addFolderToolStripMenuItem,
            this.toolStripMenuItem1,
            this.vectorLayerToolStripMenuItem,
            this.rasterLayerToolStripMenuItem});
            this.toolStripSplitButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonAdd.Image")));
            this.toolStripSplitButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonAdd.Name = "toolStripSplitButtonAdd";
            this.toolStripSplitButtonAdd.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButtonAdd.Text = "toolStripSplitButton1";
            // 
            // addMapToolStripMenuItem
            // 
            this.addMapToolStripMenuItem.Name = "addMapToolStripMenuItem";
            this.addMapToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addMapToolStripMenuItem.Text = "Add map...";
            this.addMapToolStripMenuItem.Click += new System.EventHandler(this.addMapToolStripMenuItem_Click);
            // 
            // addFolderToolStripMenuItem
            // 
            this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addFolderToolStripMenuItem.Text = "Add folder...";
            this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.addFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // vectorLayerToolStripMenuItem
            // 
            this.vectorLayerToolStripMenuItem.Name = "vectorLayerToolStripMenuItem";
            this.vectorLayerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vectorLayerToolStripMenuItem.Text = "Vector layer...";
            this.vectorLayerToolStripMenuItem.Click += new System.EventHandler(this.vectorLayerToolStripMenuItem_Click);
            // 
            // rasterLayerToolStripMenuItem
            // 
            this.rasterLayerToolStripMenuItem.Name = "rasterLayerToolStripMenuItem";
            this.rasterLayerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rasterLayerToolStripMenuItem.Text = "Raster layer...";
            this.rasterLayerToolStripMenuItem.Click += new System.EventHandler(this.rasterLayerToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // mapEditor
            // 
            this.mapEditor.BackColor = System.Drawing.Color.White;
            this.mapEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapEditor.Location = new System.Drawing.Point(0, 25);
            this.mapEditor.MapBox = null;
            this.mapEditor.MapName = null;
            this.mapEditor.Name = "mapEditor";
            this.mapEditor.Size = new System.Drawing.Size(305, 305);
            this.mapEditor.TabIndex = 1;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "ASASD";
            // 
            // ProjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mapEditor);
            this.Controls.Add(this.toolStrip);
            this.Name = "ProjectEditor";
            this.Size = new System.Drawing.Size(305, 330);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMaps;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonAdd;
        private System.Windows.Forms.ToolStripMenuItem addMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vectorLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rasterLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private MapEditor mapEditor;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;

    }
}
