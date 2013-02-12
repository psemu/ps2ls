using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ps2ls.Assets.Dme
{
    public struct BoneMapEntry
    {
        public UInt16 BoneIndex;
        public UInt16 GlobalIndex;

        public static BoneMapEntry LoadFromStream(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            BoneMapEntry boneMapEntry = new BoneMapEntry();

            boneMapEntry.BoneIndex = binaryReader.ReadUInt16();
            boneMapEntry.GlobalIndex = binaryReader.ReadUInt16();

            return boneMapEntry;
        }
    }

    public class BoneMap
    {
        public UInt32 Unknown0 { get; private set; }
        public UInt32 BoneStart { get; private set; }
        public UInt32 BoneCount { get; private set; }
        public UInt32 Delta { get; private set; }
        public UInt32 Unknown1 { get; private set; }
        public UInt32 BoneEnd { get; private set; }
        public UInt32 VertexCount { get; private set; }
        public UInt32 Unknown2 { get; private set; }
        public UInt32 IndexCount { get; private set; }

        private BoneMap()
        {
        }

        public static BoneMap LoadFromStream(Stream stream)
        {
            if (stream == null)
                return null;

            BinaryReader binaryReader = new BinaryReader(stream);

            BoneMap boneMap = new BoneMap();

            boneMap.Unknown0 = binaryReader.ReadUInt32();
            boneMap.BoneStart = binaryReader.ReadUInt32();
            boneMap.BoneCount = binaryReader.ReadUInt32();
            boneMap.Delta = binaryReader.ReadUInt32();
            boneMap.Unknown1 = binaryReader.ReadUInt32();
            boneMap.BoneEnd = binaryReader.ReadUInt32();
            boneMap.VertexCount = binaryReader.ReadUInt32();
            boneMap.Unknown2 = binaryReader.ReadUInt32();
            boneMap.IndexCount = binaryReader.ReadUInt32();

            return boneMap;
        }
    }
}
