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
using ps2ls.Assets.Dma;
using ps2ls.Assets.Pack;
using System.Diagnostics;
using System.IO;

namespace ps2ls.Forms
{
    public partial class MaterialBrowser : UserControl
    {
        #region Singleton
        private static MaterialBrowser instance = null;

        public static void CreateInstance()
        {
            instance = new MaterialBrowser();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static MaterialBrowser Instance { get { return instance; } }
        #endregion

        private MaterialBrowser()
        {
            InitializeComponent();

            //HACK: Can't load MaterialBrowser.cs in design mode unless we have at least one item for some reason.
            //Clear items after construction.
            texturesListBox.Items.Clear();

            Dock = DockStyle.Fill;
        }

        private void MaterialBrowserControl_Load(object sender, EventArgs e)
        {
            glControl1.CreateGraphics();

            Application.Idle += applicationIdle;
        }

        private void applicationIdle(object sender, EventArgs e)
        {
            while (glControl1.Context != null && glControl1.IsIdle)
            {
                update();
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
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            update();
            render();
        }

        private void update()
        {
            glControl1.Camera.AspectRatio = (Single)glControl1.ClientSize.Width / (Single)glControl1.ClientSize.Height;
            glControl1.Camera.Update();
        }

        private void render()
        {
            glControl1.MakeCurrent();

            GL.Viewport(0, 0, glControl1.ClientSize.Width, glControl1.ClientSize.Height);

            GL.ClearColor(Color.Wheat);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //projection matrix
            Matrix4 projection = glControl1.Camera.Projection;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            //view matrix
            Matrix4 view = glControl1.Camera.View;
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);

            GL.UseProgram(0);

            //if (dma != null && dma.te.Count > 1)
            //{
            //    Vector2[] vertices = new Vector2[]
            //    {
            //        new Vector2(-0.5f, -0.5f),
            //        new Vector2( 0.5f, -0.5f),
            //        new Vector2( 0.5f,  0.5f),
            //        new Vector2(-0.5f,  0.5f)
            //    };

            //    Vector2[] texCoords0 = new Vector2[]
            //    {
            //        new Vector2(0.0f, 0.0f),
            //        new Vector2(1.0f, 0.0f),
            //        new Vector2(1.0f, 1.0f),
            //        new Vector2(0.0f, 1.0f)
            //    };

            //    ushort[] indices = new ushort[]
            //    {
            //        0, 1, 2, 0, 2, 3
            //    };

            //    GL.PushAttrib(AttribMask.EnableBit | AttribMask.CurrentBit);

            //    GL.Disable(EnableCap.Lighting);

            //    GL.Color3(Color.White);

            //    GL.EnableClientState(ArrayCap.VertexArray);
            //    GL.EnableClientState(ArrayCap.TextureCoordArray);

            //    GL.VertexPointer(2, VertexPointerType.Float, 0, vertices);
            //    GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, texCoords0);

            //    GL.Enable(EnableCap.Texture2D);
            //    GL.BindTexture(TextureTarget.Texture2D, dma.Textures[1]);

            //    GL.DrawRangeElements(BeginMode.Triangles, 0, 5, 6, DrawElementsType.UnsignedShort, indices);

            //    GL.BindTexture(TextureTarget.Texture2D, 0);

            //    GL.DisableClientState(ArrayCap.TextureCoordArray);
            //    GL.DisableClientState(ArrayCap.VertexArray);

            //    GL.PopAttrib();
            //}

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

            List<Asset> assets = new List<Asset>();
            List<Asset> dmas = null;

            AssetManager.Instance.AssetsByType.TryGetValue(Asset.Types.DMA, out dmas);

            if (dmas != null)
            {
                assets.AddRange(dmas);
            }

            assets.Sort(new Asset.NameComparer());

            if (assets != null)
            {
                foreach (Asset asset in assets)
                {
                    if (asset.Name.IndexOf(searchTexturesText.Text, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        texturesListBox.Items.Add(asset);
                    }
                }
            }

            Int32 count = texturesListBox.Items.Count;
            Int32 max = assets != null ? assets.Count : 0;

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

        private void showBoundingBoxButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void glControl1_MouseEnter(object sender, EventArgs e)
        {
        }
    }
}
