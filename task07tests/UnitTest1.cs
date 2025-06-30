using Xunit;
using task07;
using System.Reflection;

public class AttributeReflectionTests
{
    [Fact]
    public void Class_HasDisplayNameAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = Attribute.GetCustomAttribute(type, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
        Assert.NotNull(attribute);
        Assert.Equal("Пример класса", attribute.DisplayName);
    }

    [Fact]
    public void Method_HasDisplayNameAttribute()
    {
        var method = typeof(SampleClass).GetMethod("TestMethod");
        var attribute = Attribute.GetCustomAttribute(method, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
        Assert.NotNull(attribute);
        Assert.Equal("Тестовый метод", attribute.DisplayName);
    }

    [Fact]
    public void Property_HasDisplayNameAttribute()
    {
        var prop = typeof(SampleClass).GetProperty("Number");
        var attribute = Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
        Assert.NotNull(attribute);
        Assert.Equal("Числовое свойство", attribute.DisplayName);
    }

    [Fact]
    public void Class_HasVersionAttribute()
    {
        var type = typeof(SampleClass);
        var attribute = Attribute.GetCustomAttribute(type, typeof(VersionAttribute)) as VersionAttribute;
        Assert.NotNull(attribute);
        Assert.Equal(1, attribute.Major);
        Assert.Equal(0, attribute.Minor);
    }
}
