using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ps2ls.Assets.Pack;

namespace ps2ls.Forms.Controls
{
    public class CustomListBox : ListBox
    {
        public Image Image { get; set; }

        public Asset.Types AssetType
        {
            get;
            set;
        }

        public int MaxCount { get; protected set; }
        public CustomListBox()
        {
            this.DrawItem += new DrawItemEventHandler(this.CustomListBox_DrawItem);

            DrawMode = DrawMode.OwnerDrawFixed;
        }

        private void CustomListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            e.DrawBackground();

            String text = ((ListBox)sender).Items[e.Index].ToString();
            Point point = new Point(0, e.Bounds.Y);

            if (Image != null)
            {
                e.Graphics.DrawImage(Image, point);
                point.X += Image.Width;
            }

            e.Graphics.DrawString(text, e.Font, new SolidBrush(Color.Black), point);
            e.DrawFocusRectangle();
        }

        public void PopulateBox(string searchText)
        {
            this.Items.Clear();

            List<Asset> assets = new List<Asset>();
            List<Asset> images = null;

            AssetManager.Instance.AssetsByType.TryGetValue(AssetType, out images);

            if (images != null)
            {
                assets.AddRange(images);
            }

            assets.Sort(new Asset.NameComparer());

            if (assets != null)
            {
                foreach (Asset asset in assets)
                {
                    if (asset.Name.IndexOf(searchText, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        this.Items.Add(asset);
                    }

                }
            }
            MaxCount = assets == null ? assets.Count : 0;
        }
    }
}
