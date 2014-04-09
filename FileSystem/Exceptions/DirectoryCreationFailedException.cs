using System;

namespace Org.Filedrops.FileSystem.Exceptions
{
    public class DirectoryCreationFailedException : Exception
    {
        public DirectoryCreationFailedException(string path)
            : base("Failed to create directory at: " + path) { }
    }
}
