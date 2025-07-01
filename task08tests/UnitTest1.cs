using CommandLib;
using FileSystemCommands;
using System;
using System.IO;
using Xunit;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var dir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(dir);
        File.WriteAllText(Path.Combine(dir, "t1.txt"), "test");
        
        var command = new DirectorySizeCommand(dir);
        command.Execute();
        
        Assert.True(command.Size > 0);
        Directory.Delete(dir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var dir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(dir);
        File.WriteAllText(Path.Combine(dir, "f1.txt"), "test");
        
        var command = new FindFilesCommand(dir, "*.txt");
        command.Execute();
        
        Assert.Single(command.FoundFiles);
        Directory.Delete(dir, true);
    }

    [Fact]
    public void ShouldThrowWhenDirectoryNotExists()
    {
        var invalidPath = Path.Combine(Path.GetTempPath(), "NonExistingDir");
        
        Assert.Throws<DirectoryNotFoundException>(() => new DirectorySizeCommand(invalidPath));
        Assert.Throws<DirectoryNotFoundException>(() => new FindFilesCommand(invalidPath, "*.*"));
    }

    [Fact]
    public void FindFiles_ShouldReturnEmptyWhenNoMatches()
    {
        var dir = Path.Combine(Path.GetTempPath(), "EmptyDir");
        Directory.CreateDirectory(dir);
        
        var command = new FindFilesCommand(dir, "*.mp3");
        command.Execute();
        
        Assert.Empty(command.FoundFiles);
        Directory.Delete(dir, true);
    }
}
