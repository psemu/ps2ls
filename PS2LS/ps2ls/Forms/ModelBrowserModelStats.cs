using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.Assets.Dme;

namespace ps2ls.Forms
{
    public partial class ModelBrowserModelStats : UserControl
    {
        private Model model;

        public ModelBrowserModelStats()
        {
            InitializeComponent();
        }

        public Model Model
        {
            get { return model; }
            set
            {
                model = value;

                nameLabel.Text = model != null ? model.Name : "";
                meshCountLabel.Text = model != null ? model.Meshes.Length.ToString() : "0";
                modelVertexCountLabel.Text = model != null ? model.VertexCount.ToString() : "0";
                modelTriangleCountLabel.Text = model != null ? (model.IndexCount / 3).ToString() : "0";
                materialCount.Text = model != null ? model.Materials.Count.ToString() : "0";
                modelUnknown0Label.Text = model != null ? model.Unknown0.ToString() : "0";
                modelUnknown1Label.Text = model != null ? model.Unknown1.ToString() : "0";
                mdoelUnknown2Label.Text = model != null ? model.Unknown2.ToString() : "0";
                modelVersionLabel.Text = model != null ? model.Version.ToString() : "0";

                meshesComboBox.Items.Clear();
                textureComboBox1.Items.Clear();
                texturesComboBox2.Items.Clear();

                texturesComboBox2.Visible = false;
                label18.Visible = false;

                if (model != null)
                {
                    for (Int32 i = 0; i < model.Meshes.Length; ++i)
                    {
                        meshesComboBox.Items.Add("Mesh " + i);
                        textureComboBox1.Items.Add("" + i);
                    }

                    for (int i = 0; i < model.TextureStrings.Count; i++)
                    {
                        texturesComboBox2.Items.Add(model.TextureStrings[i]);
                    }
                }

                if (meshesComboBox.Items.Count > 0)
                {
                    meshesComboBox.SelectedIndex = 0;
                }


            }
        }

        private void meshesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mesh mesh = null;

            if (model != null && meshesComboBox.SelectedIndex >= 0)
            {
                mesh = model.Meshes[meshesComboBox.SelectedIndex];
            }

            meshVertexCountLabel.Text = mesh != null ? mesh.VertexCount.ToString() : "0";
            meshTriangleCountLabel.Text = mesh != null ? (mesh.IndexCount / 3).ToString() : "0";
            //meshBytesPerVertexLabel.Text = mesh != null ? mesh.BytesPerVertex.ToString() : "0";
            meshIndexLabel.Text = mesh != null ? mesh.MaterialIndex.ToString() : "0";
            meshUnknown1Label.Text = mesh != null ? mesh.Unknown1.ToString() : "0";
            meshUnknown2Label.Text = mesh != null ? mesh.Unknown2.ToString() : "0";
            meshUnknown3Label.Text = mesh != null ? mesh.Unknown3.ToString() : "0";
            meshUnknown4Label.Text = mesh != null ? mesh.Unknown4.ToString() : "0";
            meshVertexBlockCountLabel.Text = mesh != null ? mesh.VertexStreams.Length.ToString() : "0";
        }

        private void textureComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            texturesComboBox2.Visible = true;
            texturesComboBox2.SelectedItem = null;
            label18.Visible = true;
        }

        private void texturesComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelBrowser.Instance.SetTextureForMesh(int.Parse((string)textureComboBox1.SelectedItem), (string)texturesComboBox2.SelectedItem);
        }
    }
}
