using System;
using System.Collections.Concurrent;
using System.Threading;

public interface ICommand
{
    void Execute();
}

public class ServerThread
{
    private readonly Thread _thread;
    private readonly ConcurrentQueue<ICommand> _commandQueue = new();
    private volatile bool _isRunning;
    private readonly ManualResetEventSlim _commandAvailable = new();

    public ServerThread(string name)
    {
        _thread = new Thread(Run) { Name = name, IsBackground = true };
    }

    public void Start()
    {
        if (!_thread.IsAlive)
        {
            _isRunning = true;
            _thread.Start();
        }
    }

    public void AddCommand(ICommand command)
    {
        _commandQueue.Enqueue(command);
        _commandAvailable.Set();
    }

    private void Run()
    {
        while (_isRunning)
        {
            if (_commandQueue.TryDequeue(out var cmd))
            {
                cmd.Execute();
                continue;
            }
            _commandAvailable.Wait(10);
            _commandAvailable.Reset();
        }
    }

    public void Stop(bool hardStop)
    {
        if (hardStop)
        {
            _isRunning = false;
            _commandAvailable.Set();
        }
        else
        {
            AddCommand(new SoftStopCommand(this));
        }
    }

    public class HardStopCommand : ICommand
    {
        private readonly ServerThread _server;
        public HardStopCommand(ServerThread server) => _server = server;
        public void Execute() => _server.Stop(true);
    }

    public class SoftStopCommand : ICommand
    {
        private readonly ServerThread _server;
        public SoftStopCommand(ServerThread server) => _server = server;
        public void Execute() => _server.Stop(false);
    }
}
