using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ps2ls.Assets.Dma
{
    public class Material
    {
        public UInt32 NameHash { get; private set; }
        public UInt32 DataLength { get; private set; }
        public UInt32 MaterialDefinitionHash { get; private set; }
        public List<Parameter> Parameters { get; private set; }

        private Material()
        {
        }

        public static Material LoadFromStream(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            Material material = new Material();

            material.NameHash = binaryReader.ReadUInt32();
            material.DataLength = binaryReader.ReadUInt32();
            material.MaterialDefinitionHash = binaryReader.ReadUInt32();

            UInt32 parameterCount = binaryReader.ReadUInt32();
            material.Parameters = new List<Parameter>((Int32)parameterCount);

            for (UInt32 j = 0; j < parameterCount; ++j)
            {
                Parameter parameter = Parameter.LoadFromStream(stream);

                material.Parameters.Add(parameter);
            }

            return material;
        }

        public class Parameter
        {
            //http://msdn.microsoft.com/en-us/library/windows/desktop/bb205378(v=vs.85).aspx
            public enum D3DXParameterClass
            {
                Scalar = 0,
                Vector,
                MatrixRows,
                MatrixColumns,
                Object,
                Struct,
                ForceDword = 0x7fffffff
            }

            //http://msdn.microsoft.com/en-us/library/windows/desktop/bb205380(v=vs.85).aspx
            public enum D3DXParameterType
            {
                Void = 0,
                Bool,
                Int,
                Float,
                String,
                Texture,
                Texture1D,
                Texture2D,
                Texture3D,
                TextureCube,
                Sampler,
                Sampler1D,
                Sampler2D,
                Sampler3D,
                SamplerCube,
                PixelShader,
                VertexShader,
                PixelFragment,
                VertexFrament,
                Unsupported,
                ForceDword = 0x7fffffff
            }

            private Parameter()
            {
            }

            public static Parameter LoadFromStream(Stream stream)
            {
                Parameter parameter = new Parameter();

                BinaryReader binaryReader = new BinaryReader(stream);

                parameter.NameHash = binaryReader.ReadUInt32();
                parameter.Class = (D3DXParameterClass)binaryReader.ReadUInt32();
                parameter.Type = (D3DXParameterType)binaryReader.ReadUInt32();

                UInt32 dataLength = binaryReader.ReadUInt32();

                parameter.Data = binaryReader.ReadBytes((Int32)dataLength);

                return parameter;
            }

            public UInt32 NameHash { get; private set; }
            public D3DXParameterClass Class { get; private set; }
            public D3DXParameterType Type { get; private set; }
            public byte[] Data { get; private set; }
        }
    }
}
