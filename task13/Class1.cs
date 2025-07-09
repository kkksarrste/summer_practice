using System.Text.Json;
using System.Text.Json.Serialization;

public class Subject
{
    public required string Name { get; set; }
    public int Grade { get; set; }
}

public class Student
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Subject> Grades { get; set; } = new();
}

public static class StudentSerializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        Converters = { new DateTimeConverter() }
    };

    public static string ToJson(Student s) => JsonSerializer.Serialize(s, _options);
    public static Student? FromJson(string json) => JsonSerializer.Deserialize<Student>(json, _options);
    public static void SaveToFile(Student s, string path) => File.WriteAllText(path, ToJson(s));
    public static Student? LoadFromFile(string path) => FromJson(File.ReadAllText(path));
}

public class DateTimeConverter : JsonConverter<DateTime>  
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();
        if (string.IsNullOrEmpty(dateString))
            throw new JsonException("error");
            
        return DateTime.Parse(dateString);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
}
