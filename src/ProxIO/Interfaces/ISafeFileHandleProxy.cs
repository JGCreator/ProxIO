using Microsoft.Win32.SafeHandles;

namespace ProxIO.Interfaces
{
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
}