namespace MNutz.ContactDemo.Ui.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Data;

using Factories;

/// <summary>
/// Represents a view model for editing contacts.
/// </summary>
public partial class ContactCardEditViewModel(IContact contact) : ObservableObject
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
    public void Initialize() => Contact = ViewModelFactory.CreateContactViewModel(contact);

    /// <summary>
    /// Invoked, when the view should be changed to display.
    /// </summary>
    public event EventHandler? Displaying;

    [RelayCommand]
    private void Save()
    {
        ContactsRepository.UpdateContact(contact);
        Displaying?.Invoke(contact, EventArgs.Empty);
    }

    [RelayCommand]
    private void Cancel() => Displaying?.Invoke(contact, EventArgs.Empty);
}
