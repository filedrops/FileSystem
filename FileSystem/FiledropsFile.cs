using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Org.Filedrops.FileSystem
{
    /// <summary>
    /// Representation of a file with metadata.
    /// </summary>
    [Serializable()]
    public abstract class FiledropsFile : FiledropsFileSystemEntry
    {
        public FiledropsFile(FiledropsFileSystem fileSystem, string fullName,
                             FiledropsDirectory parent)
        {
            EntryType = FileSystemEntryType.File;
            FileSystem = fileSystem;
            FullName = fullName;
            Parent = parent;
        }

        public FiledropsFile(FiledropsFileSystem fileSystem, string fullName)
            : this(fileSystem, fullName, null) { }

        public FiledropsFile(FiledropsFile file)
            : this(file.FileSystem, file.FullName, file.Parent) { }

		public override string FullName
		{
			get
			{
				return base.FullName;
			}
			set
			{
				base.FullName = value;
				Extension = Path.GetExtension(value);
                NameWithoutExtension = Path.GetFileNameWithoutExtension(value);
			}
		}

        public virtual string Extension { get; set; }

        /// <summary>
        /// The bytes content of this file as it is read from its file system.
        /// </summary>
        private byte[] _BytesContent;
        public byte[] BytesContent 
        {
            get
            {
                return _BytesContent;
            }
            set
            {
                _BytesContent = value;
                _StringContent = Encoding.Default.GetString(value);
            }
        }

        /// <summary>
        /// The bytes content of this file as it is read from its file system.
        /// </summary>
        private string _StringContent;
        public string StringContent
        {
            get
            {
                return _StringContent;
            }
            set
            {
                _StringContent = value;
                _BytesContent = Encoding.Default.GetBytes(value);
            }
        }
        
        /// <summary>
        /// Create this entry in FileSystem at Path.
        /// </summary>
        public virtual void Create(byte[] bytescontent, XmlDocument metaData = null,
                                   bool createDirectoryPath = true) 
        {
            BytesContent = bytescontent;
            MetaData = metaData;
            Create(createDirectoryPath);
        }

        /// <summary>
        /// Create this entry in FileSystem at Path.
        /// </summary>
        public virtual void Create(string stringcontent, XmlDocument metaData = null,
                                   bool createDirectoryPath = true)
        {
            StringContent = stringcontent;
            MetaData = metaData;
            Create(createDirectoryPath);
        }

        /// <summary>
        /// Read this file from its IFiledropsFileSystem.
        /// </summary>
        public abstract void Read();

        /// <summary>
        /// Write this file to the given IFiledropsFileSystem.
        /// </summary>
        public abstract void Create(bool createDirectoryPath = true);
    }
}
