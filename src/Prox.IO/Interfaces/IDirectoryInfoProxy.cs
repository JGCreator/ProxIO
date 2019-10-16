using System.IO;
using System.Security.AccessControl;

namespace Prox.IO.Interfaces
{
    public interface IDirectoryInfoProxy : IProxy<DirectoryInfo>
    {
        bool Exists { get; }

        string FullName { get; }

        string Name { get; }

        IDirectoryInfoProxy Parent { get; }

        IDirectoryInfoProxy Root { get; }

        void Create();

        void Create(DirectorySecurity directorySecurity);
    }
}