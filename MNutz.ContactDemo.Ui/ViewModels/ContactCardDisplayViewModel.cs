namespace MNutz.ContactDemo.Ui.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Data;

using Factories;

/// <summary>
/// Represents a view model for displaying contacts.
/// </summary>
public partial class ContactCardDisplayViewModel(IContact contact) : ObservableObject
{
    /// <summary>
    /// Gets or sets the factory for creating view models.
    /// This property is intended to be set by the dependency injection container.
    /// </summary>
    public required IViewModelFactory ViewModelFactory { get; init; }

    /// <summary>
    /// Gets or sets the repository for contacts.
    /// This property is intended to be set by the dependency injection container.
    /// </summary>
    public required IContactsRepository ContactsRepository { get; init; }

    /// <summary>
    /// Gets the contact view model.
    /// </summary>
    public ContactViewModel? Contact { get; private set; }

    /// <summary>
    /// Lazy initialization of the view model.
    /// </summary>
    public void Initialize() => Contact = ViewModelFactory.GetContactViewModel(contact);

    /// <summary>
    /// Invoked, when the view should change to edit
    /// </summary>
    public event EventHandler? Editing;

    [RelayCommand]
    private void Edit() => Editing?.Invoke(contact, EventArgs.Empty);

    [RelayCommand]
    private void Delete() => ContactsRepository.DeleteContact(contact);
}
