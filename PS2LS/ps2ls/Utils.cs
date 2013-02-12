using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace ps2ls
{
    public static class Utils
    {
        public static Color GenerateRandomColor(Random random, Color mix)
        {
            Int32 red = random.Next(256);
            Int32 green = random.Next(256);
            Int32 blue = random.Next(256);

            red = (red + mix.R) / 2;
            green = (green + mix.R) / 2;
            blue = (blue + mix.R) / 2;

            return Color.FromArgb(red, green, blue);
        }

        static int[] knownLocations = { 0x44,  0x0b74, 0x0868, 0x250, 0x354, 0x96c, 0x660, 0x1088, 0x55c, 0x458, 0x764 };

        public static MemoryStream FixSoundHeader(MemoryStream stream)
        {

            for (int i = 0; i < knownLocations.Length; i++)
            {
                int loc = knownLocations[i];
                stream.Seek(loc + 0x03, SeekOrigin.Begin);
                 byte c = (byte)stream.ReadByte();

                 if (c == 0x16)
                 {
                     stream.Seek(loc, SeekOrigin.Begin);
                     byte b = (byte)stream.ReadByte();
                     b = (byte)(b - 1);
                     stream.Seek(loc, SeekOrigin.Begin);
                     stream.WriteByte(b);
                     break;
                 }
            }
                       
            return stream;
        }
        
    }
}
