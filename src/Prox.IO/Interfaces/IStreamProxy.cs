using System;
using System.IO;

namespace Prox.IO.Interfaces
{
    public interface IStreamProxy: IProxy<Stream>, IDisposable
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

        void CopyTo(IProxy<Stream> streamProxy);
    }

}
