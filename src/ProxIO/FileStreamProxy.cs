using System.IO;
using ProxIO.Interfaces;

namespace ProxIO
{
    public class FileStreamProxy : StreamProxy, IFileStreamProxy
    {
        public static IFileStreamProxy Representing(FileStream instance)
        {
            return new FileStreamProxy(instance);
        }

        public FileStreamProxy(string path, FileMode fileMode)
            : this(new FileStream(path, fileMode))
        {

        }

        private FileStreamProxy(FileStream instance)
            : base(instance)
        {
            Client = instance;
        }

        public string Name => Client.Name;

        public bool IsAsync => Client.IsAsync;

        public FileStream Client { get; }

        public ISafeFileHandleProxy SafeFileHandle => SafeFileHandleProxy.Representing(Client.SafeFileHandle);

        public IFileSecurityProxy GetAccessControl()
        {
            return FileSecurityProxy.Representing(Client.GetAccessControl());
        }

        public void Lock(long position, long length)
        {
            Client.Lock(position, length);
        }

        public void SetAccessControl(IFileSecurityProxy fileSecurity)
        {
            Client.SetAccessControl(fileSecurity.Client);
        }

        public void Unlock(long position, long length)
        {
            Client.Unlock(position, length);
        }

        //public override void CopyTo(IProxy<Stream> proxy)
        //{
        //    Client.CopyTo(proxy.Client);

        //    var interfaceName = typeof(IStreamProxy).Name;
        //    var className = GetType().Name;
        //    throw new ArgumentException($"This method is executing on a {className} implementation of the {interfaceName} interface." +
        //                                $"For this implementation the argument object implementing {interfaceName} " +
        //                                $"must be able to take the shape of (via casting, can be explicitly converted to) {className}.");
        //}
    }
}