using System.IO;
using System.Text;
using Prox.IO.Interfaces;

namespace Prox.IO
{
    public class StreamWriterProxy : IStreamWriterProxy
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

        public void Write(object value)
        {
            Client.Write(value);
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
}