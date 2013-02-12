using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Assets.Dme
{
    public class Vertex
    {
        public Vertex()
        {
            Data = new List<byte>();
        }

        public Vector3 Position;
        public Vector3 Normal;
        public List<Byte> Data;
    }
}
