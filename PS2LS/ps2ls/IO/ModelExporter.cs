using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using ps2ls.Assets.Dme;

namespace ps2ls.IO
{
    public enum Axes
    {
        X,
        Y,
        Z
    }

    public class ExportOptions
    {
        public Axes UpAxis;
        public Axes LeftAxis;
        public Axes ForwardAxis
        {
            get
            {
                if (LeftAxis != Axes.X && UpAxis != Axes.X)
                {
                    return Axes.X;
                }
                else if (LeftAxis != Axes.Y && UpAxis != Axes.Y)
                {
                    return Axes.Y;
                }

                return Axes.Z;
            }
        }
        public Boolean Normals;
        public Boolean TextureCoordinates;
        public Vector3 Scale;
        public Boolean Textures;
        public Boolean Package;
        public TextureExporter.TextureFormatInfo TextureFormat;
    }

    public interface ModelExporter
    {
        String Name { get; }
        String Extension { get; }
        Boolean CanExportNormals { get; }
        Boolean CanExportTextureCoordinates { get; }

        void ExportModelToDirectoryWithExportOptions(Model model, String directory, ExportOptions exportOptions);
    }
}
