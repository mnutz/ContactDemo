namespace MNutz.ContactDemo.Ui.ViewModels;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using CommunityToolkit.Mvvm.ComponentModel;

using Data;

/// <summary>
/// Represents a contact.
/// </summary>
/// <remarks>
/// Instantiates a new <see cref="ContactViewModel" />
/// </remarks>
public partial class ContactViewModel(IContact contact) : ObservableObject
{
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private long id;
    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string phone = string.Empty;

    /// <summary>
    /// Gets or sets the repository for contacts.
    /// This property is intended to be set by the dependency injection container.
    /// </summary>
    public required IContactsRepository ContactsRepository { get; init; }

    /// <summary>
    /// Lazy initialization of the view model.
    /// </summary>
    public void Initialize()
    {
        UpdateProperties();
        contact.PropertyChanged += HandlePropertyChanged;
    }

    private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e) => UpdateProperties();

#pragma warning disable MVVMTK0034 // Direct field reference to [ObservableProperty] backing field
    [MemberNotNull(nameof(name), nameof(email), nameof(phone))]
#pragma warning restore MVVMTK0034 // Direct field reference to [ObservableProperty] backing field
    private void UpdateProperties()
    {
        var updatedContact = ContactsRepository.GetContactById(contact.Id);
        if (updatedContact is null)
        {
            throw new InvalidOperationException($"{nameof(updatedContact)}can not be null!");
        }

        Id = updatedContact.Id;
        Name = updatedContact.Name;
        Email = updatedContact.Email;
        Phone = updatedContact.Phone;
    }

    partial void OnNameChanged(string value) => contact.Name = value;
    partial void OnEmailChanged(string value) => contact.Email = value;
    partial void OnPhoneChanged(string value) => contact.Phone = value;
}
