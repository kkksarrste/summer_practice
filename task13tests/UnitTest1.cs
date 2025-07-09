using System;
using System.Text.Json;
using Xunit;

public class StudentJsonShortTests
{
    private readonly Student _testStudent = new()
    {
        FirstName = "A",
        LastName = "B",
        BirthDate = new DateTime(2000, 1, 1),
        Grades = new List<Subject> { new() { Name = "Math", Grade = 5 } }
    };

    private readonly JsonSerializerOptions _options = new()
    {
        Converters = { new DateTimeConverter() }
    };

    [Fact]
    public void Serialize_KeepsData() 
    {
        string json = JsonSerializer.Serialize(_testStudent, _options);
        Assert.Contains("A", json);
        Assert.Contains("Math", json);
    }

    [Fact]
    public void Deserialize_RestoresObject()
    {
        string json = JsonSerializer.Serialize(_testStudent, _options);
        var restored = JsonSerializer.Deserialize<Student>(json, _options);
        
        Assert.Equal(_testStudent.FirstName, restored.FirstName);
        Assert.Equal(_testStudent.Grades[0].Name, restored.Grades[0].Name);
    }

    [Fact]
    public void File_Works()
    {
        string path = "test.json";
        File.WriteAllText(path, JsonSerializer.Serialize(_testStudent, _options));
        var fromFile = JsonSerializer.Deserialize<Student>(File.ReadAllText(path), _options);
        
        Assert.Equal(_testStudent.BirthDate, fromFile.BirthDate);
        File.Delete(path); 
    }
}
