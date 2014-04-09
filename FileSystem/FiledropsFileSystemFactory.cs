using System;
using System.Xml;

namespace Org.Filedrops.FileSystem
{
    /// <summary>
    /// Factory for IDirectory objects.
    /// </summary>
    public static class FiledropsFileSystemFactory
    {
        /// <summary>
        /// Create an IDirectory object that is represented by an XML document.
        /// </summary>
        public static FiledropsFileSystem Create(string path)
        {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            return Create(document.DocumentElement);
        }

        
        /// <summary>
        /// Create an IDirectory object that is represented by an XML document.
        /// </summary>
        public static FiledropsFileSystem Create(XmlElement fileSystemElement)
        {
            string type = fileSystemElement.GetAttribute("type");
            object[] argObjects = new[] { fileSystemElement };
            return (FiledropsFileSystem)Activator.CreateInstance(Type.GetType(type), argObjects);
        }
    }
}
