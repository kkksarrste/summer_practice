using System;
using System.Reflection;

namespace task07
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisplayNameAttribute : Attribute
    {
        public string DisplayName { get; }

        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute : Attribute
    {
        public int Major { get; }
        public int Minor { get; }

        public VersionAttribute(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }
    }

    [DisplayName("Пример класса")]
    [Version(1, 0)]
    public class SampleClass
    {
        [DisplayName("Числовое свойство")]
        public int Number { get; set; }

        [DisplayName("Тестовый метод")]
        public void TestMethod() { }

        public void MethodWithoutAttribute() { }
    }

    public static class ReflectionHelper
    {
        public static string PrintTypeInfo(Type type)
        {
            string result = "";
            var displayNameAttr = type.GetCustomAttribute<DisplayNameAttribute>();
            if (displayNameAttr != null)
            {
                result += $"DisplayName: {displayNameAttr.DisplayName}\n";
            }

            var versionAttr = type.GetCustomAttribute<VersionAttribute>();
            if (versionAttr != null)
            {
                result += $"Version: {versionAttr.Major}.{versionAttr.Minor}\n";
            }

            result += "Properties:\n";
            foreach (var prop in type.GetProperties())
            {
                var propAttr = prop.GetCustomAttribute<DisplayNameAttribute>();
                result += propAttr != null 
                    ? $"{prop.Name} (DisplayName: {propAttr.DisplayName})\n" 
                    : $"{prop.Name}\n";
            }

            result += "Methods:\n";
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (method.IsSpecialName) continue;

                var methodAttr = method.GetCustomAttribute<DisplayNameAttribute>();
                result += methodAttr != null 
                    ? $"{method.Name} (DisplayName: {methodAttr.DisplayName})\n" 
                    : $"{method.Name}\n";
            }

            return result;
        }
    }
}
