using System;
using System.IO;
using System.Security.AccessControl;
using ComplianceTool.Common.Interfaces;
using ComplianceTool.Common.ProxIO.Interfaces;

namespace ComplianceTool.Common.ProxIO
{
    public class DirectoryInfoProxy : IDirectoryInfoProxy
    {
        public static IDirectoryInfoProxy Representing(DirectoryInfo instance)
        {
            return new DirectoryInfoProxy(instance);
        }

        public DirectoryInfoProxy(string path)
        {
            Client = new DirectoryInfo(path);
        }

        private DirectoryInfoProxy(DirectoryInfo instance)
        {
            Client = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        public bool Exists => Client.Exists;

        public string FullName => Client.FullName;

        public string Name => Client.Name;

        public IDirectoryInfoProxy Parent => new DirectoryInfoProxy(Client.Parent);

        public IDirectoryInfoProxy Root => new DirectoryInfoProxy(Client.Root);

        public DirectoryInfo Client { get; }

        public void Create()
        {
            Client.Create();
        }

        public void Create(DirectorySecurity directorySecurity)
        {
            Client.Create(directorySecurity);
        }
    }
}