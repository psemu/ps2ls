using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using ps2ls.Cryptography;

namespace ps2ls.Graphics.Materials
{
    public class MaterialDefinition
    {
        public String Name { get; private set; }
        public UInt32 NameHash { get; private set; }
        public String Type { get; private set; }
        public UInt32 TypeHash { get; private set; }
        public List<DrawStyle> DrawStyles { get; private set; }

        private MaterialDefinition()
        {
            Name = String.Empty;
            NameHash = 0;
            Type = String.Empty;
            TypeHash = 0;
            DrawStyles = new List<DrawStyle>();
        }

        public static MaterialDefinition LoadFromXPathNavigator(XPathNavigator navigator)
        {
            if (navigator == null)
            {
                return null;
            }

            MaterialDefinition materialDefinition = new MaterialDefinition();

            //name
            materialDefinition.Name = navigator.GetAttribute("Name", String.Empty);
            materialDefinition.NameHash = Jenkins.OneAtATime(materialDefinition.Name);

            //type
            materialDefinition.Type = navigator.GetAttribute("Type", String.Empty);
            materialDefinition.TypeHash = Jenkins.OneAtATime(materialDefinition.Type);

            //draw styles
            XPathNodeIterator entries = navigator.Select("./Array[@Name='DrawStyles']/Object[@Class='DrawStyle']");

            while (entries.MoveNext())
            {
                DrawStyle drawStyle = DrawStyle.LoadFromXPathNavigator(entries.Current);

                if (drawStyle != null)
                {
                    materialDefinition.DrawStyles.Add(drawStyle);
                }
            }

            return materialDefinition;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
