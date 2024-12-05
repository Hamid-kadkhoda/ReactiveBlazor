namespace Microsoft.Extensions.DependencyInjection;

public static class AutoDependencyInjection
{
    /// <summary>
    /// Automatically registers services based on marker interfaces (`IScoped`, `ITransient`, `ISingleton`).
    /// </summary>
    /// <param name="services">The IServiceCollection to register services into.</param>
    public static void AutoRegister(this IServiceCollection services)
    {
        // Get all assemblies in the current app domain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Iterate through all loaded assemblies
        foreach (var assembly in assemblies)
        {
            // Get all non-abstract, non-generic classes from the assembly
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType);

            foreach (var type in types)
            {
                // Get all interfaces implemented by this class
                var interfaces = type.GetInterfaces();

                // Register as Scoped if it implements IScoped
                if (interfaces.Any(i => i == typeof(IScoped)))
                {
                    foreach (var @interface in interfaces.Where(i => i != typeof(IScoped)))
                    {
                        services.AddScoped(@interface, type);
                    }
                }

                // Register as Transient if it implements ITransient
                else if (interfaces.Any(i => i == typeof(ITransient)))
                {
                    foreach (var @interface in interfaces.Where(i => i != typeof(ITransient)))
                    {
                        services.AddTransient(@interface, type);
                    }
                }

                // Register as Singleton if it implements ISingleton
                else if (interfaces.Any(i => i == typeof(ISingleton)))
                {
                    foreach (var @interface in interfaces.Where(i => i != typeof(ISingleton)))
                    {
                        services.AddSingleton(@interface, type);
                    }
                }
            }
        }

        // Optional: Add logging to verify types are being registered
        Console.WriteLine("Auto registration of services completed.");
    }
}