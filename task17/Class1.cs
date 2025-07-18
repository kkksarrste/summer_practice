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
    private bool _stopRequested;

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

    public void AddCommand(ICommand command) => _commandQueue.Enqueue(command);

    private void Run()
    {
        while (_isRunning && !_stopRequested)
        {
            if (_commandQueue.TryDequeue(out var cmd))
            {
                cmd.Execute();
            }
            else 
            {
                Thread.Sleep(10);
            }
        }
        _isRunning = false;
    }

    public void Stop(bool hardStop)
    {
        if (hardStop)
        {
            _stopRequested = true;
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
