using System;
using System.IO;

namespace Prox.IO.Interfaces
{
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
}