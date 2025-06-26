using System;
using System.Collections.Generic;
using System.Linq;

namespace task02
{
    public class Student
    {
        public required string Name { get; set; }
        public required string Faculty { get; set; }
        public List<int>? Grades { get; set; }
    }

    public class StudentService
    {
        private readonly List<Student> _students;

        public StudentService(List<Student> students)
        {
            _students = students ?? new List<Student>();
        }

        public IEnumerable<Student> GetStudentsByFaculty(string faculty)
        {
            return from student in _students
                   where student.Faculty == faculty
                   select student;
        }

        public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minGrade)
        {
            return _students
                .Where(s => s.Grades != null && s.Grades.Any())
                .Where(s => s.Grades!.Average() >= minGrade);
        }

        public IEnumerable<Student> GetStudentsOrderedByName()
        {
            return _students
                .OrderBy(s => s.Name, StringComparer.CurrentCulture);
        }

        public ILookup<string, Student> GroupStudentsByFaculty()
        {
            return _students.ToLookup(s => s.Faculty);
        }

        public string? GetFacultyWithHighestAverageGrade()
        {
            var facultiesWithGrades = _students
                .Where(s => s.Grades != null && s.Grades.Any())
                .GroupBy(s => s.Faculty)
                .Select(group => new
                {
                    FacultyName = group.Key,
                    AvgScore = group.Average(s => s.Grades!.Average()) 
                })
                .ToList();

            return facultiesWithGrades.Any() 
                ? facultiesWithGrades.OrderByDescending(x => x.AvgScore).First().FacultyName 
                : null;
        }
    }
}
