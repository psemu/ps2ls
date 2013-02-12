using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.Assets.Pack;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace ps2ls.Forms
{
    public partial class SoundBrowser : UserControl
    {

        #region Singleton
        private static SoundBrowser instance = null;

        public static void CreateInstance()
        {
            instance = new SoundBrowser();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static SoundBrowser Instance { get { return instance; } }
        #endregion

        public SoundBrowser()
        {
            InitializeComponent();

            soundListBox.Items.Clear();

            Dock = DockStyle.Fill;
        }
        FMOD.System system;
        FMOD.Sound fsb = null;

        FMOD.Channel channel = null;
        
           

      


        private FMOD.SOUND_PCMREADCALLBACK pcmreadcallback = new FMOD.SOUND_PCMREADCALLBACK(PCMREADCALLBACK);
        private FMOD.SOUND_PCMSETPOSCALLBACK pcmsetposcallback = new FMOD.SOUND_PCMSETPOSCALLBACK(PCMSETPOSCALLBACK);

        private static float t1 = 0, t2 = 0;        // time
        private static float v1 = 0, v2 = 0;        // velocity

        private static FMOD.RESULT PCMREADCALLBACK(IntPtr soundraw, IntPtr data, uint datalen)
        {
            unsafe
            {
                uint count;

                short* stereo16bitbuffer = (short*)data.ToPointer();

                for (count = 0; count < (datalen >> 2); count++)        // >>2 = 16bit stereo (4 bytes per sample)
                {
                    *stereo16bitbuffer++ = (short)(Math.Sin(t1) * 32767.0f);    // left channel
                    *stereo16bitbuffer++ = (short)(Math.Sin(t2) * 32767.0f);    // right channel

                    t1 += 0.01f + v1;
                    t2 += 0.0142f + v2;
                    v1 += (float)(Math.Sin(t1) * 0.002f);
                    v2 += (float)(Math.Sin(t2) * 0.002f);
                }
            }
            return FMOD.RESULT.OK;
        }

        private static FMOD.RESULT PCMSETPOSCALLBACK(IntPtr soundraw, int subsound, uint pcmoffset, FMOD.TIMEUNIT postype)
        {
            /*
                This is useful if the user calls Sound::setTime or Sound::setPosition and you want to seek your data accordingly.
            */

            return FMOD.RESULT.OK;
        }


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



        private void initFmod()
        {
            FMOD.RESULT res;

            res = FMOD.Factory.System_Create(ref system);

            system.init(32, FMOD.INITFLAGS.NORMAL, (IntPtr)null);

            system.setFileSystem(myopen, myclose, myread, myseek, null, null, 2048);

            system.setOutput(FMOD.OUTPUTTYPE.AUTODETECT);



        }

        private void loadSound(string name)
        {
            if (channel != null)
            {
                bool playing = false;
                channel.isPlaying(ref playing);
                if (playing)
                {
                    channel.stop();
                }
                subsound.release();
                fsb.release();
               
            }

           FMOD.RESULT res =  system.createSound(name, (FMOD.MODE._2D | FMOD.MODE.HARDWARE | FMOD.MODE.CREATESTREAM), ref fsb);
            
          
         
           if (res != FMOD.RESULT.OK)
           {
               MessageBox.Show("Cannot load file.  Reason: " + res.ToString(), "FMOD Load Error", MessageBoxButtons.OK);
           }

        }

        private void refreshListBox()
        {
            
            soundListBox.Items.Clear();

            List<Asset> assets = new List<Asset>();
            List<Asset> images = null;

            AssetManager.Instance.AssetsByType.TryGetValue(Asset.Types.FSB, out images);

            if (images != null)
            {
                assets.AddRange(images);
            }

            assets.Sort(new Asset.NameComparer());

            if (assets != null)
            {

                int i = 0;
                foreach (Asset asset in assets)
                {
                    i++;
                    if (i >= 1000)
                    {
                        i = 0;
                        this.Update();
                    }
                    if (asset.Name.IndexOf(searchBox.Text, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        soundListBox.Items.Add(asset);
                    }                   
                      
                }
               
            }

            int count = soundListBox.Items.Count;
            int max = assets != null ? assets.Count : 0;

            filesListed.Text = count + "/" + max;

        }

        private void SoundBrowser_Load(object sender, EventArgs e)
        {
            initFmod();

            Application.Idle += onIdle;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (searchBox.Text.Length > 0)
            {
                searchBox.BackColor = Color.Yellow;
                SearchBoxClear.Enabled = true;

            }
            else
            {
                searchBox.BackColor = Color.White;
                SearchBoxClear.Enabled = false;
            }

            refreshTimer.Stop();
            refreshListBox();
            
        }

        public void onEnter(object sender, EventArgs e)
        {
           
        }

        private void SearchBoxClear_Click(object sender, EventArgs e)
        {
            searchBox.Clear();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            refreshTimer.Stop();
            refreshTimer.Start();
        }
        FMOD.Sound subsound = null;
           
        private void PlayPause_Click(object sender, EventArgs e)
        {
            fsb.getSubSound(0, ref subsound);
            FMOD.RESULT res = system.playSound(FMOD.CHANNELINDEX.FREE, subsound, false, ref channel);
          
            if (res != FMOD.RESULT.OK)
            {
                MessageBox.Show("Cannot Play file.  Reason: " + res.ToString(), "FMOD Load Error", MessageBoxButtons.OK);
            }


        }


        private void onIdle(object sender, EventArgs e)
        {
            system.update();           
          
        }

        private void soundListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Asset asset = null;

            try
            {
                asset = (Asset)soundListBox.SelectedItem;
            }
            catch (InvalidCastException) { return; }

            loadSound(asset.Name);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if(channel != null)
            {
                bool playing = false;
                channel.isPlaying(ref playing);
                if (playing)
                {
                    channel.stop();
                }
                subsound.release();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            List<String> fileNames = new List<string>();

            foreach (object selectedItem in soundListBox.SelectedItems)
            {
                Asset asset = null;

                try
                {
                    asset = (Asset)selectedItem;
                }
                catch (InvalidCastException) { continue; }

                fileNames.Add(asset.Name);
            }

           // SoundExportForm modelExportForm = new SoundExportForm();
           // modelExportForm.FileNames = fileNames;
           // modelExportForm.ShowDialog();
        }

        
    }
}
