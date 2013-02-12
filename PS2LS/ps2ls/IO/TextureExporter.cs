using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevIL;

namespace ps2ls.IO
{
    public static class TextureExporter
    {
        public class TextureFormatInfo
        {
            public String Name { get; internal set; }
            public String Extension { get; internal set; }
            public ImageType ImageType { get; internal set; }

            internal TextureFormatInfo()
            {
            }

            public override string ToString()
            {
                return Name + @" (*." + Extension + @")";
            }
        }

        public static TextureFormatInfo[] TextureFormats;

        static TextureExporter()
        {
            createTextureFormats();
        }

        private static void createTextureFormats()
        {
            List<TextureFormatInfo> textureFormats = new List<TextureFormatInfo>();

            //DirectDraw Surface (*.dds)
            TextureFormatInfo textureFormat = null;

            textureFormat = new TextureFormatInfo();
            textureFormat.Name = "DirectDraw Surface";
            textureFormat.Extension = "dds";
            textureFormat.ImageType = ImageType.Dds;
            textureFormats.Add(textureFormat);

            //Microsoft Windows Bitmap (*.bmp)
            textureFormat = new TextureFormatInfo();
            textureFormat.Name = "Windows Bitmap";
            textureFormat.Extension = "bmp";
            textureFormat.ImageType = ImageType.Bmp;
            textureFormats.Add(textureFormat);

            //Portal Network Graphics (*.png)
            textureFormat = new TextureFormatInfo();
            textureFormat.Name = "Portable Network Graphics";
            textureFormat.Extension = "png";
            textureFormat.ImageType = ImageType.Png;
            textureFormats.Add(textureFormat);

            //Truevision TGA (*.tga)
            textureFormat = new TextureFormatInfo();
            textureFormat.Name = "Truevision TGA";
            textureFormat.Extension = "tga";
            textureFormat.ImageType = ImageType.Tga;
            textureFormats.Add(textureFormat);

            TextureFormats = textureFormats.ToArray();
        }
    }
}
