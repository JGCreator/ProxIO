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
    public interface IFileInfoProxy
    {
        IDirectoryInfoProxy Directory { get; }
        string DirectoryName { get; }
        bool Exists { get; }
        bool IsReadOnly { get; }
        string Name { get; }
        string FullName { get; }
        IFileStreamProxy Create();
        IStreamWriterProxy CreateText();
        IStreamWriterProxy AppendText();
    }

    public class FileInfoProxy : IFileInfoProxy
    {
        private FileInfo _instance;

        public FileInfoProxy(string fullName)
        {
            _instance = new FileInfo(fullName);
        }

        public IDirectoryInfoProxy Directory => DirectoryInfoProxy.Representing(_instance.Directory);
        public string DirectoryName => _instance.DirectoryName;
        public bool Exists => _instance.Exists;
        public bool IsReadOnly => _instance.IsReadOnly;
        public string Name => _instance.Name;
        public string FullName => _instance.FullName;

        public IFileStreamProxy Create()
        {
            return FileStreamProxy.Representing(_instance.Create());
        }

        public IStreamWriterProxy CreateText()
        {
            return StreamWriterProxy.Representing(_instance.CreateText());
        }

        public IStreamWriterProxy AppendText()
        {
            return StreamWriterProxy.Representing(_instance.AppendText());
        }

    }

    public interface IDirectoryInfoProxy
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
        private bool _dirExists;
        private StreamWriter _streamWriter;
        private DirectoryInfo _instance;

        public static IDirectoryInfoProxy Representing(DirectoryInfo instance)
        {
            return new DirectoryInfoProxy(instance);
        }

        public DirectoryInfoProxy(string path)
        {
            _instance = new DirectoryInfo(path);
        }

        private DirectoryInfoProxy(DirectoryInfo instance)
        {
            _instance = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        public bool Exists => _instance.Exists;
        public string FullName => _instance.FullName;
        public string Name => _instance.Name;
        public IDirectoryInfoProxy Parent => new DirectoryInfoProxy(_instance.Parent);
        public IDirectoryInfoProxy Root => new DirectoryInfoProxy(_instance.Root);

        public void Create()
        {
            _instance.Create();
        }

        public void Create(DirectorySecurity directorySecurity)
        {
            _instance.Create(directorySecurity);
        }
    }

    public interface IStreamWriterProxy
    {

    }

    public class StreamWriterProxy : IStreamWriterProxy, IDisposable
    {
        private StreamWriter _instance;

        public static IStreamWriterProxy Representing(StreamWriter instance)
        {
            return new StreamWriterProxy(instance);
        }

        public StreamWriterProxy(string path)
        {
            _instance = new StreamWriter(path);
        }

        private StreamWriterProxy(StreamWriter instance)
        {
            _instance = instance;
            instance.
        }

        public bool AutoFlush
        {
            get => _instance.AutoFlush;
            set => _instance.AutoFlush = value;
        }

        public void Dispose()
        {
            _instance?.Dispose();
        }
    }

    public interface IFileStreamProxy
    {
        
        void Dispose();
    }

    public class FileStreamProxy : StreamProxy, IFileStreamProxy
    {
        private FileStream _instance;

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
            //instance.IsAsync;
            //instance.SafeFileHandle;
            //instance.GetAccessControl();
            //instance.Lock();
            //instance.SetAccessControl();
            //instance.Unlock();

            _instance = instance;
        }

        public string Name => _instance.Name;
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

    public class StreamProxy : IStreamProxy, IDisposable
    {
        private Stream _instance;

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
            if (streamProxy is StreamProxy proxy)
            {
                _instance.CopyTo(proxy._instance);    
            }

            var interfaceName = typeof(IStreamProxy).Name;
            var className = GetType().Name;
            throw new ArgumentException($"This method is executing on a {className} implementation of the {interfaceName} interface." + 
                                        $"For this implementation the argument object implementing {interfaceName} " + 
                                        $"must be able to take the shape of (via casting, can be explicitly converted to) {className}.");
        }

        #region IDisposable Support
        private bool _disposed = false; // To detect redundant calls

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

    public class Writer
    {

        public void WriteFile(string name, string path, string contents)
        {
            if (!_dirExists)
            {
                throw new InvalidOperationException("The path given has not been validated or is invalid");
            }
        }

        public void WriteFile(string name, string path, Stream contents)
        {
            throw new NotImplementedException();
        }

        private void ValidateDirectory(string path)
        {

        }

        public void Dispose()
        {
            _streamWriter?.Dispose();
        }
    }
}
