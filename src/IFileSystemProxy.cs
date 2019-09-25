using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Common;

namespace ComplianceTool.Common.Interfaces
{
    public interface IProxy<out TClientType>
    {
        TClientType Client { get; }
    }

    public interface IFileInfoProxy : IProxy<FileInfo>
    {
        IDirectoryInfoProxy Directory { get; }

        string DirectoryName { get; }

        bool Exists { get; }

        bool IsReadOnly { get; }

        string Name { get; }

        string FullName { get; }

        DateTime CreationTime { get; }

        string Extension { get; }

        FileAttributes Attributes { get; set; }

        IFileStreamProxy Create();

        IStreamWriterProxy CreateText();

        IStreamWriterProxy AppendText();

        void CopyTo(string destFileName);

        void CopyTo(string destFileName, bool overwrite);

        IFileStreamProxy Open(FileMode mode);

        IFileStreamProxy Open(FileMode mode, FileAccess access);

        IFileStreamProxy Open(FileMode mode, FileAccess access, FileShare share);
    }

    public class FileInfoProxy : IFileInfoProxy
    {
        public static FileInfoProxy Representing(FileInfo instance)
        {
            return new FileInfoProxy(instance);
        }

        public FileInfoProxy(string fullName)
        {
            Client = new FileInfo(fullName);
        }

        private FileInfoProxy(FileInfo instance)
        {
            Client = instance;
        }

        public IDirectoryInfoProxy Directory => DirectoryInfoProxy.Representing(Client.Directory);

        public string DirectoryName => Client.DirectoryName;

        public bool Exists => Client.Exists;

        public bool IsReadOnly => Client.IsReadOnly;

        public string Name => Client.Name;

        public string FullName => Client.FullName;

        public DateTime CreationTime => Client.CreationTime;

        public string Extension => Client.Extension;

        public FileInfo Client { get; }

        public FileAttributes Attributes
        {
            get => Client.Attributes;
            set => Client.Attributes = value;
        }

        public IFileStreamProxy Create()
        {
            return FileStreamProxy.Representing(Client.Create());
        }

        public IStreamWriterProxy CreateText()
        {
            return StreamWriterProxy.Representing(Client.CreateText());
        }

        public IStreamWriterProxy AppendText()
        {
            return StreamWriterProxy.Representing(Client.AppendText());
        }

        public void CopyTo(string destFileName)
        {
            Client.CopyTo(destFileName);
        }

        public void CopyTo(string destFileName, bool overwrite)
        {
            Client.CopyTo(destFileName, overwrite);
        }

        public IFileStreamProxy Open(FileMode mode)
        {
            return FileStreamProxy.Representing(Client.Open(mode));
        }

        public IFileStreamProxy Open(FileMode mode, FileAccess access)
        {
            return FileStreamProxy.Representing(Client.Open(mode, access));
        }

        public IFileStreamProxy Open(FileMode mode, FileAccess access, FileShare share)
        {
            return FileStreamProxy.Representing(Client.Open(mode, access, share));
        }
    }

    public interface IDirectoryInfoProxy : IProxy<DirectoryInfo>
    {
        bool Exists { get; }

        string FullName { get; }

        string Name { get; }

        IDirectoryInfoProxy Parent { get; }

        IDirectoryInfoProxy Root { get; }

        void Create();

        void Create(DirectorySecurity directorySecurity);
    }

    public class DirectoryInfoProxy : IDirectoryInfoProxy
    {
        public static IDirectoryInfoProxy Representing(DirectoryInfo instance)
        {
            return new DirectoryInfoProxy(instance);
        }

        public DirectoryInfoProxy(string path)
        {
            Client = new DirectoryInfo(path);
        }

        private DirectoryInfoProxy(DirectoryInfo instance)
        {
            Client = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        public bool Exists => Client.Exists;

        public string FullName => Client.FullName;

        public string Name => Client.Name;

        public IDirectoryInfoProxy Parent => new DirectoryInfoProxy(Client.Parent);

        public IDirectoryInfoProxy Root => new DirectoryInfoProxy(Client.Root);

        public DirectoryInfo Client { get; }

        public void Create()
        {
            Client.Create();
        }

        public void Create(DirectorySecurity directorySecurity)
        {
            Client.Create(directorySecurity);
        }
    }

    public interface IStreamWriterProxy : IProxy<StreamWriter>
    {
        string NewLine { get; }

        Encoding Encoding { get; }

        IStreamProxy BaseStream { get; }

        bool AutoFlush { get; set; }

        void Close();

        void Flush();

        void Write(char[] buffer, int index, int count);

        void Write(double value);

        void Write(char[] value);

        void Write(bool value);

        void Write(char value);

        void WriteLine();

        void WriteLine(bool value);

        void WriteLine(char value);

        void WriteLine(char[] value);

        void WriteLine(char[] buffer, int index, int count);
    }

    public class StreamWriterProxy : IStreamWriterProxy, IDisposable
    {
        public static IStreamWriterProxy Representing(StreamWriter instance)
        {
            return new StreamWriterProxy(instance);
        }

        public StreamWriterProxy(string path)
        {
            Client = new StreamWriter(path);
        }

        private StreamWriterProxy(StreamWriter instance)
        {
            Client = instance;
        }

        public string NewLine => Client.NewLine;

        public Encoding Encoding => Client.Encoding;

        public IStreamProxy BaseStream => StreamProxy.Representing(Client.BaseStream);
        
        public void Close()
        {
            Client.Close();
        }

        public void Flush()
        {
            Client.Flush();
        }

        public void Write(char[] buffer, int index, int count)
        {
            Client.Write(buffer, index, count);
        }

        public void Write(double value)
        {
            Client.Write(value);
        }

        public void Write(char[] value)
        {
            Client.Write(value);
        }

