using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ps2ls.Graphics.Materials;
using ps2ls.Cryptography;
using OpenTK;

namespace ps2ls.Assets.Dme
{
    public class Mesh
    {
        public class VertexStream
        {
            public static VertexStream LoadFromStream(Stream stream, Int32 vertexCount, Int32 bytesPerVertex)
            {
                VertexStream vertexStream = new VertexStream();

                vertexStream.BytesPerVertex = bytesPerVertex;

                BinaryReader binaryReader = new BinaryReader(stream);

                vertexStream.Data = binaryReader.ReadBytes(vertexCount * bytesPerVertex);

                return vertexStream;
            }

            public Int32 BytesPerVertex { get; private set; }
            public Byte[] Data { get; private set; }
        }

        public VertexStream[] VertexStreams { get; private set; }
        public Byte[] IndexData { get; private set; }

        public UInt32 MaterialIndex { get; set; }
        public UInt32 Unknown1 { get; set; }
        public UInt32 Unknown2 { get; set; }
        public UInt32 Unknown3 { get; set; }
        public UInt32 Unknown4 { get; set; }
        public UInt32 VertexCount { get; set; }
        public UInt32 IndexCount { get; private set; }
        public UInt32 IndexSize { get; private set; }

        private Mesh()
        {
        }

        public static Mesh LoadFromStream(Stream stream, ICollection<Dma.Material> materials)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            Mesh mesh = new Mesh();

            UInt32 bytesPerVertex = 0;
            UInt32 vertexStreamCount = 0;

            mesh.MaterialIndex = binaryReader.ReadUInt32();
            mesh.Unknown1 = binaryReader.ReadUInt32();
            mesh.Unknown2 = binaryReader.ReadUInt32();
            mesh.Unknown3 = binaryReader.ReadUInt32();
            vertexStreamCount = binaryReader.ReadUInt32();
            mesh.IndexSize = binaryReader.ReadUInt32();
            mesh.IndexCount = binaryReader.ReadUInt32();
            mesh.VertexCount = binaryReader.ReadUInt32();

            mesh.VertexStreams = new VertexStream[(Int32)vertexStreamCount];

            // read vertex streams
            for (Int32 j = 0; j < vertexStreamCount; ++j)
            {
                bytesPerVertex = binaryReader.ReadUInt32();

                VertexStream vertexStream = VertexStream.LoadFromStream(binaryReader.BaseStream, (Int32)mesh.VertexCount, (Int32)bytesPerVertex);

                if (vertexStream != null)
                {
                    mesh.VertexStreams[j] = vertexStream;
                }
            }

            // read indices
            mesh.IndexData = binaryReader.ReadBytes((Int32)mesh.IndexCount * (Int32)mesh.IndexSize);

            return mesh;
        }
    }
}
