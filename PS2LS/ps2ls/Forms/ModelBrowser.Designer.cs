namespace ps2ls.Forms
{
    partial class ModelBrowser
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
            ps2ls.Cameras.ArcBallCamera arcBallCamera1 = new ps2ls.Cameras.ArcBallCamera();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelBrowser));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.modelsListBox = new ps2ls.Forms.Controls.CustomListBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.modelsCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.searchModelsText = new System.Windows.Forms.ToolStripTextBox();
            this.clearSearchModelsText = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showAutoLODModelsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportSelectedModelsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ModelBrowserModelStats1 = new ps2ls.Forms.ModelBrowserModelStats();
            this.glControl1 = new ps2ls.Forms.ModelBrowserGLControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.showAxesButton = new System.Windows.Forms.ToolStripButton();
            this.showBoundingBoxButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.renderModeWireframeButton = new System.Windows.Forms.ToolStripButton();
            this.renderModeSmoothButton = new System.Windows.Forms.ToolStripButton();
            this.materialSelectionComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.searchModelsTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.modelsListBox);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ModelBrowserModelStats1);
            this.splitContainer1.Panel2.Controls.Add(this.glControl1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // modelsListBox
            // 
            this.modelsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelsListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.modelsListBox.FormattingEnabled = true;
            this.modelsListBox.Image = global::ps2ls.Properties.Resources.tree_small;
            this.modelsListBox.Items.AddRange(new object[] {
            "CustomListBox"});
            this.modelsListBox.Location = new System.Drawing.Point(0, 25);
            this.modelsListBox.Name = "modelsListBox";
            this.modelsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.modelsListBox.Size = new System.Drawing.Size(274, 553);
            this.modelsListBox.TabIndex = 3;
            this.modelsListBox.SelectedIndexChanged += new System.EventHandler(this.modelsListBox_SelectedIndexChanged);
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
            this.searchModelsText,
            this.clearSearchModelsText,
            this.toolStripSeparator2,
            this.showAutoLODModelsButton,
            this.toolStripSeparator1,
            this.exportSelectedModelsToolStripButton});
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
            // searchModelsText
            // 
            this.searchModelsText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchModelsText.Name = "searchModelsText";
            this.searchModelsText.Size = new System.Drawing.Size(100, 25);
            this.searchModelsText.TextChanged += new System.EventHandler(this.searchModelsText_TextChanged);
            // 
            // clearSearchModelsText
            // 
            this.clearSearchModelsText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearSearchModelsText.Enabled = false;
            this.clearSearchModelsText.Image = global::ps2ls.Properties.Resources.ui_text_field_clear_button;
            this.clearSearchModelsText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearSearchModelsText.Name = "clearSearchModelsText";
            this.clearSearchModelsText.Size = new System.Drawing.Size(23, 22);
            this.clearSearchModelsText.Text = "Clear Search Text";
            this.clearSearchModelsText.Click += new System.EventHandler(this.clearSearchModelsText_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // showAutoLODModelsButton
            // 
            this.showAutoLODModelsButton.CheckOnClick = true;
            this.showAutoLODModelsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showAutoLODModelsButton.Image = global::ps2ls.Properties.Resources.robot;
            this.showAutoLODModelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showAutoLODModelsButton.Name = "showAutoLODModelsButton";
            this.showAutoLODModelsButton.Size = new System.Drawing.Size(23, 22);
            this.showAutoLODModelsButton.Text = "Show Auto LOD Models";
            this.showAutoLODModelsButton.CheckedChanged += new System.EventHandler(this.showAutoLODModelsButton_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // exportSelectedModelsToolStripButton
            // 
            this.exportSelectedModelsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportSelectedModelsToolStripButton.Enabled = false;
            this.exportSelectedModelsToolStripButton.Image = global::ps2ls.Properties.Resources.drive_download;
            this.exportSelectedModelsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportSelectedModelsToolStripButton.Name = "exportSelectedModelsToolStripButton";
            this.exportSelectedModelsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.exportSelectedModelsToolStripButton.Text = "Export Selected Models...";
            this.exportSelectedModelsToolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // ModelBrowserModelStats1
            // 
            this.ModelBrowserModelStats1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ModelBrowserModelStats1.Location = new System.Drawing.Point(0, 439);
            this.ModelBrowserModelStats1.Model = null;
            this.ModelBrowserModelStats1.Name = "ModelBrowserModelStats1";
            this.ModelBrowserModelStats1.Size = new System.Drawing.Size(522, 161);
            this.ModelBrowserModelStats1.TabIndex = 3;
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            arcBallCamera1.AspectRatio = 0F;
            arcBallCamera1.DesiredDistance = 10F;
            arcBallCamera1.DesiredPitch = 0.7853982F;
            arcBallCamera1.DesiredTarget = ((OpenTK.Vector3)(resources.GetObject("arcBallCamera1.DesiredTarget")));
            arcBallCamera1.DesiredYaw = -0.7853982F;
            arcBallCamera1.FarPlaneDistance = 65536F;
            arcBallCamera1.FieldOfView = 1.291544F;
            arcBallCamera1.NearPlaneDistance = 0.00390625F;
            arcBallCamera1.Pitch = 0.7853982F;
            arcBallCamera1.Position = ((OpenTK.Vector3)(resources.GetObject("arcBallCamera1.Position")));
            arcBallCamera1.Yaw = -0.7853982F;
            this.glControl1.Camera = arcBallCamera1;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(0, 25);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(522, 575);
            this.glControl1.TabIndex = 1;
            this.glControl1.VSync = false;
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.MouseEnter += new System.EventHandler(this.glControl1_MouseEnter);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAxesButton,
            this.showBoundingBoxButton,
            this.toolStripSeparator3,
            this.renderModeWireframeButton,
            this.renderModeSmoothButton,
            this.materialSelectionComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(522, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // showAxesButton
            // 
            this.showAxesButton.Checked = true;
            this.showAxesButton.CheckOnClick = true;
            this.showAxesButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAxesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showAxesButton.Image = global::ps2ls.Properties.Resources.axes;
            this.showAxesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showAxesButton.Name = "showAxesButton";
            this.showAxesButton.Size = new System.Drawing.Size(23, 22);
            this.showAxesButton.Text = "Show Axes";
            this.showAxesButton.Click += new System.EventHandler(this.showAxesButton_Click);
            // 
            // showBoundingBoxButton
            // 
            this.showBoundingBoxButton.CheckOnClick = true;
            this.showBoundingBoxButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showBoundingBoxButton.Enabled = false;
            this.showBoundingBoxButton.Image = global::ps2ls.Properties.Resources.sphere_aabb2;
            this.showBoundingBoxButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showBoundingBoxButton.Name = "showBoundingBoxButton";
            this.showBoundingBoxButton.Size = new System.Drawing.Size(23, 22);
            this.showBoundingBoxButton.Text = "Show Bounding Box";
            this.showBoundingBoxButton.Click += new System.EventHandler(this.showBoundingBoxButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // renderModeWireframeButton
            // 
            this.renderModeWireframeButton.CheckOnClick = true;
            this.renderModeWireframeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renderModeWireframeButton.Image = global::ps2ls.Properties.Resources.wireframe;
            this.renderModeWireframeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renderModeWireframeButton.Name = "renderModeWireframeButton";
            this.renderModeWireframeButton.Size = new System.Drawing.Size(23, 22);
            this.renderModeWireframeButton.Text = "Show Wireframe";
            this.renderModeWireframeButton.ToolTipText = "Wireframe (F5)";
            this.renderModeWireframeButton.CheckedChanged += new System.EventHandler(this.renderModeWireframeButton_CheckedChanged);
            // 
            // renderModeSmoothButton
            // 
            this.renderModeSmoothButton.Checked = true;
            this.renderModeSmoothButton.CheckOnClick = true;
            this.renderModeSmoothButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.renderModeSmoothButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renderModeSmoothButton.Image = global::ps2ls.Properties.Resources.smooth;
            this.renderModeSmoothButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renderModeSmoothButton.Name = "renderModeSmoothButton";
            this.renderModeSmoothButton.Size = new System.Drawing.Size(23, 22);
            this.renderModeSmoothButton.Text = "Smooth";
            this.renderModeSmoothButton.ToolTipText = "Smooth (F6)";
            this.renderModeSmoothButton.CheckedChanged += new System.EventHandler(this.renderModeSmoothButton_CheckedChanged);
            // 
            // materialSelectionComboBox
            // 
            this.materialSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialSelectionComboBox.Name = "materialSelectionComboBox";
            this.materialSelectionComboBox.Size = new System.Drawing.Size(400, 25);
            this.materialSelectionComboBox.Sorted = true;
            this.materialSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.materialSelectionComboBox_Changed);
            // 
            // searchModelsTimer
            // 
            this.searchModelsTimer.Interval = 500;
            this.searchModelsTimer.Tick += new System.EventHandler(this.searchModelsTimer_Tick);
            // 
            // ModelBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ModelBrowser";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ModelBrowserControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox searchModelsText;
        private System.Windows.Forms.ToolStripButton clearSearchModelsText;
        private System.Windows.Forms.Timer searchModelsTimer;
        private ps2ls.Forms.Controls.CustomListBox modelsListBox;
        private System.Windows.Forms.ToolStripStatusLabel modelsCountToolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton exportSelectedModelsToolStripButton;
        private ModelBrowserGLControl glControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton renderModeWireframeButton;
        private System.Windows.Forms.ToolStripButton renderModeSmoothButton;
        private System.Windows.Forms.ToolStripButton showAxesButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton showBoundingBoxButton;
        private ModelBrowserModelStats ModelBrowserModelStats1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton showAutoLODModelsButton;
        private System.Windows.Forms.ToolStripComboBox materialSelectionComboBox;
    }
}
