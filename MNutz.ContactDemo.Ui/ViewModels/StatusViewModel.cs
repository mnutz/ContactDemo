namespace MNutz.ContactDemo.Ui.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

using Data;

/// <summary>
/// Represents the view model for the main view.
/// </summary>
public partial class StatusViewModel : ObservableObject
{
    [ObservableProperty] private long numContacts;

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
        ContactsRepository.CollectionChanged += HandleRepositoryChanged;
    }

    private void HandleRepositoryChanged(object? sender, EventArgs e) => UpdateProperties();

    private void UpdateProperties() => NumContacts = ContactsRepository.GetContacts().Count;
}
