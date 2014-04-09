using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Org.Filedrops.FileSystem
{
    [Serializable()]
    public abstract class FiledropsDirectory : FiledropsFileSystemEntry
    {
        public virtual BitmapImage OpenIcon16x16 { get { return null; } }
        public virtual BitmapImage OpenIcon24x24 { get { return null; } }
        public virtual BitmapImage OpenIcon32x32 { get { return null; } }
        public virtual BitmapImage OpenIcon48x48 { get { return null; } }
        public virtual BitmapImage OpenIcon64x64 { get { return null; } }
        public virtual BitmapImage OpenIcon128x128 { get { return null; } }
        public virtual BitmapImage OpenIcon256x256 { get { return null; } }

        public FiledropsDirectory(FiledropsFileSystem fileSystem,
                                  string fullName, FiledropsDirectory parent)
        {
            EntryType = FileSystemEntryType.Folder;
            FileSystem = fileSystem;
            FullName = fullName;
            Parent = parent;
        }

        public FiledropsDirectory(FiledropsFileSystem fileSystem, string fullName)
            : this(fileSystem, fullName, null) { }

        public FiledropsDirectory(FiledropsDirectory directory)
            : this(directory.FileSystem, directory.FullName, directory.Parent) { }

        /// <summary>
        /// Create this entry in FileSystem at Path.
        /// </summary>
        public abstract void Create();

        /// <summary>
        /// Delete this entry from FileSystem.
        /// </summary>
        public abstract void Delete(bool recursive = false);

        public override void Delete()
        {
            Delete(false);
        }

        /// <summary>
        /// Return all files found in this directory. The returned IFiledropsFiles
        /// must have their Path property set to a relative path to a file in
        /// this directory.
        /// </summary>
        public abstract List<FiledropsFile> GetFiles(SearchOption sOption = SearchOption.TopDirectoryOnly);
		public abstract List<FiledropsFile> GetFiles(string filter, SearchOption sOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Return all files found in this directory. The returned IFiledropsFiles
        /// must have their Path property set to a relative path to a file in
        /// this directory. Only the files which have been modified after a
        /// given date are returned.
        /// </summary>
		public virtual List<FiledropsFile> GetFiles(DateTime modifiedAfter, SearchOption sOption = SearchOption.TopDirectoryOnly)
        {
            List<FiledropsFile> files = GetFiles(sOption);
            files.RemoveAll(file => WasModifiedEarlier(file, modifiedAfter));
            return files;
        }
		public virtual List<FiledropsFile> GetFiles(DateTime modifiedAfter, string filter, SearchOption sOption = SearchOption.TopDirectoryOnly)
		{
			List<FiledropsFile> files = GetFiles(filter, sOption);
			files.RemoveAll(file => WasModifiedEarlier(file, modifiedAfter));
			return files;
		}

        public virtual List<FiledropsFileSystemEntry> GetEntries(SearchOption sOption = SearchOption.TopDirectoryOnly)
        {
            List<FiledropsFileSystemEntry> list = GetFiles(sOption).Cast<FiledropsFileSystemEntry>().ToList();
            list.AddRange(GetDirectories(sOption));
            return list;
        }

		public virtual List<FiledropsFileSystemEntry> GetEntries(DateTime modifiedAfter, SearchOption sOption = SearchOption.TopDirectoryOnly)
        {
            List<FiledropsFileSystemEntry> files = GetEntries(sOption);
            files.RemoveAll(file => WasModifiedEarlier(file, modifiedAfter));
            return files;
        }

		public abstract List<FiledropsDirectory> GetDirectories(SearchOption sOption = SearchOption.TopDirectoryOnly);

		public virtual List<FiledropsDirectory> GetDirectories(DateTime modifiedAfter, SearchOption sOption = SearchOption.TopDirectoryOnly)
        {
            List<FiledropsDirectory> files = GetDirectories(sOption);
            files.RemoveAll(file => WasModifiedEarlier(file, modifiedAfter));
            return files;
        }

        protected bool WasModifiedEarlier(FiledropsFileSystemEntry entry, DateTime modifiedAfter)
        {
            return modifiedAfter.CompareTo(entry.LastModified) > 0;
        }
	}
}
