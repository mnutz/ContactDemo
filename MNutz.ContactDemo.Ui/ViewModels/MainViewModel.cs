namespace MNutz.ContactDemo.Ui.ViewModels;

using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Data;

using Factories;

/// <summary>
/// Represents the view model for the main view.
/// </summary>
public class MainViewModel : ObservableObject
{
    /// <summary>
    /// Gets the collection of contacts.
    /// </summary>
    public ObservableCollection<ContactCardViewModel> Contacts { get; } = [];

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
    /// Lazy initialization of the view model.
    /// </summary>
    public void Initialize()
    {
        UpdateContactsList();
        ContactsRepository.CollectionChanged += HandleRepositoryChanged;
    }

    private void HandleRepositoryChanged(object? sender, EventArgs e) => UpdateContactsList();

    private void UpdateContactsList()
    {
        var contacts = ContactsRepository.GetContacts();

        var viewModels = contacts.Select(ViewModelFactory.GetContactCardViewModel).ToArray();

        var toAdd = viewModels.Except(Contacts).ToArray();
        var toDelete = Contacts.Except(viewModels).ToArray();

        foreach (var viewModel in toDelete)
        {
            _ = Contacts.Remove(viewModel);
        }

        foreach (var viewModel in toAdd)
        {
            Contacts.Add(viewModel);
        }
    }
}
