using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ps2ls.Forms.Controls
{
    public class CustomListBox : ListBox
    {
        public Image Image { get; set; }

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
    }
}
