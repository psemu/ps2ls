namespace DevILNet.Sample {
    partial class ImageViewer {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewer));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.filterToolStrip = new System.Windows.Forms.ToolStrip();
            this.alienifyButton = new System.Windows.Forms.ToolStripButton();
            this.embossButton = new System.Windows.Forms.ToolStripButton();
            this.negativeButton = new System.Windows.Forms.ToolStripButton();
            this.equalizeButton = new System.Windows.Forms.ToolStripButton();
            this.blurDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.blurAverageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blurGaussianMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeDetectDropdownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.edgeDetectEMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeDetectSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeDetectPMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageRotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate180 = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate90CW = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate90CCW = new System.Windows.Forms.ToolStripMenuItem();
            this.resetImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mirrorYAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipXAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.filterToolStrip.SuspendLayout();
            this.appMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(624, 393);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // filterToolStrip
            // 
            this.filterToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.filterToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alienifyButton,
            this.embossButton,
            this.negativeButton,
            this.equalizeButton,
            this.blurDropDownButton,
            this.edgeDetectDropdownButton});
            this.filterToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.filterToolStrip.Location = new System.Drawing.Point(0, 24);
            this.filterToolStrip.MinimumSize = new System.Drawing.Size(484, 25);
            this.filterToolStrip.Name = "filterToolStrip";
            this.filterToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.filterToolStrip.Size = new System.Drawing.Size(624, 25);
            this.filterToolStrip.TabIndex = 2;
            this.filterToolStrip.Text = "Filter Tool Bar";
            // 
            // alienifyButton
            // 
            this.alienifyButton.Image = ((System.Drawing.Image)(resources.GetObject("alienifyButton.Image")));
            this.alienifyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alienifyButton.Name = "alienifyButton";
            this.alienifyButton.Size = new System.Drawing.Size(67, 22);
            this.alienifyButton.Text = "Alienify";
            this.alienifyButton.Click += new System.EventHandler(this.AlienifyImage);
            // 
            // embossButton
            // 
            this.embossButton.Image = ((System.Drawing.Image)(resources.GetObject("embossButton.Image")));
            this.embossButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.embossButton.Name = "embossButton";
            this.embossButton.Size = new System.Drawing.Size(68, 22);
            this.embossButton.Text = "Emboss";
            this.embossButton.Click += new System.EventHandler(this.EmbossImage);
            // 
            // negativeButton
            // 
            this.negativeButton.Image = ((System.Drawing.Image)(resources.GetObject("negativeButton.Image")));
            this.negativeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.negativeButton.Name = "negativeButton";
            this.negativeButton.Size = new System.Drawing.Size(74, 22);
            this.negativeButton.Text = "Negative";
            this.negativeButton.Click += new System.EventHandler(this.NegativeImage);
            // 
            // equalizeButton
            // 
            this.equalizeButton.Image = ((System.Drawing.Image)(resources.GetObject("equalizeButton.Image")));
            this.equalizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.equalizeButton.Name = "equalizeButton";
            this.equalizeButton.Size = new System.Drawing.Size(70, 22);
            this.equalizeButton.Text = "Equalize";
            this.equalizeButton.Click += new System.EventHandler(this.EqualizeImage);
            // 
            // blurDropDownButton
            // 
            this.blurDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blurAverageMenuItem,
            this.blurGaussianMenuItem});
            this.blurDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("blurDropDownButton.Image")));
            this.blurDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.blurDropDownButton.Name = "blurDropDownButton";
            this.blurDropDownButton.Size = new System.Drawing.Size(57, 22);
            this.blurDropDownButton.Text = "Blur";
            // 
            // blurAverageMenuItem
            // 
            this.blurAverageMenuItem.Name = "blurAverageMenuItem";
            this.blurAverageMenuItem.Size = new System.Drawing.Size(152, 22);
            this.blurAverageMenuItem.Text = "Blur Average";
            this.blurAverageMenuItem.Click += new System.EventHandler(this.BlurAverageImage);
            // 
            // blurGaussianMenuItem
            // 
            this.blurGaussianMenuItem.Name = "blurGaussianMenuItem";
            this.blurGaussianMenuItem.Size = new System.Drawing.Size(152, 22);
            this.blurGaussianMenuItem.Text = "Blur Gaussian";
            this.blurGaussianMenuItem.Click += new System.EventHandler(this.BlurGaussianImage);
            // 
            // edgeDetectDropdownButton
            // 
            this.edgeDetectDropdownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.edgeDetectEMenuItem,
            this.edgeDetectSMenuItem,
            this.edgeDetectPMenuItem});
            this.edgeDetectDropdownButton.Image = ((System.Drawing.Image)(resources.GetObject("edgeDetectDropdownButton.Image")));
            this.edgeDetectDropdownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.edgeDetectDropdownButton.Name = "edgeDetectDropdownButton";
            this.edgeDetectDropdownButton.Size = new System.Drawing.Size(99, 22);
            this.edgeDetectDropdownButton.Text = "Edge Detect";
            // 
            // edgeDetectEMenuItem
            // 
            this.edgeDetectEMenuItem.Name = "edgeDetectEMenuItem";
            this.edgeDetectEMenuItem.Size = new System.Drawing.Size(157, 22);
            this.edgeDetectEMenuItem.Text = "Edge Detect \"E\"";
            this.edgeDetectEMenuItem.Click += new System.EventHandler(this.EdgeDetectEImage);
            // 
            // edgeDetectSMenuItem
            // 
            this.edgeDetectSMenuItem.Name = "edgeDetectSMenuItem";
            this.edgeDetectSMenuItem.Size = new System.Drawing.Size(157, 22);
            this.edgeDetectSMenuItem.Text = "Edge Detect \"S\"";
            this.edgeDetectSMenuItem.Click += new System.EventHandler(this.EdgeDetectSImage);
            // 
            // edgeDetectPMenuItem
            // 
            this.edgeDetectPMenuItem.Name = "edgeDetectPMenuItem";
            this.edgeDetectPMenuItem.Size = new System.Drawing.Size(157, 22);
            this.edgeDetectPMenuItem.Text = "Edge Detect \"P\"";
            this.edgeDetectPMenuItem.Click += new System.EventHandler(this.EdgeDetectPImage);
            // 
            // appMenuStrip
            // 
            this.appMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imageToolStripMenuItem1});
            this.appMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.appMenuStrip.Name = "appMenuStrip";
            this.appMenuStrip.Size = new System.Drawing.Size(624, 24);
            this.appMenuStrip.TabIndex = 3;
            this.appMenuStrip.Text = "App Menu Strip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenFileDialog);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveImage);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsFileDialog);
            // 
            // imageToolStripMenuItem1
            // 
            this.imageToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageRotationToolStripMenuItem,
            this.mirrorYAxisToolStripMenuItem,
            this.flipXAxisToolStripMenuItem,
            this.resetImageToolStripMenuItem});
            this.imageToolStripMenuItem1.Name = "imageToolStripMenuItem1";
            this.imageToolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.imageToolStripMenuItem1.Text = "Image";
            // 
            // imageRotationToolStripMenuItem
            // 
            this.imageRotationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotate180,
            this.rotate90CW,
            this.rotate90CCW});
            this.imageRotationToolStripMenuItem.Name = "imageRotationToolStripMenuItem";
            this.imageRotationToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.imageRotationToolStripMenuItem.Text = "Image Rotation";
            // 
            // rotate180
            // 
            this.rotate180.Name = "rotate180";
            this.rotate180.Size = new System.Drawing.Size(152, 22);
            this.rotate180.Text = "180°";
            this.rotate180.Click += new System.EventHandler(this.RotateImage180);
            // 
            // rotate90CW
            // 
            this.rotate90CW.Name = "rotate90CW";
            this.rotate90CW.Size = new System.Drawing.Size(152, 22);
            this.rotate90CW.Text = "90° CW";
            this.rotate90CW.Click += new System.EventHandler(this.RotateImage90CW);
            // 
            // rotate90CCW
            // 
            this.rotate90CCW.Name = "rotate90CCW";
            this.rotate90CCW.Size = new System.Drawing.Size(152, 22);
            this.rotate90CCW.Text = "90° CCW";
            this.rotate90CCW.Click += new System.EventHandler(this.RotateImage90CCW);
            // 
            // resetImageToolStripMenuItem
            // 
            this.resetImageToolStripMenuItem.Name = "resetImageToolStripMenuItem";
            this.resetImageToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.resetImageToolStripMenuItem.Text = "Reset Image";
            this.resetImageToolStripMenuItem.Click += new System.EventHandler(this.ResetImage);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 393);
            this.panel1.TabIndex = 4;
            // 
            // mirrorYAxisToolStripMenuItem
            // 
            this.mirrorYAxisToolStripMenuItem.Name = "mirrorYAxisToolStripMenuItem";
            this.mirrorYAxisToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.mirrorYAxisToolStripMenuItem.Text = "Mirror (Y-Axis)";
            this.mirrorYAxisToolStripMenuItem.Click += new System.EventHandler(this.MirrorImage);
            // 
            // flipXAxisToolStripMenuItem
            // 
            this.flipXAxisToolStripMenuItem.Name = "flipXAxisToolStripMenuItem";
            this.flipXAxisToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.flipXAxisToolStripMenuItem.Text = "Flip (X-Axis)";
            this.flipXAxisToolStripMenuItem.Click += new System.EventHandler(this.FlipImage);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.filterToolStrip);
            this.Controls.Add(this.appMenuStrip);
            this.MainMenuStrip = this.appMenuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ClientSizeChanged += new System.EventHandler(this.OnResize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.filterToolStrip.ResumeLayout(false);
            this.filterToolStrip.PerformLayout();
            this.appMenuStrip.ResumeLayout(false);
            this.appMenuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStrip filterToolStrip;
        private System.Windows.Forms.ToolStripButton alienifyButton;
        private System.Windows.Forms.ToolStripButton embossButton;
        private System.Windows.Forms.MenuStrip appMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton edgeDetectDropdownButton;
        private System.Windows.Forms.ToolStripMenuItem edgeDetectEMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeDetectSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeDetectPMenuItem;
        private System.Windows.Forms.ToolStripButton equalizeButton;
        private System.Windows.Forms.ToolStripButton negativeButton;
        private System.Windows.Forms.ToolStripDropDownButton blurDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem blurAverageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blurGaussianMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem imageRotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotate180;
        private System.Windows.Forms.ToolStripMenuItem rotate90CW;
        private System.Windows.Forms.ToolStripMenuItem rotate90CCW;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem resetImageToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem mirrorYAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipXAxisToolStripMenuItem;
    }
}

