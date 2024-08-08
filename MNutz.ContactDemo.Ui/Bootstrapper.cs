namespace MNutz.ContactDemo.Ui;

using System.Diagnostics.CodeAnalysis;

using Autofac;
using Autofac.Configuration;

using Microsoft.Extensions.Configuration;

/// <summary>
/// Provides functionality to initialize and resolve dependencies using Autofac.
/// </summary>
public class Bootstrapper
{
    private ILifetimeScope? container;

    /// <summary>
    /// Initializes the dependency injection container.
    /// </summary>
    public void Initialize()
    {
        ContainerBuilder builder = new();
        RegisterJsonModule(builder, "autofac.json");
        container = builder.Build();
    }

    /// <summary>
    /// Resolves an instance of the specified type from the container.
    /// </summary>
    /// <typeparam name="T">The type of the instance to resolve.</typeparam>
    /// <returns>An instance of the specified type.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the container has not been initialized by calling <see cref="Initialize" />.
    /// </exception>
    public T Resolve<T>() where T : class
    {
        AssertScopeInitialized();
        return container.Resolve<T>();
    }

    [MemberNotNull(nameof(container))]
    private void AssertScopeInitialized()
    {
        if (container is null)
        {
            throw new InvalidOperationException(
                $"Scope is not initialized. You must call {nameof(Initialize)} first!");
        }
    }

    private static void RegisterJsonModule(ContainerBuilder builder, string fileName)
    {
        ConfigurationBuilder config = new();
        _ = config.AddJsonFile(fileName);
        ConfigurationModule module = new(config.Build());
        _ = builder.RegisterModule(module);
    }
}
