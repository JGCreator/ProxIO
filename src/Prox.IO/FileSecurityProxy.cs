using System.Security.AccessControl;
using Prox.IO.Interfaces;

namespace Prox.IO
{
    public class FileSecurityProxy : IFileSecurityProxy
    {
        public static FileSecurityProxy Representing(FileSecurity instance)
        {
            return new FileSecurityProxy(instance);
        }

        public FileSecurityProxy()
        {
            Client = new FileSecurity();
        }

        public FileSecurityProxy(string fileName, AccessControlSections includeSections)
        {
            Client = new FileSecurity(fileName, includeSections);
        }

        private FileSecurityProxy(FileSecurity instance)
        {
            Client = instance;
        }

        public FileSecurity Client { get; }
    }
}