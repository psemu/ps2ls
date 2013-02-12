using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ps2ls
{
    public partial class GenericLoadingForm : Form
    {
        public GenericLoadingForm()
        {
            InitializeComponent();
        }

        public void SetWindowTitle(String title)
        {
            Text = title;
        }

        public void SetProgressBarPercent(Int32 percent)
        {
            progressBar1.Value = percent;
        }

        public void SetLabelText(String text)
        {
            label1.Text = text;
        }
    }
}
