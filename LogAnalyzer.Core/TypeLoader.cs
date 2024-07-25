using System.Reflection;

namespace LogAnalyzer.Core;

public class TypeLoader
{
    public static IEnumerable<Type> LoadAssembliesTypesByBase(string assemblyNameFormat, Type baseType)
    {
        IEnumerable<string> relevantAssemblyNames = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, $"{assemblyNameFormat}*.dll");

        foreach (string assemblyName in relevantAssemblyNames)
        {
            IEnumerable<Type> loadTypesByBase = LoadTypesByBase(Path.Combine(assemblyName), baseType);
            foreach (Type type in loadTypesByBase)
                yield return type;
        }
    }

    private static IEnumerable<Type> LoadTypesByBase(string assemblyPath, Type baseType)
    {
        try
        {
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Type[] types = assembly.GetExportedTypes();

            return types
                .Where(t => IsSubclassOfRawGeneric(baseType, t) && !t.IsInterface && !t.IsAbstract);
        }
        catch (ReflectionTypeLoadException ex)
        {
            foreach (Exception? loaderException in ex.LoaderExceptions)
                Console.WriteLine($"Error while loading type: {loaderException?.Message}");

            return [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
            return [];
        }
    }

    private static bool IsSubclassOfRawGeneric(Type generic, Type? toCheck)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            if (toCheck.IsGenericType) 
                toCheck = toCheck.GetGenericTypeDefinition();
          
            if (generic == toCheck)
                return true;
          
            toCheck = toCheck.BaseType;
        }
        return false;
    }
}