        public void Write(bool value)
        {
            Client.Write(value);
        }

        public void Write(char value)
        {
            Client.Write(value);
        }

        public void WriteLine()
        {
            Client.WriteLine();
        }

        public void WriteLine(bool value)
        {
            Client.WriteLine(value);
        }

        public void WriteLine(char value)
        {
            Client.WriteLine(value);
        }

        public void WriteLine(char[] value)
        {
            Client.WriteLine(value);
        }

        public void WriteLine(char[] buffer, int index, int count)
        {
            Client.WriteLine(buffer, index, count);
        }

        public StreamWriter Client { get; }

        public bool AutoFlush
        {
            get => Client.AutoFlush;
            set => Client.AutoFlush = value;
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }

            Client?.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }

    public interface IFileStreamProxy : IProxy<FileStream>
    {
        string Name { get; }

        bool IsAsync { get; }

        ISafeFileHandleProxy SafeFileHandle { get; }

        IFileSecurityProxy GetAccessControl();

        void Lock(long position, long length);

        void SetAccessControl(IFileSecurityProxy fileSecurity);

        void Unlock(long position, long length);
    }

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
        
        public override void CopyTo(IStreamProxy streamProxy)
        {
            if (streamProxy is IProxy<FileStream> proxy)
            {
                Client.CopyTo(proxy.Client);
            }

            var interfaceName = typeof(IStreamProxy).Name;
            var className = GetType().Name;
            throw new ArgumentException($"This method is executing on a {className} implementation of the {interfaceName} interface." +
                                        $"For this implementation the argument object implementing {interfaceName} " +
                                        $"must be able to take the shape of (via casting, can be explicitly converted to) {className}.");
        }
    }

    public interface IFileSecurityProxy : IProxy<FileSecurity>
    {

    }

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

    public interface ISafeFileHandleProxy : IProxy<SafeFileHandle>
    {
        bool IsInvalid { get; }

        bool IsClosed { get; }

        void Close();

        void DangerousAddRef(ref bool success);

        void DangerousGetHandle();

        void DangerousRelease();

        void SetHandleAsInvalid();

    }

    public class SafeFileHandleProxy : ISafeFileHandleProxy, IDisposable
    {
        public static SafeFileHandleProxy Representing(SafeFileHandle instance)
        {
            return new SafeFileHandleProxy(instance);
        }

        public void Dispose()
        {
            Client.Dispose();
        }

        public SafeFileHandleProxy(IntPtr preexistingHandle, bool ownsHandle)
        {
            Client = new SafeFileHandle(preexistingHandle, ownsHandle);
        }

        private SafeFileHandleProxy(SafeFileHandle instance)
        {
            Client = instance;
        }

        public bool IsInvalid => Client.IsInvalid;

        public bool IsClosed => Client.IsClosed;

        public SafeFileHandle Client { get; }

        public void Close()
        {
            Client.Close();
        }

        public void DangerousAddRef(ref bool success)
        {
            Client.DangerousAddRef(ref success);
        }

        public void DangerousGetHandle()
        {
            Client.DangerousGetHandle();
        }

        public void DangerousRelease()
        {
            Client.DangerousRelease();
        }

        public void SetHandleAsInvalid()
        {
            Client.SetHandleAsInvalid();
        }
    }

    public interface IStreamProxy
    {
        bool CanRead { get; }

        bool CanWrite { get; }

        bool CanSeek { get; }

        long Length { get; }

        bool CanTimeout { get; }

        long Position { get; }

        int ReadTimeout { get; }

        int WriteTimeout { get; }

        void Flush();

        int Read(byte[] buffer, int offset, int count);

        void Write(byte[] buffer, int offset, int count);

        long Seek(long offset, SeekOrigin origin);

        void Close();

        void CopyTo(IStreamProxy streamProxy);
    }

    public class StreamProxy : IStreamProxy, IProxy<Stream>, IDisposable
    {
        public static IStreamProxy Representing(Stream instance)
        {
            return new StreamProxy(instance);
        }

        protected StreamProxy(Stream instance)
        {
            _instance = instance;
        }
        
        public bool CanRead => _instance.CanRead;

        public bool CanWrite => _instance.CanWrite;

        public bool CanSeek => _instance.CanSeek;

        public long Length => _instance.Length;

        public bool CanTimeout => _instance.CanTimeout;

        public long Position => _instance.Position;

        public int ReadTimeout => _instance.ReadTimeout;

        public int WriteTimeout => _instance.WriteTimeout;

        Stream IProxy<Stream>.Client => _instance;

        public virtual void Flush()
        {
            _instance.Flush();
        }

        public virtual int Read(byte[] buffer, int offset, int count)
        {
            return _instance.Read(buffer, offset, count);
        }

        public virtual void Write(byte[] buffer, int offset, int count)
        {
            _instance.Write(buffer, offset, count);
        }

        public virtual long Seek(long offset, SeekOrigin origin)
        {
            return _instance.Seek(offset, origin);
        }

        public virtual void Close()
        {
            _instance.Close();
        }

        public virtual void CopyTo(IStreamProxy streamProxy)
        {
            if (streamProxy is IProxy<Stream> proxy)
            {
                _instance.CopyTo(proxy.Client);    
            }

            var interfaceName = typeof(IStreamProxy).Name;
            var className = GetType().Name;
            throw new ArgumentException($"This method is executing on a {className} implementation of the {interfaceName} interface." + 
                                        $"For this implementation the argument object implementing {interfaceName} " + 
                                        $"must be able to take the shape of (via casting, can be explicitly converted to) {className}.");
        }

        #region IDisposable Support
        private bool _disposed = false; // To detect redundant calls
        private Stream _instance;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }

            _instance?.Dispose();
            _instance = null;
            _disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
