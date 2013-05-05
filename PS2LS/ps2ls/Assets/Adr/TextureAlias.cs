using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ps2ls.Assets.Adr
{
    public class TextureAliasEntry
    {
        public uint Hash
        {
            get;
            internal set;
        }
        public string Name
        {
            get;
            internal set;
        }
        public byte MaterialId
        {
            get;
            internal set;
        }

        public TextureAliasEntry()
        {
            MaterialId = 0;

        }


    }

    public class TextureAlias
    {
        public string Name
        {
            get;
            internal set;
        }
        List<TextureAliasEntry> Entries = new List<TextureAliasEntry>();

        protected void Add(uint hash, string name, byte materialID)
        {
            TextureAliasEntry entry = new TextureAliasEntry()
            {
                Hash = hash,
                Name = name,
                MaterialId = materialID
            };
            Entries.Add(entry);
        }
              

        public TextureAliasEntry Find(uint hash, byte materialID)
        {
            return Entries.DefaultIfEmpty(null).FirstOrDefault(entry => entry.Hash == hash && entry.MaterialId == materialID);
        }


        public static List<TextureAlias> Aliases = new List<TextureAlias>();
        static TextureAlias FindOrAddAlias(string name)
        {
            foreach (TextureAlias alias in Aliases)
            {
                if (alias.Name == name) return alias;
            }

            TextureAlias a = new TextureAlias()
            {
                Name = name
            };
            Aliases.Add(a);
            return a;
        }

        static TextureAlias GetAlias(string name)
        {
            return Aliases.DefaultIfEmpty(null).FirstOrDefault(alias => alias.Name == name);
        }



    }
}
