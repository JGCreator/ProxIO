using System;
using Prox.IO.Interfaces;
using Microsoft.Win32.SafeHandles;

namespace Prox.IO
{
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
}