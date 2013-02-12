/*
* Copyright (c) 2012 Nicholas Woodfield
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevIL;
using DevIL.Unmanaged;
using SD = System.Drawing;
using SDI = System.Drawing.Imaging;

namespace DevILNet.Sample {
    public partial class ImageViewer : Form {
        private static String s_image = "DevIL.jpg";
        private ImageImporter m_importer;
        private ImageExporter m_exporter;
        private Image m_activeImage;
        private Image m_copy;

        public ImageViewer() {
            InitializeComponent();
            Text = "Hello you DevIL!";
            this.CenterToScreen();

            InitDevIL();
            SetUpFileExtensions();

            LoadDefaultImage();
        }

        private void InitDevIL() {
            m_importer = new ImageImporter();
            m_exporter = new ImageExporter();
            ImageState state = new ImageState();
            state.AbsoluteFormat = DataFormat.BGRA;
            state.AbsoluteDataType = DataType.UnsignedByte;
            state.AbsoluteOrigin = OriginLocation.UpperLeft;
            state.Apply();

            CompressionState comp = new CompressionState();
            comp.KeepDxtcData = true;
            comp.Apply();

            SaveState sstate = new SaveState();
            sstate.OverwriteExistingFile = true;
            sstate.Apply();
        }

        private void LoadDefaultImage() {
            String filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), s_image);
            if(File.Exists(filename)) {
                using(Stream str = File.OpenRead(filename)) {
                    LoadImage(str);
                    RefreshPictureBox();
                }
            }
        }

        private void SetUpFileExtensions() {
            openFileDialog.Filter = CreateFilter(m_importer.GetSupportedExtensions());
            openFileDialog.Title = "Open an Image File";
            openFileDialog.AddExtension = true;
            saveFileDialog.Filter = CreateFilter(m_exporter.GetSupportedExtensions());
            saveFileDialog.Title = "Save an Image File";
        }

        private String CreateFilter(String[] exts) {
            StringBuilder filter = new StringBuilder();
            filter.Append("Image Files (");
            foreach(String ext in exts) {
                filter.Append("*" + ext + ",");
            }
            filter.Append(") |");
            return filter.ToString();
        }

        private void OpenFileDialog(object sender, EventArgs e) {
            DialogResult result = openFileDialog.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK) {
                if(File.Exists(openFileDialog.FileName)) {
                    using(Stream stream = openFileDialog.OpenFile()) {
                        LoadImage(stream);
                    }
                    RefreshPictureBox();
                }
            }
        }

        private void LoadImage(Stream stream) {
            try {
                m_activeImage = m_importer.LoadImageFromStream(stream);
                m_copy = m_activeImage.Clone();
            } catch(Exception) {
                m_activeImage = null;
                m_copy = null;
                //Show a pop up dialog?
            }
        }

        private void RefreshPictureBox() {
            if(m_activeImage == null) {
                pictureBox.Image = null;
            } else {
               
                m_activeImage.Bind();
                ImageInfo info = IL.GetImageInfo();
                SD.Bitmap bitmap = new SD.Bitmap(info.Width, info.Height, SDI.PixelFormat.Format32bppArgb);
                SD.Rectangle rect = new SD.Rectangle(0, 0, info.Width, info.Height);
                SDI.BitmapData data = bitmap.LockBits(rect, SDI.ImageLockMode.WriteOnly, SDI.PixelFormat.Format32bppArgb);

                //Since Scan0 is an IntPtr to the bitmap data and we're all the same size...just do a copy.
                IL.CopyPixels(0, 0, 0, info.Width, info.Height, 1, DataFormat.BGRA, DataType.UnsignedByte, data.Scan0);

                bitmap.UnlockBits(data);

                pictureBox.Image = (SD.Image) bitmap;
                
                ScaleImageToFit();
                pictureBox.Refresh();
            }
        }

        private void ScaleImageToFit() {
            if(pictureBox.Image == null) {
                return;
            }
      
            int width = pictureBox.Image.Width;
            int height = pictureBox.Image.Height;

            if(width <= panel1.Width && height <= panel1.Height) {
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            } else {
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void AlienifyImage(object sender, EventArgs e) {
            m_importer.Filter.Alienify(m_activeImage);
            RefreshPictureBox();
        }

        private void EmbossImage(object sender, EventArgs e) {
            m_importer.Filter.Emboss(m_activeImage);
            RefreshPictureBox();
        }

        private void NegativeImage(object sender, EventArgs e) {
            m_importer.Filter.Negative(m_activeImage);
            RefreshPictureBox();
        }

        private void EqualizeImage(object sender, EventArgs e) {
            m_importer.Filter.Equalize(m_activeImage);
            RefreshPictureBox();
        }

        private void BlurAverageImage(object sender, EventArgs e) {
            m_importer.Filter.BlurAverage(m_activeImage, 4);
            RefreshPictureBox();
        }

        private void BlurGaussianImage(object sender, EventArgs e) {
            m_importer.Filter.BlurGaussian(m_activeImage, 4);
            RefreshPictureBox();
        }

        private void EdgeDetectEImage(object sender, EventArgs e) {
            m_importer.Filter.EdgeDetectE(m_activeImage);
            RefreshPictureBox();
        }

        private void EdgeDetectSImage(object sender, EventArgs e) {
            m_importer.Filter.EdgeDetectS(m_activeImage);
            RefreshPictureBox();
        }

        private void EdgeDetectPImage(object sender, EventArgs e) {
            m_importer.Filter.EdgeDetectP(m_activeImage);
            RefreshPictureBox();
        }

        private void OnResize(object sender, EventArgs e) {
            ScaleImageToFit();
        }

        private void RotateImage(float angle) {
            if(m_activeImage != null) {
                m_importer.Transform.Rotate(m_activeImage, angle);
                RefreshPictureBox();
            }
        }

        private void RotateImage180(object sender, EventArgs e) {
            RotateImage(180);
        }

        private void RotateImage90CW(object sender, EventArgs e) {
           RotateImage(90);
        }

        private void RotateImage90CCW(object sender, EventArgs e) {
            RotateImage(-90);
        }

        private void ResetImage(object sender, EventArgs e) {
            if(m_activeImage != null) {
                m_activeImage.Dispose();
            }

            m_activeImage = m_copy.Clone();
            RefreshPictureBox();
        }

        private void SaveImage(object sender, EventArgs e) {

        }

        private void SaveAsFileDialog(object sender, EventArgs e) {
            DialogResult result = saveFileDialog.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK) {
                m_exporter.SaveImage(m_activeImage, saveFileDialog.FileName);
            }
        }

        private void MirrorImage(object sender, EventArgs e) {
            m_importer.Transform.Mirror(m_activeImage);
            RefreshPictureBox();
        }

        private void FlipImage(object sender, EventArgs e) {
            m_importer.Transform.FlipImage(m_activeImage);
            RefreshPictureBox();
        }
    }
}
