using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ps2ls.Assets.Dme;
using System.Globalization;
using System.IO;
using DevIL;
using ps2ls.Assets.Pack;
using ps2ls.Graphics.Materials;
using OpenTK;

namespace ps2ls.IO
{
    public class ObjModelExporter : ModelExporter
    {
        public string Name
        {
            get { return "Wavefront OBJ"; }
        }

        public string Extension
        {
            get { return "obj"; }
        }

        public bool CanExportNormals
        {
            get { return false; }
        }

        public bool CanExportTextureCoordinates
        {
            get { return true; }
        }

        public void ExportModelToDirectoryWithExportOptions(Model model, String directory, ExportOptions exportOptions)
        {
            //TODO: Figure out what to do with non-version 4 models.
            if (model != null && model.Version != 4)
            {
                return;
            }

            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            if (exportOptions.Package)
            {
                try
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(directory + @"\" + Path.GetFileNameWithoutExtension(model.Name));
                    directory = directoryInfo.FullName;
                }
                catch (Exception) { }
            }

            if (exportOptions.Textures)
            {
                ImageImporter imageImporter = new ImageImporter();
                ImageExporter imageExporter = new ImageExporter();

                foreach(String textureString in model.TextureStrings)
                {
                    MemoryStream textureMemoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(textureString);

                    if(textureMemoryStream == null)
                        continue;

                    Image textureImage = imageImporter.LoadImageFromStream(textureMemoryStream);

                    if(textureImage == null)
                        continue;

                    imageExporter.SaveImage(textureImage, exportOptions.TextureFormat.ImageType, directory + @"\" + Path.GetFileNameWithoutExtension(textureString) + @"." + exportOptions.TextureFormat.Extension);
                }
            }

            String path = directory + @"\" + Path.GetFileNameWithoutExtension(model.Name) + ".obj";

            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            for (Int32 i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

                MaterialDefinition materialDefinition = MaterialDefinitionManager.Instance.MaterialDefinitions[model.Materials[(Int32)mesh.MaterialIndex].MaterialDefinitionHash];
                VertexLayout vertexLayout = MaterialDefinitionManager.Instance.VertexLayouts[materialDefinition.DrawStyles[0].VertexLayoutNameHash];

                //position
                VertexLayout.Entry.DataTypes positionDataType;
                Int32 positionOffset;
                Int32 positionStreamIndex;

                vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Position, 0, out positionDataType, out positionStreamIndex, out positionOffset);
                
                Mesh.VertexStream positionStream = mesh.VertexStreams[positionStreamIndex];

                for (Int32 j = 0; j < mesh.VertexCount; ++j)
                {
                    Vector3 position = readVector3(exportOptions, positionOffset, positionStream, j);

                    position.X *= exportOptions.Scale.X;
                    position.Y *= exportOptions.Scale.Y;
                    position.Z *= exportOptions.Scale.Z;

                    streamWriter.WriteLine("v " + position.X.ToString(format) + " " + position.Y.ToString(format) + " " + position.Z.ToString(format));
                }

                //texture coordinates
                if (exportOptions.TextureCoordinates)
                {
                    VertexLayout.Entry.DataTypes texCoord0DataType;
                    Int32 texCoord0Offset = 0;
                    Int32 texCoord0StreamIndex = 0;

                    Boolean texCoord0Present = vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Texcoord, 0, out texCoord0DataType, out texCoord0StreamIndex, out texCoord0Offset);

                    if (texCoord0Present)
                    {
                        Mesh.VertexStream texCoord0Stream = mesh.VertexStreams[texCoord0StreamIndex];

                        for (Int32 j = 0; j < mesh.VertexCount; ++j)
                        {
                            Vector2 texCoord;

                            switch (texCoord0DataType)
                            {
                                case VertexLayout.Entry.DataTypes.Float2:
                                    texCoord.X = BitConverter.ToSingle(texCoord0Stream.Data, (j * texCoord0Stream.BytesPerVertex) + 0);
                                    texCoord.Y = 1.0f - BitConverter.ToSingle(texCoord0Stream.Data, (j * texCoord0Stream.BytesPerVertex) + 4);
                                    break;
                                case VertexLayout.Entry.DataTypes.float16_2:
                                    texCoord.X = Half.FromBytes(texCoord0Stream.Data, (j * texCoord0Stream.BytesPerVertex) + texCoord0Offset + 0).ToSingle();
                                    texCoord.Y = 1.0f - Half.FromBytes(texCoord0Stream.Data, (j * texCoord0Stream.BytesPerVertex) + texCoord0Offset + 2).ToSingle();
                                    break;
                                default:
                                    texCoord.X = 0;
                                    texCoord.Y = 0;
                                    break;
                            }

                            streamWriter.WriteLine("vt " + texCoord.X.ToString(format) + " " + texCoord.Y.ToString(format));
                        }
                    }
                }
            }

