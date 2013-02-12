namespace ps2ls.Forms
{
    partial class ModelBrowserGLControl
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
            this.SuspendLayout();
            // 
            // ModelBrowserGLControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ModelBrowserGLControl";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModelBrowserGLControl_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ModelBrowserGLControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModelBrowserGLControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ModelBrowserGLControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
