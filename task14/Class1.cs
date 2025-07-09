using System;
using System.Threading;

public class DefiniteIntegral
{
    private class IntegralData
    {
        public double Start { get; set; }
        public double End { get; set; }
        public Func<double, double> Function { get; set; } = x => x;
        public double Step { get; set; }
        public Barrier Barrier { get; set; } = new Barrier(1);
    }

    private static double sharedResult;
    private static readonly object lockObj = new object();
    
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
    {
        if (threadsNumber < 1) threadsNumber = 1;
        if (step <= 0) step = 1e-5;
        
        double segmentLength = (b - a) / threadsNumber;
        sharedResult = 0;
        
        Thread[] threads = new Thread[threadsNumber];
        Barrier barrier = new Barrier(threadsNumber + 1);
        
        for (int i = 0; i < threadsNumber; i++)
        {
            double start = a + i * segmentLength;
            double end = (i == threadsNumber - 1) ? b : start + segmentLength;
            
            IntegralData data = new IntegralData
            {
                Start = start,
                End = end,
                Function = function,
                Step = step,
                Barrier = barrier
            };
            
            threads[i] = new Thread(CalculatePartialIntegral);
            threads[i].Start(data);
        }
        
        barrier.SignalAndWait();
        
        foreach (var thread in threads)
        {
            thread.Join();
        }
        
        return sharedResult;
    }
    
    private static void CalculatePartialIntegral(object? data)
    {
        if (data is not IntegralData integralData) return;
        
        double a = integralData.Start;
        double b = integralData.End;
        Func<double, double> function = integralData.Function;
        double step = integralData.Step;
        
        double sum = 0;
        double x = a;
        
        while (x < b)
        {
            double nextX = Math.Min(x + step, b);
            double y1 = function(x);
            double y2 = function(nextX);
            sum += (y1 + y2) * (nextX - x) / 2;
            x = nextX;
        }
        
        lock (lockObj)
        {
            sharedResult += sum;
        }
        
        integralData.Barrier.SignalAndWait();
    }
}
