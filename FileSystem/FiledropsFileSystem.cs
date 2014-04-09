using System;
using System.Windows.Media.Imaging;
using System.Xml;


namespace Org.Filedrops.FileSystem
{
    public delegate BitmapImage FileIconProvider(FiledropsFile f, int size);
    public delegate BitmapImage OpenDirIconProvider(FiledropsDirectory d, int size);
    public delegate BitmapImage ClosedDirIconProvider(FiledropsDirectory d, int size);

    /// <summary>
    /// Represents a local or remote file system.
    /// </summary>
    [Serializable()]
    public abstract class FiledropsFileSystem
    {

        public FileIconProvider GetFileIcon{ get; set;}

        public OpenDirIconProvider GetOpenDirIcon { get; set;}

        public ClosedDirIconProvider GetClosedDirIcon { get; set; }

        public abstract FiledropsDirectory WorkingDirectory { get; set; }

        public abstract XmlDocument GetMetaDataDefinition();

        public abstract FiledropsFile ConstructFile(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderpath">path of parent dir with including slash</param>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public FiledropsFile ConstructFileRecursive(string folderpath, string name, string extension)
        {
            return createFileRecur(1, folderpath, name, extension);
        }

        private FiledropsFile createFileRecur(int i, string path, string name, string extension)
        {
            FiledropsFile file;
            file = ConstructFile(path + name + i + "." + extension);
            if (file.Exists())
            {
                return createFileRecur(++i, path, name, extension);
            }
            else
            {
                file.Create();
                return file;
            }                
        }

        public abstract FiledropsDirectory ConstructDirectory(string path);

        public FiledropsDirectory ConstructDirectoryRecursive(string path)
        {
            return createFolderRecur(1, path);
        }

        private FiledropsDirectory createFolderRecur(int i, string path)
        {
            FiledropsDirectory folder;
            folder = ConstructDirectory(path + i);
            if (folder.Exists())
            {
                return createFolderRecur(++i, path);
            }
            else
            {
                folder.Create();
                return folder;
            }
        }
    }
}
