using CommandLib;
using System;
using System.Reflection;

namespace CommandRunner
{
    class Program
    {
        static void Main()
        {
            var assembly = Assembly.LoadFrom("FileSystemCommands.dll");
            
            var directorySizeCommand = CreateCommand(assembly, "DirectorySizeCommand", @"C:\Temp");
            var findFilesCommand = CreateCommand(assembly, "FindFilesCommand", @"C:\Temp", "*.txt");
            
            directorySizeCommand?.Execute();
            findFilesCommand?.Execute();
        }

        private static ICommand CreateCommand(Assembly assembly, string commandName, params object[] parameters)
        {
            var commandType = assembly.GetType($"FileSystemCommands.{commandName}");
            return (ICommand)Activator.CreateInstance(commandType, parameters);
        }
    }
}
