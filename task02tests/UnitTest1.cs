using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string Name { get; set; }
    public string Faculty { get; set; }
    public List<int> Grades { get; set; }
}

public class StudentService
{
    private readonly List<Student> _students;

    public StudentService(List<Student> students) 
        => _students = students ?? new List<Student>();

    public IEnumerable<Student> GetStudentsByFaculty(string faculty) 
        => _students.Where(s => s.Faculty == faculty);

    public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minAverageGrade)
        => _students
            .Where(s => s.Grades?.Any() == true)
            .Where(s => s.Grades.Average() >= minAverageGrade)
            .OrderByDescending(s => s.Grades.Average())
            .Take(1);

    public IEnumerable<Student> GetStudentsOrderedByName()
        => _students.OrderBy(s => s.Name);

    public ILookup<string, Student> GroupStudentsByFaculty()
        => _students.ToLookup(s => s.Faculty);

    public string GetFacultyWithHighestAverageGrade()
        => _students
            .Where(s => s.Grades?.Any() == true)
            .GroupBy(s => s.Faculty)
            .Select(g => new {
                Faculty = g.Key,
                Avg = g.Average(s => s.Grades.Average())
            })
            .OrderByDescending(x => x.Avg)
            .FirstOrDefault()?.Faculty;
}
