using System;
using System.IO;

namespace ComplianceTool.Common.ProxIO.Interfaces
{
    public interface IFileStreamProxy : IProxy<FileStream>, IDisposable
    {
        string Name { get; }

        bool IsAsync { get; }

        ISafeFileHandleProxy SafeFileHandle { get; }

        IFileSecurityProxy GetAccessControl();

        void Lock(long position, long length);

        void SetAccessControl(IFileSecurityProxy fileSecurity);

        void Unlock(long position, long length);
    }
}