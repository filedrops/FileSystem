using System.IO;

namespace Org.Filedrops.FileSystem.Exceptions
{
    public class DirectoryNotEmptyException : IOException
    {
        public DirectoryNotEmptyException(string path)
            : base("Directory is not empty at: " + path) { }
    }
}
