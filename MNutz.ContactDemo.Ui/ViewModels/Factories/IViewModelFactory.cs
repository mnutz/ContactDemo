namespace MNutz.ContactDemo.Ui.ViewModels.Factories;

using Data;

/// <summary>
/// Defines a factory for creating view models from domain models.
/// </summary>
public interface IViewModelFactory
{
    /// <summary>
    /// Gets a cached <see cref="ContactCardViewModel" /> from a <see cref="IContact" /> or creates a new one.
    /// </summary>
    ContactCardViewModel GetContactCardViewModel(IContact contact);

    /// <summary>
    /// Gets a cached <see cref="ContactCardDisplayViewModel" /> from a <see cref="IContact" /> or creates a new one.
    /// </summary>
    ContactCardDisplayViewModel GetContactCardDisplayViewModel(IContact contact);

    /// <summary>
    /// Creates a new <see cref="ContactCardEditViewModel" /> from a <see cref="IContact" />.
    /// </summary>
    ContactCardEditViewModel CreateContactCardEditViewModel(IContact contact);

    /// <summary>
    /// Creates a new <see cref="ContactViewModel" /> from a <see cref="IContact" />.
    /// </summary>
    ContactViewModel CreateContactViewModel(IContact contact);

    /// <summary>
    /// Gets a cached <see cref="ContactViewModel" /> from a <see cref="IContact" /> or creates a new one.
    /// </summary>
    ContactViewModel GetContactViewModel(IContact contact);
}
