using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Org.Filedrops.FileSystem
{
    [Serializable()]
    public abstract class FiledropsFileSystemEntry
    {
        /// <summary>
        /// The metadata associated with this system entry.
        /// </summary>
        public XmlDocument MetaData { 
            get{return doc;}
            set{
                if (value != null)
                {
                    doc = value;
                }
            }
        }

        [field: NonSerialized()]
        private XmlDocument doc;

        /// <summary>
        /// The underlying FileSystem, i.e. where this entry is read from.
        /// </summary>
        public FiledropsFileSystem FileSystem { get; set; }

        /// <summary>
        /// The relative path to this system entry.
        /// </summary>
		private string _fullname;
		public virtual string FullName
		{
			get
			{
				return _fullname;
			}
			set
			{
				_fullname = value;
				Name = Path.GetFileName(value);
                NameWithoutExtension = Name;
			}
		}

        public virtual DateTime LastModified { get; set; }

        public virtual FiledropsDirectory Parent { get; set; }

        public virtual string Name { get; set; }

        public virtual string NameWithoutExtension { get; set; }

        public virtual FileSystemEntryType EntryType { get; protected set; }

        /// <summary>
        /// Construct a metadata file using a default metadata object.
        /// </summary>
        public FiledropsFileSystemEntry() { initializeMetaData(); }

        /// <summary>
        /// Construct a metadata file using the given metadata XML-document.
        /// </summary>
        public FiledropsFileSystemEntry(XmlDocument metaData) { MetaData = metaData; }

        /// <summary>
        /// Initializes the MetaData property to an empty XML-file containing
        /// only an XML declaration and a root element &lt;MetaData&gt;.
        /// </summary>
        void initializeMetaData()
        {
            MetaData = new XmlDocument();
            MetaData.AppendChild(MetaData.CreateXmlDeclaration("1.0", null, null));
            MetaData.AppendChild(MetaData.CreateElement("MetaData"));
        }

        public abstract void Rename(string name);

        public abstract void Delete();

        public abstract bool Exists();

        /// <summary>
        /// Add a key-value pair to the metadata document, appending to root.
        /// </summary>
        public virtual void AddMetaData(string key, string value)
        {
            AddMetaData("MetaData", key, value);
        }

        /// <summary>
        /// Extension of AddMetaData(string key, string value), appends an attribute
        /// </summary>
        public virtual void AddMetaData(string key, string value, string attributeName, string attributeValue)
        {
            AddMetaData("MetaData", key, value, attributeName, attributeValue);
        }

        /// <summary>
        /// Add a key-value pair to the metadata document, appending to any
        /// node that matches the given xpath.
        /// </summary>
        public virtual void AddMetaData(string xpath, string key, string value)
        {
            foreach (XmlNode node in MetaData.SelectNodes(xpath))
            {
                XmlElement elt = MetaData.CreateElement(key);
                elt.InnerText = value;
                node.AppendChild(elt);
            }
        }

        /// <summary>
        /// Extension of AddMetaData(string xpath, string key, string value),
        /// appends an attribute
        /// </summary>
        public virtual void AddMetaData(string xpath, string key, string value, string attributeName, string attributeValue)
        {
            foreach (XmlNode node in MetaData.SelectNodes(xpath))
            {
                XmlElement elt = MetaData.CreateElement(key);
                elt.SetAttribute(attributeName, attributeValue);
                elt.InnerText = value;
                node.AppendChild(elt);
            }
        }


        //TODO: implement for each filesystem
        [field: NonSerialized()]
        protected BitmapImage _icon16x16;
        [field: NonSerialized()]
        protected BitmapImage _icon24x24;
        [field: NonSerialized()]
        protected BitmapImage _icon32x32;
        [field: NonSerialized()]
        protected BitmapImage _icon48x48;
        [field: NonSerialized()]
        protected BitmapImage _icon64x64;
        [field: NonSerialized()]
        protected BitmapImage _icon128x128;
        [field: NonSerialized()]
        protected BitmapImage _icon256x256;


        public virtual BitmapImage Icon16x16 { get { return null; } }
        public virtual BitmapImage Icon24x24 { get { return null; } }
        public virtual BitmapImage Icon32x32 { get { return null; } }
        public virtual BitmapImage Icon48x48 { get { return null; } }
        public virtual BitmapImage Icon64x64 { get { return null; } }
        public virtual BitmapImage Icon128x128 { get { return null; } }
        public virtual BitmapImage Icon256x256 { get { return null; } }

		/*public Image Icon16x16 { get { return FileImageGetter.GetImage(FullName, 16); } }
		public Image Icon24x24 { get { return FileImageGetter.GetImage(FullName, 24); } }
		public Image Icon32x32 { get { return FileImageGetter.GetImage(FullName, 32); } }
		public Image Icon48x48 { get { return FileImageGetter.GetImage(FullName, 48); } }
		public Image Icon64x64 { get { return FileImageGetter.GetImage(FullName, 64); } }
		public Image Icon128x128 { get { return FileImageGetter.GetImage(FullName, 128); } }
		public Image Icon256x256 { get { return FileImageGetter.GetImage(FullName, 256); } }*/
    }

	public enum FileSystemEntryType : short
	{
		Folder, File, Undefined
	}
}