            //faces
            UInt32 vertexCount = 0;

            for (Int32 i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

                streamWriter.WriteLine("g Mesh" + i);

                for (Int32 j = 0; j < mesh.IndexCount; j += 3)
                {
                    UInt32 index0, index1, index2;

                    switch (mesh.IndexSize)
                    {
                        case 2:
                            index0 = vertexCount + BitConverter.ToUInt16(mesh.IndexData, (j * 2) + 0) + 1;
                            index1 = vertexCount + BitConverter.ToUInt16(mesh.IndexData, (j * 2) + 2) + 1;
                            index2 = vertexCount + BitConverter.ToUInt16(mesh.IndexData, (j * 2) + 4) + 1;
                            break;
                        case 4:
                            index0 = vertexCount + BitConverter.ToUInt32(mesh.IndexData, (j * 4) + 0) + 1;
                            index1 = vertexCount + BitConverter.ToUInt32(mesh.IndexData, (j * 4) + 4) + 1;
                            index2 = vertexCount + BitConverter.ToUInt32(mesh.IndexData, (j * 4) + 8) + 1;
                            break;
                        default:
                            index0 = 0;
                            index1 = 0;
                            index2 = 0;
                            break;
                    }

                    if (exportOptions.Normals && exportOptions.TextureCoordinates)
                    {
                        streamWriter.WriteLine("f " + index2 + "/" + index2 + "/" + index2 + " " + index1 + "/" + index1 + "/" + index1 + " " + index0 + "/" + index0 + "/" + index0);
                    }
                    else if (exportOptions.Normals)
                    {
                        streamWriter.WriteLine("f " + index2 + "//" + index2 + " " + index1 + "//" + index1 + " " + index0 + "//" + index0);
                    }
                    else if (exportOptions.TextureCoordinates)
                    {
                        streamWriter.WriteLine("f " + index2 + "/" + index2 + " " + index1 + "/" + index1 + " " + index0 + "/" + index0);
                    }
                    else
                    {
                        streamWriter.WriteLine("f " + index2 + " " + index1 + " " + index0);
                    }
                }

                vertexCount += (UInt32)mesh.VertexCount;
            }

            streamWriter.Close();
        }

        private static Vector3 readVector3(ExportOptions exportOptions, Int32 offset, Mesh.VertexStream vertexStream, Int32 index)
        {
            Vector3 vector3 = new Vector3();

            switch (exportOptions.LeftAxis)
            {
                case Axes.X:
                    vector3.X = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 0);
                    break;
                case Axes.Y:
                    vector3.Y = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 0);
                    break;
                case Axes.Z:
                    vector3.Z = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 0);
                    break;
            }

            switch (exportOptions.UpAxis)
            {
                case Axes.X:
                    vector3.X = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 4);
                    break;
                case Axes.Y:
                    vector3.Y = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 4);
                    break;
                case Axes.Z:
                    vector3.Z = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 4);
                    break;
            }

            switch (exportOptions.ForwardAxis)
            {
                case Axes.X:
                    vector3.X = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 8);
                    break;
                case Axes.Y:
                    vector3.Y = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 8);
                    break;
                case Axes.Z:
                    vector3.Z = BitConverter.ToSingle(vertexStream.Data, (vertexStream.BytesPerVertex * index) + offset + 8);
                    break;
            }

            return vector3;
        }
    }
}
