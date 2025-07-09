using Xunit;

public class UnitTest1
{
    [Fact]
    public void TestLinearFunction()
    {
        var X = (double x) => x;
        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2), 1e-4);
    }

    [Fact]
    public void TestSinFunction()
    {
        var SIN = (double x) => Math.Sin(x);
        Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8), 1e-4);
    }

    [Fact]
    public void TestAnotherLinearCase()
    {
        var X = (double x) => x;
        Assert.Equal(10, DefiniteIntegral.Solve(0, 5, X, 1e-6, 8), 1e-5);
    }
}
