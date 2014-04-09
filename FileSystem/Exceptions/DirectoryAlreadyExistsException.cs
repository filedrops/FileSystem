using System;

namespace Org.Filedrops.FileSystem.Exceptions
{
    public class DirectoryAlreadyExistsException : Exception
    {
        public DirectoryAlreadyExistsException(string path)
            : base("Directory already exists at: " + path) { }
    }
}
