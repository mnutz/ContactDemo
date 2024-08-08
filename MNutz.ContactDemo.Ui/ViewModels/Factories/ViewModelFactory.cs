namespace MNutz.ContactDemo.Ui.ViewModels.Factories;

using System.Collections.Concurrent;

using Autofac;

using CommunityToolkit.Mvvm.ComponentModel;

using Data;

/// <summary>
/// A factory for creating and caching view models.
/// </summary>
public class ViewModelFactory : IViewModelFactory
{
    private readonly ConcurrentDictionary<Tuple<object?, Type>, ObservableObject> cache = new();

    /// <summary>
    /// Gets or sets the Autofac lifetime scope for resolving dependencies.
    /// </summary>
    public required ILifetimeScope Scope { get; init; }

    /// <inheritdoc />
    public ContactCardViewModel GetContactCardViewModel(IContact contact) => GetCached(contact,
        c => Scope.Resolve<ContactCardViewModel>(new NamedParameter("contact", c)));

    /// <inheritdoc />
    public ContactCardDisplayViewModel GetContactCardDisplayViewModel(IContact contact) =>
        GetCached(contact,
            c => Scope.Resolve<ContactCardDisplayViewModel>(new NamedParameter("contact", c)));

    /// <inheritdoc />
    public ContactCardEditViewModel CreateContactCardEditViewModel(IContact contact) =>
        Scope.Resolve<ContactCardEditViewModel>(new NamedParameter("contact", contact));

    /// <inheritdoc />
    public ContactViewModel CreateContactViewModel(IContact contact) =>
        Scope.Resolve<ContactViewModel>(new NamedParameter("contact", contact));

    /// <inheritdoc />
    public ContactViewModel GetContactViewModel(IContact contact) => GetCached(contact,
        c => Scope.Resolve<ContactViewModel>(new NamedParameter("contact", c)));

    private T GetCached<T, TKey>(TKey key, Func<TKey, T> initializer) where T : ObservableObject
    {
        var typedKey = AddKeyType<T>(key);
        var obj = cache.GetOrAdd(typedKey, k => initializer((TKey)k.Item1!));

        return obj as T ?? throw new InvalidOperationException("Object is null!");
    }

    private static Tuple<object?, Type> AddKeyType<T>(object? key) => Tuple.Create(key, typeof(T));
}
