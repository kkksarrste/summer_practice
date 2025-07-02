using System;
using System.IO;
using System.Reflection;

namespace task09
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Укажите путь к DLL");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Файл не найден");
                return;
            }

            try
            {
                var assembly = Assembly.LoadFrom(args[0]);
                Console.WriteLine($"Библиотека: {assembly.GetName().Name}");
                
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass)
                    {
                        Console.WriteLine($"\nКласс: {type.Name}");
                        
                        Console.WriteLine("\nКонструкторы:");
                        foreach (var ctor in type.GetConstructors())
                        {
                            Console.Write($"  {type.Name}(");
                            var parameters = ctor.GetParameters();
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                                if (i < parameters.Length - 1) Console.Write(", ");
                            }
                            Console.WriteLine(")");
                        }

                        Console.WriteLine("\nМетоды:");
                        foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                        {
                            if (!method.IsSpecialName)
                            {
                                Console.Write($"  {method.ReturnType.Name} {method.Name}(");
                                var parameters = method.GetParameters();
                                for (int i = 0; i < parameters.Length; i++)
                                {
                                    Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                                    if (i < parameters.Length - 1) Console.Write(", ");
                                }
                                Console.WriteLine(")");
                            }
                        }

                        Console.WriteLine("\nСвойства:");
                        foreach (var prop in type.GetProperties())
                        {
                            Console.WriteLine($"  {prop.PropertyType.Name} {prop.Name}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка");
            }
        }
    }
}
