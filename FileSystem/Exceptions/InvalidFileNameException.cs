using System.IO;

namespace Org.Filedrops.FileSystem.Exceptions
{
    public class InvalidFileNameException : IOException
    {
        public InvalidFileNameException(string name)
            : base("Invalid file name: " + name) { }
    }
}
