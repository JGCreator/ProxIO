using System;
using System.IO;
using System.Text;

namespace ComplianceTool.Common.ProxIO.Interfaces
{
    public interface IStreamWriterProxy : IProxy<StreamWriter>, IDisposable
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
        void Write(object value);
    }
}