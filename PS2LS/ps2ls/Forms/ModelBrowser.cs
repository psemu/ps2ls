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
using ps2ls.Assets.Dme;
using ps2ls.Assets.Pack;
using System.Diagnostics;
using ps2ls.Graphics.Materials;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;

namespace ps2ls.Forms
{
    public partial class ModelBrowser : UserControl
    {
        #region Singleton
        private static ModelBrowser instance = null;

        public static void CreateInstance()
        {
            instance = new ModelBrowser();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static ModelBrowser Instance { get { return instance; } }
        #endregion
        private ColorDialog backgroundColorDialog = new ColorDialog();

      
        private List<ToolStripButton> renderModeButtons = new List<ToolStripButton>();

      

   
        private ModelBrowser()
        {
            InitializeComponent();

            //HACK: Can't load ModelBrowser.cs in design mode unless we have at least one item for some reason.
            //Clear items after construction.
            modelsListBox.Items.Clear();

            Dock = DockStyle.Fill;

            backgroundColorDialog.Color = Color.FromArgb(32, 32, 32);

            renderModeButtons.Add(renderModeWireframeButton);
            renderModeButtons.Add(renderModeSmoothButton);

           
        }

       
                  

       
        public override void Refresh()
        {
            base.Refresh();

            refreshModelsListBox();
        }

        private void refreshModelsListBox()
        {
            modelsListBox.Items.Clear();

            List<Asset> assets = new List<Asset>();
            List<Asset> dmes = null;

            AssetManager.Instance.AssetsByType.TryGetValue(Asset.Types.DME, out dmes);

            if (dmes != null)
            {
                assets.AddRange(dmes);
            }

            assets.Sort(new Asset.NameComparer());

            if (assets != null)
            {
                foreach (Asset asset in assets)
                {
                    if (showAutoLODModelsButton.Checked == false)
                    {
                        if (asset.Name.EndsWith("Auto.dme"))
                        {
                            continue;
                        }
                    }

                    if (asset.Name.IndexOf(searchModelsText.Text, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        modelsListBox.Items.Add(asset);
                    }
                }
            }

            Int32 count = modelsListBox.Items.Count;
            Int32 max = assets != null ? assets.Count : 0;

            modelsCountToolStripStatusLabel.Text = count + "/" + max;
        }

        private void searchModelsText_TextChanged(object sender, EventArgs e)
        {
            searchModelsTimer.Stop();
            searchModelsTimer.Start();
        }

        private void searchModelsTimer_Tick(object sender, EventArgs e)
        {
            if (searchModelsText.Text.Length > 0)
            {
                searchModelsText.BackColor = Color.Yellow;
                clearSearchModelsText.Enabled = true;
            }
            else
            {
                searchModelsText.BackColor = Color.White;
                clearSearchModelsText.Enabled = false;
            }

            searchModelsTimer.Stop();

            refreshModelsListBox();
        }

        private void clearSearchModelsText_Click(object sender, EventArgs e)
        {
            searchModelsText.Clear();
        }

      
        private void modelsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Asset asset = null;

            exportSelectedModelsToolStripButton.Enabled = modelsListBox.SelectedItems.Count > 0;

            try
            {
                asset = (Asset)modelsListBox.SelectedItem;
            }
            catch (InvalidCastException) { return; }

            System.IO.MemoryStream memoryStream = asset.Pack.CreateAssetMemoryStreamByName(asset.Name);

            Model model = Model.LoadFromStream(asset.Name, memoryStream);

            ModelBrowserModelStats1.Model = model;
            if (model.TextureStrings.Count != 0)
            {
                foreach (string textureName in model.TextureStrings)
                {
                    materialSelectionComboBox.Items.Add(textureName);
                }
            }
           
            materialSelectionComboBox.SelectedIndex = 0;
            materialSelectionComboBox.Items.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<String> fileNames = new List<string>();

            foreach (object selectedItem in modelsListBox.SelectedItems)
            {
                Asset asset = null;

                try
                {
                    asset = (Asset)selectedItem;
                }
                catch (InvalidCastException) { continue; }

                fileNames.Add(asset.Name);
            }

            ModelExportForm modelExportForm = new ModelExportForm();
            modelExportForm.FileNames = fileNames;
            modelExportForm.ShowDialog();
        }

       
        private void showAxesButton_Click(object sender, EventArgs e)
        {
            glControl1.ShowAxis = !glControl1.ShowAxis;
            glControl1.Invalidate();
        }

        private void showWireframeButton_Click(object sender, EventArgs e)
        {
            glControl1.Wireframe = !glControl1.Wireframe;
            glControl1.Invalidate();
        }

        private void showAABBButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void renderModeWireframeButton_Click(object sender, EventArgs e)
        {
            foreach (ToolStripButton button in renderModeButtons)
            {
                button.Checked = (sender == button);
            }
        }

        private void showBoundingBoxButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }


        private void renderModeWireframeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (renderModeWireframeButton.Checked)
            {
                foreach (ToolStripButton button in renderModeButtons)
                {
                    if (sender != button)
                    {
                        button.Checked = false;
                    }
                }
            }
        }

        private void renderModeSmoothButton_CheckedChanged(object sender, EventArgs e)
        {
            if (renderModeSmoothButton.Checked)
            {
                foreach (ToolStripButton button in renderModeButtons)
                {
                    if (sender != button)
                    {
                        button.Checked = false;
                    }
                }
            }
        }

       

        private void showAutoLODModelsButton_CheckedChanged(object sender, EventArgs e)
        {
            refreshModelsListBox();
        }
         private void materialSelectionComboBox_Changed(object sender, EventArgs e)
        {
          
        }

       
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            backgroundColorDialog.ShowDialog();
        }

        public void SetTextureForMesh(int meshID, string name)
        {
            glControl1.SetTextureForMesh(meshID, name);
        }
    }
}
