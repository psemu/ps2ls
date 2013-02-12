namespace ps2ls.Forms
{
    partial class MaterialBrowser
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.modelsCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.searchTexturesText = new System.Windows.Forms.ToolStripTextBox();
            this.clearSearchTexturesText = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportSelectedTexturesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.searchTexturesTimer = new System.Windows.Forms.Timer(this.components);
            this.texturesListBox = new ps2ls.Forms.Controls.CustomListBox();
            this.glControl1 = new ps2ls.Forms.MaterialBrowserGLControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.texturesListBox);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.glControl1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 1;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelsCountToolStripStatusLabel});
            this.statusStrip2.Location = new System.Drawing.Point(0, 578);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(274, 22);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 2;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // modelsCountToolStripStatusLabel
            // 
            this.modelsCountToolStripStatusLabel.Image = global::ps2ls.Properties.Resources.document_search_result;
            this.modelsCountToolStripStatusLabel.Name = "modelsCountToolStripStatusLabel";
            this.modelsCountToolStripStatusLabel.Size = new System.Drawing.Size(40, 17);
            this.modelsCountToolStripStatusLabel.Text = "0/0";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.searchTexturesText,
            this.clearSearchTexturesText,
            this.toolStripSeparator2,
            this.exportSelectedTexturesToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(274, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel3.Image = global::ps2ls.Properties.Resources.magnifier;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel3.Text = "toolStripLabel1";
            this.toolStripLabel3.ToolTipText = "Search";
            // 
            // searchTexturesText
            // 
            this.searchTexturesText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTexturesText.Name = "searchTexturesText";
            this.searchTexturesText.Size = new System.Drawing.Size(100, 25);
            this.searchTexturesText.TextChanged += new System.EventHandler(this.searchModelsText_TextChanged);
            // 
            // clearSearchTexturesText
            // 
            this.clearSearchTexturesText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearSearchTexturesText.Enabled = false;
            this.clearSearchTexturesText.Image = global::ps2ls.Properties.Resources.ui_text_field_clear_button;
            this.clearSearchTexturesText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearSearchTexturesText.Name = "clearSearchTexturesText";
            this.clearSearchTexturesText.Size = new System.Drawing.Size(23, 22);
            this.clearSearchTexturesText.Text = "Clear Search Text";
            this.clearSearchTexturesText.Click += new System.EventHandler(this.clearSearchModelsText_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // exportSelectedTexturesToolStripButton
            // 
            this.exportSelectedTexturesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportSelectedTexturesToolStripButton.Enabled = false;
            this.exportSelectedTexturesToolStripButton.Image = global::ps2ls.Properties.Resources.drive_download;
            this.exportSelectedTexturesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportSelectedTexturesToolStripButton.Name = "exportSelectedTexturesToolStripButton";
            this.exportSelectedTexturesToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.exportSelectedTexturesToolStripButton.Text = "Export Selected Models...";
            this.exportSelectedTexturesToolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // searchTexturesTimer
            // 
            this.searchTexturesTimer.Interval = 500;
            this.searchTexturesTimer.Tick += new System.EventHandler(this.searchModelsTimer_Tick);
            // 
            // texturesListBox
            // 
            this.texturesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.texturesListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.texturesListBox.FormattingEnabled = true;
            this.texturesListBox.Image = global::ps2ls.Properties.Resources.textured1;
            this.texturesListBox.Items.AddRange(new object[] {
            "CustomListBox"});
            this.texturesListBox.Location = new System.Drawing.Point(0, 25);
            this.texturesListBox.Name = "texturesListBox";
            this.texturesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.texturesListBox.Size = new System.Drawing.Size(274, 553);
            this.texturesListBox.TabIndex = 3;
            this.texturesListBox.SelectedIndexChanged += new System.EventHandler(this.modelsListBox_SelectedIndexChanged);
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(522, 600);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // MaterialBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MaterialBrowser";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.MaterialBrowserControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox searchTexturesText;
        private System.Windows.Forms.ToolStripButton clearSearchTexturesText;
        private System.Windows.Forms.Timer searchTexturesTimer;
        private ps2ls.Forms.Controls.CustomListBox texturesListBox;
        private System.Windows.Forms.ToolStripStatusLabel modelsCountToolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton exportSelectedTexturesToolStripButton;
        private MaterialBrowserGLControl glControl1;
    }
}
