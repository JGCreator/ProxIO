using System.IO;
using ComplianceTool.Common.ProxIO.Interfaces;

namespace ComplianceTool.Common.ProxIO
{
    public class StreamProxy : IStreamProxy
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

        public virtual void CopyTo(IProxy<Stream> proxy)
        {
            _instance.CopyTo(proxy.Client);
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