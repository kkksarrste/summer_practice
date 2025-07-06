using System;
using System.Reflection;
using System.Reflection.Emit;

public interface ICalculator
{
    int Add(int a, int b);
    int Subtract(int a, int b);
    int Multiply(int a, int b);
    int Divide(int a, int b);
}

public static class CalculatorFactory
{
    public static ICalculator CreateCalculator()
    {
        var assemblyName = new AssemblyName("DynamicCalculator");
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
        
        var typeBuilder = moduleBuilder.DefineType(
            "Calculator",
            TypeAttributes.Public | TypeAttributes.Class,
            null,
            new[] { typeof(ICalculator) });

        CreateMethod(typeBuilder, "Add", typeof(int), new[] { typeof(int), typeof(int) },
            il => {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldarg_2);
                il.Emit(OpCodes.Add);
                il.Emit(OpCodes.Ret);
            });

        CreateMethod(typeBuilder, "Subtract", typeof(int), new[] { typeof(int), typeof(int) },
            il => {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldarg_2);
                il.Emit(OpCodes.Sub);
                il.Emit(OpCodes.Ret);
            });

        CreateMethod(typeBuilder, "Multiply", typeof(int), new[] { typeof(int), typeof(int) },
            il => {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldarg_2);
                il.Emit(OpCodes.Mul);
                il.Emit(OpCodes.Ret);
            });

        CreateMethod(typeBuilder, "Divide", typeof(int), new[] { typeof(int), typeof(int) },
            il => {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldarg_2);
                il.Emit(OpCodes.Div);
                il.Emit(OpCodes.Ret);
            });

        var type = typeBuilder.CreateType()!;
        return (ICalculator)Activator.CreateInstance(type)!;
    }

    private static void CreateMethod(
        TypeBuilder typeBuilder,
        string methodName,
        Type returnType,
        Type[] parameterTypes,
        Action<ILGenerator> emitAction)
    {
        var methodBuilder = typeBuilder.DefineMethod(
            methodName,
            MethodAttributes.Public | MethodAttributes.Virtual,
            returnType,
            parameterTypes);

        var il = methodBuilder.GetILGenerator();
        emitAction(il);
        
        var baseMethod = typeof(ICalculator).GetMethod(methodName)!;
        typeBuilder.DefineMethodOverride(methodBuilder, baseMethod);
    }
}

class Program
{
    static void Main()
    {
        try
        {
            var calculator = CalculatorFactory.CreateCalculator();
            Console.WriteLine($"2 + 3 = {calculator.Add(2, 3)}");
            Console.WriteLine($"5 - 2 = {calculator.Subtract(5, 2)}");
            Console.WriteLine($"3 * 4 = {calculator.Multiply(3, 4)}");
            Console.WriteLine($"8 / 2 = {calculator.Divide(8, 2)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
