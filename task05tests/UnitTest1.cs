using Xunit;

public class TestClass
{
    public int PublicField;
    private string _privateField;
    public int Property { get; set; }

    public void Method() { }
    public int MethodWithParams(int a, string b) => 0;
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods();

        Assert.Contains("Method", methods);
        Assert.Contains("MethodWithParams", methods);
        Assert.Contains("get_Property", methods);
        Assert.Contains("set_Property", methods);
    }

    [Fact]
    public void GetMethodParams_ReturnsCorrectParameters()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var parameters = analyzer.GetMethodParams("MethodWithParams");

        Assert.Contains("Int32 a", parameters);
        Assert.Contains("String b", parameters);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields();

        Assert.Contains("PublicField", fields);
        Assert.Contains("_privateField", fields);
    }

    [Fact]
    public void GetProperties_ReturnsCorrectProperties()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var properties = analyzer.GetProperties();

        Assert.Contains("Property", properties);
    }

    [Fact]
    public void HasAttribute_ReturnsTrueForClassWithAttribute()
    {
        var analyzer = new ClassAnalyzer(typeof(AttributedClass));
        Assert.True(analyzer.HasAttribute<SerializableAttribute>());
    }

    [Fact]
    public void HasAttribute_ReturnsFalseForClassWithoutAttribute()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        Assert.False(analyzer.HasAttribute<SerializableAttribute>());
    }
}
