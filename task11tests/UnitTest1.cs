using Xunit;

public class CalculatorTests
{
    [Fact]
    public void Add_ReturnsCorrectSum()
    {
        var calculator = CalculatorFactory.CreateCalculator();
        Assert.Equal(5, calculator.Add(2, 3));
    }

    [Fact]
    public void Subtract_ReturnsCorrectDifference()
    {
        var calculator = CalculatorFactory.CreateCalculator();
        Assert.Equal(3, calculator.Subtract(5, 2));
    }

    [Fact]
    public void Multiply_ReturnsCorrectProduct()
    {
        var calculator = CalculatorFactory.CreateCalculator();
        Assert.Equal(12, calculator.Multiply(3, 4));
    }

    [Fact]
    public void Divide_ReturnsCorrectQuotient()
    {
        var calculator = CalculatorFactory.CreateCalculator();
        Assert.Equal(4, calculator.Divide(8, 2));
    }

    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        var calculator = CalculatorFactory.CreateCalculator();
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(5, 0));
    }
}
