using System;
using System.Collections.Generic;
using System.IO;

namespace ps2ls.Assets.Adr
{

   
    
    public class ActorDefinition
    {
        public string ModelName
        {
            get;
            internal set;
        }
        public string MaterialName 
        {
            get;
            internal set;
        }
        public string MorphemeNetwork
        {
            get;
            internal set;
        }
        public string MorphemeRig
        {
            get;
            internal set;
        }
        public string AttachToSocket
        {
            get;
            internal set;
        }

        public List<ActorSocket> Sockets
        {
            get;
            internal set;
        }

        public TextureAlias TextureAlias
        {
            get;
            internal set;
        }



    }



}
