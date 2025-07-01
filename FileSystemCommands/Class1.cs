using CommandLib;
using System.IO;

namespace FileSystemCommands
{
    public class DirectorySizeCommand : ICommand
    {
        public long Size { get; private set; }
        private readonly string _path;

        public DirectorySizeCommand(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Directory {path} not found");
            _path = path;
        }

        public void Execute()
        {
            Size = 0;
            foreach (var file in Directory.GetFiles(_path, "*", SearchOption.AllDirectories))
                Size += new FileInfo(file).Length;
        }
    }

    public class FindFilesCommand : ICommand
    {
        public string[] FoundFiles { get; private set; }
        private readonly string _path;
        private readonly string _mask;

        public FindFilesCommand(string path, string mask)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Directory {path} not found");
            _path = path;
            _mask = mask;
        }

        public void Execute()
        {
            FoundFiles = Directory.GetFiles(_path, _mask);
        }
    }
}
