using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ps2ls.Assets.Adr
{
    public class ActorSocket
    {
        public string Bone
        {
            get;
            internal set;
        }
        public string Name
        {
            get;
            internal set;
        }

        public float Heading
        {
            get;
            internal set;
        }

        public float Pitch
        {
            get;
            internal set;
        }
        public float Roll
        {
            get;
            internal set;
        }
        public float ScaleX
        {
            get;
            internal set;
        }
        public float ScaleY
        {
            get;
            internal set;
        }
        public float ScaleZ
        {
            get;
            internal set;
        }
        public float OffsetX
        {
            get;
            internal set;
        }
        public float OffsetY
        {
            get;
            internal set;
        }
        public float OffsetZ
        {
            get;
            internal set;
        }
        public bool TransferToParent
        {
            get;
            internal set;
        }

    }

}
