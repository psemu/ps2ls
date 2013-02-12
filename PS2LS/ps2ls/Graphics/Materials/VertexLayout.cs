using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ps2ls.Cryptography;
using System.Xml.XPath;

namespace ps2ls.Graphics.Materials
{
    public class VertexLayout
    {
        public class Entry
        {
            public enum DataTypes
            {
                None = -1,
                Float3,
                D3dcolor,
                Float2,
                Float4,
                ubyte4n,
                float16_2,
                Short2,
                Float1,
                Short4,
            }

            private static String[] dataTypeStrings =
            {
                "Float3",
                "D3dcolor",
                "Float2",
                "Float4",
                "ubyte4n",
                "float16_2",
                "Short2",
                "Float1",
                "Short4"
            };

            public static Int32[] dataTypeSizes =
            {
                12, //Float3
                4,  //D3dcolor
                8,  //Float2
                16, //Float4
                4,  //ubyte4n
                8,  //float16_2
                4,  //Short2
                4,  //Float1
                8,  //Short4
            };

            public enum DataUsages
            {
                None = -1,
                Position,
                Color,
                Texcoord,
                Tangent,
                Binormal,
                BlendWeight,
                BlendIndices,
                Normal
            }

            private static String[] dataUsageStrings =
            {
                "Position",
                "Color",
                "Texcoord",
                "Tangent",
                "Binormal",
                "BlendWeight",
                "BlendIndices",
                "Normal"
            };

            public UInt32 Stream;
            public DataTypes DataType;
            public DataUsages DataUsage;
            public UInt32 DataUsageIndex;

            public static void GetDataTypeFromString(String typeString, out DataTypes type)
            {
                for (Int32 i = 0; i < dataTypeStrings.Length; ++i)
                {
                    if (String.Compare(typeString, dataTypeStrings[i], true) == 0)
                    {
                        type = (DataTypes)i;
                        return;
                    }
                }

                type = DataTypes.None;
            }

            public static void GetDataUsageFromString(String usageString, out DataUsages usage)
            {
                for (Int32 i = 0; i < dataUsageStrings.Length; ++i)
                {
                    if (String.Compare(usageString, dataUsageStrings[i], true) == 0)
                    {
                        usage = (DataUsages)i;
                        return;
                    }
                }

                usage = DataUsages.None;
            }

            public static Int32 GetDataTypeSize(DataTypes type)
            {
                return dataTypeSizes[(Int32)type];
            }
        }

        public String Name { get; private set; }
        public UInt32 NameHash { get; private set; }
        public List<Entry> Entries { get; private set; }

        private VertexLayout()
        {
            Entries = new List<Entry>();
        }

        public static VertexLayout LoadFromXPathNavigator(XPathNavigator navigator)
        {
            if (navigator == null)
            {
                return null;
            }

            VertexLayout vertexLayout = new VertexLayout();

            //name
            vertexLayout.Name = navigator.GetAttribute("Name", String.Empty);

            //name hash
            vertexLayout.NameHash = Jenkins.OneAtATime(vertexLayout.Name);

            //entries
            XPathNodeIterator entries = navigator.Select("./Array[@Name='Entries']/Object[@Class='LayoutEntry']");

            while (entries.MoveNext())
            {
                navigator = entries.Current;

                VertexLayout.Entry entry = new Entry();

                //stream
                entry.Stream = UInt32.Parse(navigator.GetAttribute("Stream", String.Empty));

                //data type
                String dataTypeString = navigator.GetAttribute("Type", String.Empty);
                Entry.GetDataTypeFromString(dataTypeString, out entry.DataType);

                //data usage
                String dataUsageString = navigator.GetAttribute("Usage", String.Empty);
                Entry.GetDataUsageFromString(dataUsageString, out entry.DataUsage);

                //data usage index
                entry.DataUsageIndex = UInt32.Parse(navigator.GetAttribute("UsageIndex", String.Empty));

                vertexLayout.Entries.Add(entry);
            }

            return vertexLayout;
        }

        public override string ToString()
        {
            return Name;
        }

        public Boolean HasDataUsage(Entry.DataUsages usage)
        {
            return GetEntryCountByDataUsage(usage) > 0;
        }

        public Int32 GetEntryCountByDataUsage(Entry.DataUsages usage)
        {
            Int32 count = 0;

            foreach (Entry entry in Entries)
            {
                if (entry.DataUsage == usage)
                {
                    ++count;
                }
            }

            return count;
        }

        public Boolean GetEntryInfoFromDataUsageAndUsageIndex(Entry.DataUsages dataUsage, Int32 usageIndex, out Entry.DataTypes dataType, out Int32 stream, out Int32 offset)
        {
            dataType = Entry.DataTypes.None;
            stream = 0;
            offset = 0;

            UInt32 previousStream = 0;

            foreach (Entry entry in Entries)
            {
                if (entry.Stream != previousStream)
                {
                    offset = 0;
                }

                stream = (Int32)entry.Stream;

                if (entry.DataUsage == dataUsage && entry.DataUsageIndex == usageIndex)
                {
                    dataType = entry.DataType;
                    return true;
                }

                //increment offset
                offset += Entry.GetDataTypeSize(entry.DataType);

                //set previous stream for next iteration
                previousStream = entry.Stream;
            }

            return false;
        }
    }
}
