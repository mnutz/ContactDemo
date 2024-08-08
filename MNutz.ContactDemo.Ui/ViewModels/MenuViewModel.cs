namespace MNutz.ContactDemo.Ui.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Data;

public partial class MenuViewModel : ObservableObject
{
    /// <summary>
    /// Gets or sets the repository for contacts.
    /// This property is intended to be set by the dependency injection container.
    /// </summary>
    public required IContactsRepository ContactsRepository { get; init; }

    /// <summary>
    /// Command for adding a user.
    /// </summary>
    [RelayCommand]
    public void AddUser()
    {
        var contact = ContactsRepository.CreateContact();
        contact.Name = "New User";
        ContactsRepository.InsertContact(contact);
    }
}
