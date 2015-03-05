namespace SharpMap.Desktop
{
    partial class MapEditorItem
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
            this.panelExpander = new System.Windows.Forms.Panel();
            this.pictureExpander = new System.Windows.Forms.PictureBox();
            this.panelLabel = new System.Windows.Forms.Panel();
            this.label = new System.Windows.Forms.Label();
            this.panelCheck = new System.Windows.Forms.Panel();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.panelSub = new System.Windows.Forms.Panel();
            this.panelIcon = new System.Windows.Forms.Panel();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.panelExpander.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureExpander)).BeginInit();
            this.panelLabel.SuspendLayout();
            this.panelCheck.SuspendLayout();
            this.panelIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // panelExpander
            // 
            this.panelExpander.BackColor = System.Drawing.SystemColors.Control;
            this.panelExpander.Controls.Add(this.pictureExpander);
            this.panelExpander.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelExpander.Location = new System.Drawing.Point(0, 0);
            this.panelExpander.Name = "panelExpander";
            this.panelExpander.Size = new System.Drawing.Size(15, 111);
            this.panelExpander.TabIndex = 7;
            this.panelExpander.MouseClick += new System.Windows.Forms.MouseEventHandler(this.control_MouseClick);
            this.panelExpander.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            this.panelExpander.MouseMove += new System.Windows.Forms.MouseEventHandler(this.control_MouseMove);
            this.panelExpander.MouseUp += new System.Windows.Forms.MouseEventHandler(this.control_MouseUp);
            // 
            // pictureExpander
            // 
            this.pictureExpander.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureExpander.BackColor = System.Drawing.Color.Transparent;
            this.pictureExpander.Image = global::SharpMap.Desktop.Resources.minus;
            this.pictureExpander.Location = new System.Drawing.Point(0, 0);
            this.pictureExpander.Name = "pictureExpander";
            this.pictureExpander.Size = new System.Drawing.Size(15, 20);
            this.pictureExpander.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureExpander.TabIndex = 4;
            this.pictureExpander.TabStop = false;
            this.pictureExpander.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // panelLabel
            // 
            this.panelLabel.BackColor = System.Drawing.Color.Transparent;
            this.panelLabel.Controls.Add(this.label);
            this.panelLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLabel.Location = new System.Drawing.Point(60, 0);
            this.panelLabel.Name = "panelLabel";
            this.panelLabel.Size = new System.Drawing.Size(176, 20);
            this.panelLabel.TabIndex = 8;
            this.panelLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.control_MouseClick);
            this.panelLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            this.panelLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.control_MouseMove);
            this.panelLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.control_MouseUp);
            // 
            // label
            // 
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(176, 20);
            this.label.TabIndex = 2;
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label.MouseClick += new System.Windows.Forms.MouseEventHandler(this.control_MouseClick);
            this.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            this.label.MouseMove += new System.Windows.Forms.MouseEventHandler(this.control_MouseMove);
            this.label.MouseUp += new System.Windows.Forms.MouseEventHandler(this.control_MouseUp);
            // 
            // panelCheck
            // 
            this.panelCheck.BackColor = System.Drawing.Color.Transparent;
            this.panelCheck.Controls.Add(this.checkBox);
            this.panelCheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCheck.Location = new System.Drawing.Point(15, 0);
            this.panelCheck.Margin = new System.Windows.Forms.Padding(0);
            this.panelCheck.Name = "panelCheck";
            this.panelCheck.Size = new System.Drawing.Size(22, 111);
            this.panelCheck.TabIndex = 9;
            this.panelCheck.MouseClick += new System.Windows.Forms.MouseEventHandler(this.control_MouseClick);
            this.panelCheck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            this.panelCheck.MouseMove += new System.Windows.Forms.MouseEventHandler(this.control_MouseMove);
            this.panelCheck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.control_MouseUp);
            // 
            // checkBox
            // 
            this.checkBox.Location = new System.Drawing.Point(5, 4);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(14, 14);
            this.checkBox.TabIndex = 1;
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.Click += new System.EventHandler(this.checkBox_Click);
            // 
            // panelSub
            // 
            this.panelSub.AutoSize = true;
            this.panelSub.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelSub.BackColor = System.Drawing.Color.Transparent;
            this.panelSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSub.Location = new System.Drawing.Point(60, 20);
            this.panelSub.Name = "panelSub";
            this.panelSub.Size = new System.Drawing.Size(176, 91);
            this.panelSub.TabIndex = 6;
            this.panelSub.MouseClick += new System.Windows.Forms.MouseEventHandler(this.control_MouseClick);
            this.panelSub.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            this.panelSub.MouseMove += new System.Windows.Forms.MouseEventHandler(this.control_MouseMove);
            this.panelSub.MouseUp += new System.Windows.Forms.MouseEventHandler(this.control_MouseUp);
            // 
            // panelIcon
            // 
            this.panelIcon.BackColor = System.Drawing.Color.Transparent;
            this.panelIcon.Controls.Add(this.pictureBoxIcon);
            this.panelIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIcon.Location = new System.Drawing.Point(37, 0);
            this.panelIcon.Margin = new System.Windows.Forms.Padding(0);
            this.panelIcon.Name = "panelIcon";
            this.panelIcon.Size = new System.Drawing.Size(23, 111);
            this.panelIcon.TabIndex = 10;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxIcon.Image = global::SharpMap.Desktop.Resources.folder;
            this.pictureBoxIcon.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(23, 20);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxIcon.TabIndex = 4;
            this.pictureBoxIcon.TabStop = false;
            // 
            // MapEditorItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelSub);
            this.Controls.Add(this.panelLabel);
            this.Controls.Add(this.panelIcon);
            this.Controls.Add(this.panelCheck);
            this.Controls.Add(this.panelExpander);
            this.Name = "MapEditorItem";
            this.Size = new System.Drawing.Size(236, 111);
            this.panelExpander.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureExpander)).EndInit();
            this.panelLabel.ResumeLayout(false);
            this.panelCheck.ResumeLayout(false);
            this.panelIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelExpander;
        private System.Windows.Forms.Panel panelLabel;
        private System.Windows.Forms.Panel panelCheck;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel panelSub;
        private System.Windows.Forms.PictureBox pictureExpander;
        private System.Windows.Forms.Panel panelIcon;
        private System.Windows.Forms.PictureBox pictureBoxIcon;

    }
}
