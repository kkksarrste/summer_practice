using Xunit;

var X = (double x) => x;
var SIN = (double x) => Math.Sin(x);

Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2), 1e-4);
Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8), 1e-4);
Assert.Equal(10, DefiniteIntegral.Solve(0, 5, X, 1e-6, 8), 1e-5);
