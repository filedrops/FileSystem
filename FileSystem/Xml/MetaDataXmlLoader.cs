using System;
using System.IO;
using System.Reflection;
using System.Xml;


namespace Org.Filedrops.FileSystem.Xml
{
    public static class MetaDataXmlLoader
    {
        static readonly string TEMPLATE_PATH = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
        static readonly string TEMPLATE_FILE = "Org.Filedrops.FileSystem.Xml.MetaDataXmlTemplate.xml";
        public static readonly string NAMESPACE = "http://filedrops.org";

        public static XmlDocument LoadTemplateXml()
        {
            XmlDocument doc = new XmlDocument(); //EmbeddedResourceTools.GetXmlDocument(TEMPLATE_FILE);
            doc.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(TEMPLATE_FILE));
            return doc;
        }

        public static XmlNode AddElement(XmlDocument doc, String name, String attrKey = null, String attrValue = null)
        {
            XmlElement el = doc.CreateElement(name);
            if (!string.IsNullOrWhiteSpace(attrKey) && !string.IsNullOrWhiteSpace(attrValue))
            {
                el.SetAttribute(attrKey, NAMESPACE, attrValue);
            }
            return doc.DocumentElement.AppendChild(el);
        }

        public static XmlNode AddCustomField(XmlDocument doc, XmlElement el)
        {
            XmlNode customFields = doc.DocumentElement.SelectSingleNode("CustomFields");
            if (customFields == null)
            {
                customFields = doc.CreateElement("CustomFields") as XmlNode;
                doc.DocumentElement.AppendChild(customFields);
            }

            return customFields.AppendChild(el);
        }
    }
}
