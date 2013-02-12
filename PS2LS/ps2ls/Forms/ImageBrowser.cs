using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.Assets.Pack;

namespace ps2ls.Forms
{
    public partial class ImageBrowser : UserControl
    {

        #region Singleton
        private static ImageBrowser instance = null;

        public static void CreateInstance()
        {
            instance = new ImageBrowser();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static ImageBrowser Instance { get { return instance; } }
        #endregion

        public ImageBrowser()
        {
            InitializeComponent();

            imageListbox.Items.Clear();

            Dock = DockStyle.Fill;

        }


        private void refreshImageListBox()
        {
            imageListbox.Items.Clear();

            List<Asset> assets = new List<Asset>();
            List<Asset> images = null;

            AssetManager.Instance.AssetsByType.TryGetValue(Asset.Types.DDS, out images);

            if (images != null)
            {
                assets.AddRange(images);
            }

            assets.Sort(new Asset.NameComparer());

            if (assets != null)
            {
                foreach (Asset asset in assets)
                {
                    if (asset.Name.IndexOf(searchText.Text, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        imageListbox.Items.Add(asset);
                    }

                }
            }

            int count = imageListbox.Items.Count;
            int max = assets != null ? assets.Count : 0;

            imagesCountLabel.Text = count + "/" + max;

        }

        private void imageListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Asset asset = null;

            try
            {
                asset = (Asset)imageListbox.SelectedItem;
            }
            catch (InvalidCastException) { return; }

            System.IO.MemoryStream memoryStream = asset.Pack.CreateAssetMemoryStreamByName(asset.Name);

            Image i = TextureManager.LoadDrawingImageFromStream(memoryStream);

            pictureWindow.BackgroundImage = i;
            BackgroundImageLayout = ImageLayout.Stretch;
            pictureWindow.Show();
            


        }

       
         private void searchText_TextChanged(object sender, EventArgs e)
        {
            searchTextTimer.Stop();
            searchTextTimer.Start();
        }

      

         private void searchTextTimer_Tick(object sender, EventArgs e)
         {
             if (searchText.Text.Length > 0)
             {
                 searchText.BackColor = Color.Yellow;
                 toolStripButton2.Enabled = true;

             }
             else
             {
                 searchText.BackColor = Color.White;
                 toolStripButton2.Enabled = false;
             }

             searchTextTimer.Stop();
             refreshImageListBox();
         }

         private void toolStripButton2_Click(object sender, EventArgs e)
         {
             searchText.Clear();
         }

         private void pictureWindow_Click(object sender, EventArgs e)
         {

         }

         private void ImageBrowser_Load(object sender, EventArgs e)
         {
             searchTextTimer.Stop();
             searchTextTimer.Start();
         }


    }
}
