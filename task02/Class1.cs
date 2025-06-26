using System;
using System.Collections.Generic;
using System.Linq;

namespace task02
{
    public class Student
    {
        public string Name { get; set; }
        public string Faculty { get; set; }
        public List<int> Grades { get; set; }
    }

    public class StudentService
    {
        private readonly List<Student> _students;

        public StudentService(List<Student> students) => 
            _students = students ?? new List<Student>();

        public IEnumerable<Student> GetStudentsByFaculty(string faculty) => 
            _students.Where(s => s.Faculty == faculty);

        public IEnumerable<Student> GetStudentsWithMinAverageGrade(double minGrade) => 
            _students
                .Where(s => s.Grades?.Any() == true)
                .Where(s => s.Grades.Average() >= minGrade);

        public IEnumerable<Student> GetStudentsOrderedByName() => 
            _students
                .Where(s => !string.IsNullOrEmpty(s.Name))
                .OrderBy(s => s.Name, StringComparer.CurrentCulture);

        public ILookup<string, Student> GroupStudentsByFaculty() => 
            _students
                .Where(s => s.Faculty != null)
                .ToLookup(s => s.Faculty);

        public string GetFacultyWithHighestAverageGrade()
        {
            var facultiesWithGrades = _students
                .Where(s => s.Faculty != null && s.Grades?.Any() == true)
                .GroupBy(s => s.Faculty);

            if (!facultiesWithGrades.Any())
                return null;

            return facultiesWithGrades
                .Select(g => new {
                    Faculty = g.Key,
                    Avg = g.Average(s => s.Grades.Average())
                })
                .OrderByDescending(x => x.Avg)
                .First().Faculty;
        }
    }
}
