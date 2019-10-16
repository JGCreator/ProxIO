using System;
using System.IO;
using ProxIO.Interfaces;

namespace ProxIO
{
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
            return StreamWriterProxy.Representing(Client.Exists 
                                                      ? new StreamWriter(Client.Open(FileMode.Truncate, FileAccess.ReadWrite)) 
                                                      : Client.CreateText());
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
}