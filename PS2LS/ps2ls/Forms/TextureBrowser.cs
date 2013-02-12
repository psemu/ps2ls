using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using ps2ls.Cameras;
using ps2ls.Files.Dme;
using ps2ls.Files.Pack;
using System.Diagnostics;

namespace ps2ls.Forms
{
    public partial class TextureBrowser : UserControl
    {
        #region Singleton
        private static TextureBrowser instance = null;

        public static void CreateInstance()
        {
            instance = new TextureBrowser();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static TextureBrowser Instance { get { return instance; } }
        #endregion

        private Model model = null;

        private TextureBrowser()
        {
            InitializeComponent();

            //HACK: Can't load TextureBrowser.cs in design mode unless we have at least one item for some reason.
            //Clear items after construction.
            texturesListBox.Items.Clear();

            Dock = DockStyle.Fill;
        }

        private void TextureBrowserControl_Load(object sender, EventArgs e)
        {
            glControl1.CreateGraphics();

            Application.Idle += applicationIdle;
        }

        private void applicationIdle(object sender, EventArgs e)
        {
            while (glControl1.Context != null && glControl1.IsIdle)
            {
                render();
            }
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            OpenTK.GLControl glControl = sender as OpenTK.GLControl;

            if (glControl.Height == 0)
            {
                glControl.ClientSize = new System.Drawing.Size(glControl.ClientSize.Width, 1);
            }

            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            render();
        }

        private void render()
        {
            glControl1.MakeCurrent();

            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            glControl1.SwapBuffers();
        }

        public override void Refresh()
        {
            base.Refresh();

            refreshTexturesListBox();
        }

        private void refreshTexturesListBox()
        {
            texturesListBox.Items.Clear();

            List<PackFile> files = new List<PackFile>();
            List<PackFile> ddsFiles = null;

            PackBrowser.Instance.PacksByType.TryGetValue(PackFile.Types.DDS, out ddsFiles);

            if (ddsFiles != null)
            {
                files.AddRange(ddsFiles);
            }

            files.Sort(new PackFile.NameComparer());

            if (files != null)
            {
                foreach (PackFile packFile in files)
                {
                    if (packFile.Name.IndexOf(searchTexturesText.Text, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        texturesListBox.Items.Add(packFile);
                    }
                }
            }

            Int32 count = texturesListBox.Items.Count;
            Int32 max = files != null ? files.Count : 0;

            modelsCountToolStripStatusLabel.Text = count + "/" + max;
        }

        private void searchModelsText_TextChanged(object sender, EventArgs e)
        {
            searchTexturesTimer.Stop();
            searchTexturesTimer.Start();
        }

        private void searchModelsTimer_Tick(object sender, EventArgs e)
        {
            if (searchTexturesText.Text.Length > 0)
            {
                searchTexturesText.BackColor = Color.Yellow;
                clearSearchTexturesText.Enabled = true;
            }
            else
            {
                searchTexturesText.BackColor = Color.White;
                clearSearchTexturesText.Enabled = false;
            }

            searchTexturesTimer.Stop();

            refreshTexturesListBox();
        }

        private void clearSearchModelsText_Click(object sender, EventArgs e)
        {
            searchTexturesText.Clear();
        }

        private void modelsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        private void snapCameraToModel()
        {
        }

        private void showAxesButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void showWireframeButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void showAABBButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void renderModeWireframeButton_Click(object sender, EventArgs e)
        {
        }

        private void showBoundingBoxButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void renderModeWireframeButton_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void renderModeSmoothButton_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void glControl1_MouseEnter(object sender, EventArgs e)
        {
        }

        private void showAutoLODModelsButton_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
