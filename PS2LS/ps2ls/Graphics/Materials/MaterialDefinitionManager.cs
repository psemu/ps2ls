using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace ps2ls.Graphics.Materials
{
    public class MaterialDefinitionManager
    {
        #region Singleton
        private static MaterialDefinitionManager instance = null;

        public static void CreateInstance()
        {
            instance = new MaterialDefinitionManager();

            StringReader stringReader = new StringReader(Properties.Resources.materials_3);
            instance.loadFromStringReader(stringReader);
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static MaterialDefinitionManager Instance { get { return instance; } }
        #endregion

        public Dictionary<UInt32, MaterialDefinition> MaterialDefinitions { get; private set; }
        public Dictionary<UInt32, VertexLayout> VertexLayouts { get; private set; }

        MaterialDefinitionManager()
        {
            MaterialDefinitions = new Dictionary<UInt32, MaterialDefinition>();
            VertexLayouts = new Dictionary<UInt32, VertexLayout>();
        }

        private void loadFromStringReader(StringReader stringReader)
        {
            if (stringReader == null)
                return;

            XPathDocument document = null;

            try
            {
                document = new XPathDocument(stringReader);
            }
            catch (Exception)
            {
                return;
            }

            XPathNavigator navigator = document.CreateNavigator();

            //vertex layouts
            loadVertexLayoutsByXPathNavigator(navigator.Clone());

            //TODO: parameter groups

            //material definitions
            loadMaterialDefinitionsByXPathNavigator(navigator.Clone());
        }

        private void loadMaterialDefinitionsByXPathNavigator(XPathNavigator navigator)
        {
            XPathNodeIterator materialDefinitions = null;

            try
            {
                materialDefinitions = navigator.Select("/Object/Array[@Name='MaterialDefinitions']/Object[@Class='MaterialDefinition']");
            }
            catch (Exception)
            {
                return;
            }

            while (materialDefinitions.MoveNext())
            {
                MaterialDefinition materialDefinition = MaterialDefinition.LoadFromXPathNavigator(materialDefinitions.Current);

                if (materialDefinition != null && false == MaterialDefinitions.ContainsKey(materialDefinition.NameHash))
                {
                    MaterialDefinitions.Add(materialDefinition.NameHash, materialDefinition);
                }
            }
        }

        private void loadVertexLayoutsByXPathNavigator(XPathNavigator navigator)
        {
            //material definitions
            XPathNodeIterator vertexLayouts = null;

            try
            {
                vertexLayouts = navigator.Select("/Object/Array[@Name='InputLayouts']/Object[@Class='InputLayout']");
            }
            catch (Exception)
            {
                return;
            }

            while (vertexLayouts.MoveNext())
            {
                VertexLayout vertexLayout = VertexLayout.LoadFromXPathNavigator(vertexLayouts.Current);

                if (vertexLayout != null && false == VertexLayouts.ContainsKey(vertexLayout.NameHash))
                {
                    VertexLayouts.Add(vertexLayout.NameHash, vertexLayout);
                }
            }
        }

        public MaterialDefinition GetMaterialDefinitionFromHash(UInt32 materialDefinitionHash)
        {
            MaterialDefinition materialDefinition = null;

            try
            {
                MaterialDefinitions.TryGetValue(materialDefinitionHash, out materialDefinition);
            }
            catch (Exception)
            {
                throw new Exception("Material definition could not be found.");
            }

            return materialDefinition;
        }
    }
}
