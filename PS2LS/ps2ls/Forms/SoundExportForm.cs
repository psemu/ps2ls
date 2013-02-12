using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ps2ls.Assets.Pack;

namespace ps2ls.Forms
{
    public partial class SoundExportForm : Form
    {
        public SoundExportForm()
        {
            InitializeComponent();
        }

        public List<String> FileNames { get; set; }

        private GenericLoadingForm loadingForm;
        private BackgroundWorker exportBackgroundWorker = new BackgroundWorker();

        private FMOD.FILE_OPENCALLBACK myopen = new FMOD.FILE_OPENCALLBACK(OPENCALLBACK);
        private FMOD.FILE_CLOSECALLBACK myclose = new FMOD.FILE_CLOSECALLBACK(CLOSECALLBACK);
        private FMOD.FILE_READCALLBACK myread = new FMOD.FILE_READCALLBACK(READCALLBACK);
        private FMOD.FILE_SEEKCALLBACK myseek = new FMOD.FILE_SEEKCALLBACK(SEEKCALLBACK);

        static MemoryStream ms;
        private static FMOD.RESULT OPENCALLBACK([MarshalAs(UnmanagedType.LPWStr)]string name, int unicode, ref uint filesize, ref IntPtr handle, ref IntPtr userdata)
        {
            ms = AssetManager.Instance.CreateAssetMemoryStreamByName(name);
            ms = Utils.FixSoundHeader(ms);
            filesize = (uint)ms.Length;

            return FMOD.RESULT.OK;
        }

        private static FMOD.RESULT CLOSECALLBACK(IntPtr handle, IntPtr userdata)
        {
            ms.Close();

            return FMOD.RESULT.OK;
        }

        private static FMOD.RESULT READCALLBACK(IntPtr handle, IntPtr buffer, uint sizebytes, ref uint bytesread, IntPtr userdata)
        {
            byte[] readbuffer = new byte[sizebytes];

            bytesread = (uint)ms.Read(readbuffer, 0, (int)sizebytes);
            if (bytesread == 0)
            {
                return FMOD.RESULT.ERR_FILE_EOF;
            }

            Marshal.Copy(readbuffer, 0, buffer, (int)sizebytes);

            return FMOD.RESULT.OK;
        }

        private static FMOD.RESULT SEEKCALLBACK(IntPtr handle, int pos, IntPtr userdata)
        {
            ms.Seek(pos, SeekOrigin.Begin);
            return FMOD.RESULT.OK;
        }


        FMOD.System system = null;
        FMOD.Sound fsb = null;
        private void initFmod()
        {
            FMOD.RESULT res;

            res = FMOD.Factory.System_Create(ref system);

            system.setOutput(FMOD.OUTPUTTYPE.WAVWRITER);

            system.init(32, FMOD.INITFLAGS.STREAM_FROM_UPDATE, (IntPtr)null);

            system.setFileSystem(myopen, myclose, myread, myseek, null, null, 2048);

         



        }

        private void loadSound(string name)
        {
            FMOD.RESULT res = system.createSound(name, (FMOD.MODE._2D | FMOD.MODE.HARDWARE | FMOD.MODE.CREATESTREAM), ref fsb);

            if (res != FMOD.RESULT.OK)
            {
                MessageBox.Show("Cannot load file.  Reason: " + res.ToString(), "FMOD Load Error", MessageBoxButtons.OK);
            }

        }


        private int DoExportSound(object sender, object arg)
        {
            List<object> arguments = (List<object>)arg;

            String directory = (String)arguments[0];
            List<String> fileNames = (List<String>)arguments[1];
            string wav = (string)arguments[2];

            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;

            initFmod();

            Int32 result = 0;





            return result;
        }




       
    }
}
