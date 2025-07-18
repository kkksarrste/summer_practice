using System;
using System.Threading;
using Xunit;

public class ServerThreadTests
{
    [Fact]
    public void HardStop_ImmediatelyTerminates()
    {
        var server = new ServerThread("Test");
        bool executed = false;
        
        server.AddCommand(new TestCommand(() => {
            Thread.Sleep(500);
            executed = true;
        }));
        server.AddCommand(new ServerThread.HardStopCommand(server));
        server.Start();
        
        Thread.Sleep(100);
        Assert.False(executed);
    }

    [Fact]
    public void SoftStop_WaitsForQueue()
    {
        var server = new ServerThread("Test");
        bool executed = false;
        
        server.AddCommand(new TestCommand(() => executed = true));
        server.AddCommand(new ServerThread.SoftStopCommand(server));
        server.Start();
        
        Thread.Sleep(100);
        Assert.True(executed);
    }

    private class TestCommand : ICommand
    {
        private readonly Action _action;
        public TestCommand(Action action) => _action = action;
        public void Execute() => _action();
    }
}
