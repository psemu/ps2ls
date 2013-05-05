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
    public partial class ActorForm : UserControl
    {
        #region Singleton
        private static ActorForm instance = null;

        public static void CreateInstance()
        {
            instance = new ActorForm();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static ActorForm Instance { get { return instance; } }
        #endregion

        public ActorForm()
        {
            InitializeComponent();



        }

        public void PopulateList()
        {
            customListBox1.PopulateBox("");

         

        }

        private void ActorForm_Enter(object sender, EventArgs e)
        {
            PopulateList();
        }
    }
}
