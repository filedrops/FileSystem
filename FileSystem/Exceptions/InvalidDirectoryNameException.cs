using System.IO;

namespace Org.Filedrops.FileSystem.Exceptions
{
    public class InvalidDirectoryNameException : IOException
    {
        public InvalidDirectoryNameException(string name)
            : base("Invalid directory name: " + name) { }
    }
}
