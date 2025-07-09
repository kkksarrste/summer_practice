using System.Text.Json;
using System.Text.Json.Serialization;

public class Subject
{
    public string Name { get; set; }
    public int Grade { get; set; }
}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Subject> Grades { get; set; } = new();
}

public static class StudentSerializer
{
    private static JsonSerializerOptions _options = new()
    {
        Converters = { new DateTimeConverter() }
    };

    public static string ToJson(Student s) => JsonSerializer.Serialize(s, _options);
    public static Student FromJson(string json) => JsonSerializer.Deserialize<Student>(json, _options);
    public static void SaveToFile(Student s, string path) => File.WriteAllText(path, ToJson(s));
    public static Student LoadFromFile(string path) => FromJson(File.ReadAllText(path));
}

class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader r, Type t, JsonSerializerOptions o) 
        => DateTime.Parse(r.GetString());

    public override void Write(Utf8JsonWriter w, DateTime d, JsonSerializerOptions o) 
        => w.WriteStringValue(d.ToString("yyyy-MM-dd"));
}
